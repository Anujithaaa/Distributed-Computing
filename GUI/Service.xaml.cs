using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

//Anujitha Fernando (20903448) & Senara Mallikahewa (20903435) - Assignment 1

namespace GUI
{
    /// <summary>
    /// Interaction logic for Service.xaml
    /// </summary>
    public partial class Service : Window
    {
        int loginToken = 0;
        static HttpClient client = new HttpClient();
        List<string> services = new List<string>();

        public Service(int token)
        {
            InitializeComponent();
            loginToken = token;
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Connect with the registry project to display all services
            var response = await client.PostAsync("https://localhost:44392/allservices", null);

            string result = response.Content.ReadAsStringAsync().Result;

            services.Add("ADDTwoNumbers");
            services.Add("ADDThreeNumbers");
            services.Add("MulTwoNumbers");
            services.Add("MulThreeNumbers");

            servicesListView.ItemsSource = services;

            lNum2.Visibility = Visibility.Hidden;
            txtNum2.Visibility = Visibility.Hidden;

            lNum3.Visibility = Visibility.Hidden;
            txtNum3.Visibility = Visibility.Hidden;
        }

        // Function to get the selected service by user
        private void serviceSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string serviceSelected = servicesListView.SelectedItem.ToString();

            lServiceSelected.Content = serviceSelected;

            if(String.Compare(serviceSelected, "ADDTwoNumbers") == 0)
            {
                lNum2.Visibility = Visibility.Visible;
                txtNum2.Visibility = Visibility.Visible;

                lNum3.Visibility = Visibility.Hidden;
                txtNum3.Visibility = Visibility.Hidden;

                txtNum1.Text = "";
                txtNum2.Text = "";
                txtNum3.Text = "";
            }
            else if (String.Compare(serviceSelected, "ADDThreeNumbers") == 0)
            {
                lNum2.Visibility = Visibility.Visible;
                txtNum2.Visibility = Visibility.Visible;

                lNum3.Visibility = Visibility.Visible;
                txtNum3.Visibility = Visibility.Visible;

                txtNum1.Text = "";
                txtNum2.Text = "";
                txtNum3.Text = "";
            }
            else if (String.Compare(serviceSelected, "MulTwoNumbers") == 0)
            {
                lNum2.Visibility = Visibility.Visible;
                txtNum2.Visibility = Visibility.Visible;

                lNum3.Visibility = Visibility.Hidden;
                txtNum3.Visibility = Visibility.Hidden;

                txtNum1.Text = "";
                txtNum2.Text = "";
                txtNum3.Text = "";
            }
            else if (String.Compare(serviceSelected, "MulThreeNumbers") == 0)
            {
                lNum2.Visibility = Visibility.Visible;
                txtNum2.Visibility = Visibility.Visible;

                lNum3.Visibility = Visibility.Visible;
                txtNum3.Visibility = Visibility.Visible;

                txtNum1.Text = "";
                txtNum2.Text = "";
                txtNum3.Text = "";
            }
            
        }

        // Search button function
        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("\nEnter the service name to unpublish: ");
            string sname = txtSearchService.Text;

            var jsonData = JsonConvert.SerializeObject(sname);
            var data = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:44389/unpublish/{0}", data);

            List<string> filteredServices = new List<string>();

            foreach (string serv in services)
            {
                if (serv.Substring(0, serv.Length).Contains(sname))
                {
                    filteredServices.Add(serv);
                }
            }

            servicesListView.ItemsSource = filteredServices;
        }

        // Submit button function
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string serviceSelected = lServiceSelected.Content.ToString();

            if (String.Compare(serviceSelected, "ADDTwoNumbers") == 0)
            {
                int num1 = Convert.ToInt32(txtNum1.Text);
                int num2 = Convert.ToInt32(txtNum2.Text);

                var response = client.GetAsync("https://localhost:44389/add2Number/"+num1+"/"+num2).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                lValue.Content = result;
            }
            else if (String.Compare(serviceSelected, "ADDThreeNumbers") == 0)
            {
                int num1 = Convert.ToInt32(txtNum1.Text);
                int num2 = Convert.ToInt32(txtNum2.Text);
                int num3 = Convert.ToInt32(txtNum3.Text);

                var response = client.GetAsync("https://localhost:44389/add3Number/" + num1 + "/" + num2 + "/" + num3).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                lValue.Content = result;
            }
            else if (String.Compare(serviceSelected, "MulTwoNumbers") == 0)
            {
                int num1 = Convert.ToInt32(txtNum1.Text);
                int num2 = Convert.ToInt32(txtNum2.Text);

                var response = client.GetAsync("https://localhost:44389/multi2Number/" + num1 + "/" + num2).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                lValue.Content = result;
            }
            else if (String.Compare(serviceSelected, "MulThreeNumbers") == 0)
            {
                int num1 = Convert.ToInt32(txtNum1.Text);
                int num2 = Convert.ToInt32(txtNum2.Text);
                int num3 = Convert.ToInt32(txtNum3.Text);

                var response = client.GetAsync("https://localhost:44389/mult3Number/" + num1 + "/" + num2 + "/" + num3).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                lValue.Content = result;
            }
        }
    }
}
