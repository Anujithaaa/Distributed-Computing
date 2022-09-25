using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

//Anujitha Fernando (20903448) & Senara Mallikahewa (20903435) - Assignment 1

namespace Authenticator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to the Server");
                Console.WriteLine("Anujitha Fernando (20903448) & Senara Mallikahewa (20903435)");

                ServiceHost host;                                   
                NetTcpBinding tcpBinding = new NetTcpBinding();     

                host = new ServiceHost(typeof(AuthenticatorIMPL));
                host.AddServiceEndpoint(typeof(AuthenticatorInterface), tcpBinding, "net.tcp://localhost:50001/AuthenticationService");

                //Open connection
                host.Open();        

                //Hold the server open until someone does something
                Console.WriteLine("Authenticator server is online");
                Console.ReadLine();

                //Close the host
                host.Close();        

            }
            catch (FaultException e)
            {
                Console.WriteLine("Error", e);
            }
        }
    }
}
