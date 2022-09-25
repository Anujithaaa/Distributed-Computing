using Authenticator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RegistryProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Web.Http;

//Anujitha Fernando (20903448) & Senara Mallikahewa (20903435) - Assignment 1

namespace RegistryProject.Controllers
{
    public class RegistryController : ApiController
    {
       
        Authenticator.AuthenticatorInterface foob;
        public RegistryController()
        {

            ChannelFactory<Authenticator.AuthenticatorInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            string URL = "net.tcp://localhost:50001/AuthenticationService";

            foobFactory = new ChannelFactory<Authenticator.AuthenticatorInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
        }

        [HttpPost]
        [Route("publish/{num1}")]
        public string Publish()
        {
            string path = @"F:\DC assignment 1\serviceDescription.txt";

            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            serviceInputModel model = JsonConvert.DeserializeObject<serviceInputModel>(jsonContent);

            using (StreamWriter sw = new StreamWriter(path, append: true))
            {
                sw.WriteLine("{\nName : " + model.Name);
                sw.WriteLine("\nDescription : " + model.Description);
                sw.WriteLine("\nAPI End point : " + model.ApiEndpoint);
                sw.WriteLine("\nNumber of operands : " + model.NumberOfOperands);
                sw.WriteLine("\nOperand type : " + model.OperandType + "\n}\n");
            }

            return "Published Successfully!";
        }


        [HttpPost]
        [Route("unpublish/{num2}")]
        public string Unpublish()
        {
            string path = @"F:\DC assignment 1\serviceDescription.txt";
            string path2 = @"F:\DC assignment 1\service.txt";

            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;

            string a = jsonContent.Remove(0, 1);
            string serviceName = a.Remove(a.Length - 1, 1);

            try
            {
                int lineNumber = 1, delLineNum = 0;
                string line = null;
                string line_to_delete = "Name : "+ serviceName;

                using (StreamReader reader = new StreamReader(path))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (String.Compare(line, line_to_delete) == 0)
                        {
                            delLineNum = lineNumber;
                            break;
                        }

                        lineNumber++;
                    }
                }

                lineNumber = 1;
                delLineNum--;

                using (StreamReader reader = new StreamReader(path))
                {
                    
                    using (StreamWriter writer = new StreamWriter(path2, append: true))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (lineNumber <= delLineNum + 11 && lineNumber >= delLineNum)
                            {
                                
                            }
                            else
                            {
                                writer.WriteLine(line);
                            }
                            
                            lineNumber++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileInfo fi = new FileInfo(path2);
            if (fi.Exists)
            {
                fi.MoveTo(@"F:\DC assignment 1\serviceDescription.txt");
            }

            return "Unpublished Successfully!";
        }


        [HttpPost]
        [Route("allservices")]
        public string Allservices()
        {
            string path = @"F:\DC assignment 1\serviceDescription.txt";
            string text;

            using (StreamReader streamReader = File.OpenText(path))
            {
                text = streamReader.ReadToEnd();
            }

            return text;
        }


        [Route("deleteService/{ApiEndpoint}/{token}")]
        [HttpDelete]

        public IHttpActionResult deleteAllServices(string apiEndpoint, int token)
        {
            string returnResult = foob.validate(token);
            string textFilePath = @"F:\DC assignment 1\serviceDescription.txt";
            string appendJson = "";



            if (returnResult.Equals("validated"))
            {

                using (var sr = new StreamReader(textFilePath))
                {
                    string line;
                    var index = 0;
                    string lineAll = sr.ReadToEnd();

                    List<serviceInputModel> serveReg = JsonConvert.DeserializeObject<List<serviceInputModel>>(lineAll);

                    List<serviceInputModel> newServiceList = new List<serviceInputModel>();

                    foreach (serviceInputModel serveList in serveReg)
                    {
                        if (!serveList.ApiEndpoint.Contains(apiEndpoint))
                        {


                            newServiceList.Add(serveList);



                        }





                    }
                    appendJson = JsonConvert.SerializeObject(newServiceList, Formatting.Indented);

                }
                File.WriteAllText(textFilePath, appendJson.ToString());
                return Ok();
            }
            else
            {

                return Ok("{“Status”: “Denied”,“Reason”: “Authentication Error”}");
            }



        }


        [HttpPost]
        [Route("search/{num3}")]
        public string Search()
        {
            string path = @"F:\DC assignment 1\serviceDescription.txt";

            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;

            string a = jsonContent.Remove(0, 1);
            string searchName = a.Remove(a.Length - 1, 1);

            int[] lineNumber = new int[1000];
            int i = 0, cline = 1, b = 0;
            string line = null;
            string finalString = "";

            try
            {
                
                using (StreamReader reader = new StreamReader(path))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Substring(0, line.Length).Contains(searchName))
                        {
                            lineNumber[i] = cline;
                            i++;
                        }

                        cline++;
                    }
                }

                cline = 1;
                i = 0;
                

                using (StreamReader reader = new StreamReader(path))
                {
                    b = lineNumber[i]-- + 11;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if ( cline <= b && cline >= b - 11)
                        {
                            finalString = finalString + line;
                        }

                        if (cline > b)
                        {
                            i++;
                            b = lineNumber[i]-- + 11;
                        }

                        cline++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return finalString;
        }
    }
}
