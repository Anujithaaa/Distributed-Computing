using Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//Anujitha Fernando (20903448) & Senara Mallikahewa (20903435) - Assignment 1

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuthenticatorInterface authenticator;
        int token = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Connect with the Authenticator project
            ChannelFactory<AuthenticatorInterface> dataFactory; //open server connection

            NetTcpBinding tcpBinding = new NetTcpBinding();

            string sURL = "net.tcp://localhost:50001/AuthenticationService";

            dataFactory = new ChannelFactory<AuthenticatorInterface>(tcpBinding, sURL);
            authenticator = dataFactory.CreateChannel();
        }

        // Signup button function
        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            string username = txtcusername.Text;
            string password = txtcpassword.Password;

            authenticator.Register(username, password);

            txtcusername.Text = "";
            txtcpassword.Password = "";
        }

        // Login button function
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            token = authenticator.Login(txtusername.Text, txtpassword.Password);
                            
            var c = new Service(token);
            c.Show();
            this.Hide();

            txtusername.Text = "";
            txtpassword.Password = "";
            
        }
    }
}
