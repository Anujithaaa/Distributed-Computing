using Authenticator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RegistryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

//Anujitha Fernando (20903448) & Senara Mallikahewa (20903435) - Assignment 1

namespace ServicePublishing
{
    class Program
    {
        static AuthenticatorInterface authenticator;
        static HttpClient client = new HttpClient();


        // Connect to the Authenticator project
        public static void authConnect()
        {
            // Open server connection
            ChannelFactory<AuthenticatorInterface> dataFactory; 

            NetTcpBinding tcpBinding = new NetTcpBinding();

            string sURL = "net.tcp://localhost:50001/AuthenticationService";

            dataFactory = new ChannelFactory<AuthenticatorInterface>(tcpBinding, sURL);
            authenticator = dataFactory.CreateChannel();
        }

        static async Task Main(string[] args)
        {
            string operation;
            int token = 0;

            Console.WriteLine("Operations:\n1. Register\n2. Login\n3. Publish service\n4. Unpublish service\n5. Quit\n");

            Console.WriteLine("Enter an operation your would prefer ");
            operation = Console.ReadLine();

            do
            {

                // Register a User using the Authenticator Service 
                if (operation.Equals("Register"))
                {
                    authConnect();

                    Console.WriteLine("Enter username: ");
                    string username = Console.ReadLine();

                    Console.WriteLine("Enter password: ");
                    string password = Console.ReadLine();

                    authenticator.Register(username, password);

                    Console.WriteLine("Registered successfully!\n");
                }

                // Login a User using the Authenticator Service 
                if (operation.Equals("Login"))
                {
                    authConnect();

                    Console.WriteLine("Enter username: ");
                    string username = Console.ReadLine();

                    Console.WriteLine("Enter password: ");
                    string password = Console.ReadLine();

                    token = authenticator.Login(username, password);

                    Console.WriteLine("Login successful\n");
                }

                // Publish service process by calling the Registry service
                if (operation.Equals("Publish service"))
                {
                    serviceInputModel sim = new serviceInputModel();

                    Console.WriteLine("\nEnter the service name to publish: ");
                    sim.Name = Console.ReadLine();

                    Console.WriteLine("\nEnter the service description: ");
                    sim.Description = Console.ReadLine();

                    Console.WriteLine("\nEnter the service API endpoint: ");
                    sim.ApiEndpoint = Console.ReadLine();

                    Console.WriteLine("\nEnter the number of operands: ");
                    sim.NumberOfOperands = Console.ReadLine();

                    Console.WriteLine("\nEnter the operand type: ");
                    sim.OperandType = Console.ReadLine();

                    var jsonData = JsonConvert.SerializeObject(sim);
                    var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:44392/publish/{0}", data);

                    string result = response.Content.ReadAsStringAsync().Result;
                                        
                    Console.WriteLine("\n"+result+"\n");
                }

                // Unpublish service process by calling the registry service
                if (operation.Equals("Unpublish service"))
                {
                    Console.WriteLine("\nEnter the service name to unpublish: ");
                    string sname = Console.ReadLine();

                    var jsonData = JsonConvert.SerializeObject(sname);
                    var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://localhost:44392/unpublish/{0}", data);

                    string result = response.Content.ReadAsStringAsync().Result;

                    Console.WriteLine("\n" + result + "\n");
                }

                Console.WriteLine("Enter an operation your would prefer ");
                operation = Console.ReadLine();

            } while (!operation.Equals("Quit"));


            // Quit if user does not need any more services
            if (operation.Equals("Quit"))
            {
                Console.WriteLine("Thank you!\n");

                System.Environment.Exit(0);
            }
        }
    }
}
