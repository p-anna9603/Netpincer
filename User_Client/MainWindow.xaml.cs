using RestaurantClient;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace User_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }

        private void BTN_Register_Click(object sender, RoutedEventArgs e)
        {
            if (KotelezoMezoEllenorzes())
            {
                // User(string _username, string _password, string _lastName, string _firstName, string _phoneNumber, string _city, string _zipcode, string _line1, string _line2)/
                string line1 = TXTB_Street_Name.Text + " " + TXTB_House_Number.Text;
                User adat = new User(TXTB_User_Name.Text, TXTB_Password.Text, TXTB_Last_Name.Text, TXTB_First_Name.Text, TXTB_Phone.Text, TXTB_City.Text, TXT_Zip_Code.Text, line1, TXTB_Line2.Text);

            }
            else
            {

            }

        }

        private void BTN_Exit_Click(object sender, RoutedEventArgs e)
        {

        }
        private void TXTB_First_Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private bool KotelezoMezoEllenorzes()
        {
            if (TXTB_First_Name.Text == "")
            {
                TXTB_First_Name.Focus();
                return false;
            }
            return true;
        }
    }
}
