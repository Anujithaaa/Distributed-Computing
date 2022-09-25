using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

//Anujitha Fernando (20903448) & Senara Mallikahewa (20903435) - Assignment 1

namespace Authenticator
{
    //Make service multithreaded
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    class AuthenticatorIMPL : AuthenticatorInterface
    {
        //constructor
        public int Login(string name, string Password)
        {
            Random random = new Random();

            int token = 0;

            //path to text files >> User details and Tokens
            string userdetailsPath = @"..\..\..\userdetails.txt";
            string tokenPath = @"..\..\..\tokens.txt";

            try
            {
                using (StreamReader sr = new StreamReader(userdetailsPath))
                {
                    string line = "", passwordLine = "";
                    Console.WriteLine("name: " + name);
                    while ((line = sr.ReadLine()) != null)
                    {
                        string username = "";
                        if (line != "")
                        {
                            username = line;
                        }
                        Console.WriteLine("Line: " + line);
                        Console.WriteLine("username: " + username);

                        if (string.Compare(username, name) == 0)
                        {
                            passwordLine = sr.ReadLine();

                            if (passwordLine.CompareTo(Password) == 0)
                            {
                                token = random.Next();

                                using (StreamWriter sw = new StreamWriter(tokenPath, append: true))
                                {
                                    sw.WriteLine(token + "\n");
                                    Console.WriteLine("Login Successful!\n");
                                    return token;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Passwords does not match\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Cannot find username\n");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return token;
        }

        public string Register(string name, string Password)
        {
            string path = @"..\..\..\userdetails.txt";

            using (StreamWriter sw = new StreamWriter(path, append: true))
            {

                sw.WriteLine(name + "\n" + Password + "\n");
                Console.WriteLine("Successfully Registered!");
            }

            return "Successfully Registered!";
        }

        //check if token is valid
        public string validate(int token)
        {
            string tokenPath = @"..\..\..\tokens.txt";
            string isValidated = "";

            try
            {
                using (StreamReader sr = new StreamReader(tokenPath))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.CompareTo(token) == 0)
                        {
                            isValidated = "validated";
                        }
                        else
                        {
                            isValidated = "not validated";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return isValidated;
        }
    }
}
