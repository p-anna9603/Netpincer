using FoodOrderClient;
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
using System.Windows.Shapes;

namespace RestaurantClient
{
    /// <summary>
    /// Interaction logic for startupWindow.xaml
    /// </summary>
    public partial class startupWindow : Window
    {
        string userName;
        string password;

        UserType signInType;
        int registryType;
        public ConnectToServer ServerConnection;

        public startupWindow()
        {
            InitializeComponent();
           ServerConnection = new ConnectToServer();
        }

        private void signInBtn_Click(object sender, RoutedEventArgs e)
        {
           if(textBoxUserName.Text == "")
            {
                errorText.Text = "Adja meg a felhasználó nevét!";
            }
           else if(textBoxPassword.Password == "")
            {
                errorText.Text = "Adja meg a jelszavát!";
            }
            else
            {
                if(clientSign.IsChecked == true)
                {
                    signInType = UserType.Customer;
                }
                else if (restaurantSign.IsChecked == true)
                {
                    signInType = UserType.RestaurantOwner;
                }
                else if (runningBoySign.IsChecked == true)
                {
                    signInType = UserType.DeliveryPerson;
                }
                else
                {
                    errorText.Text = "Adja meg a belépés típusát!";
                    return;
                }
                userName = textBoxUserName.Text;
                password = textBoxPassword.Password;
                User user = ServerConnection.getUser(userName, password, signInType);
                if(user.GetUserType == UserType.RestaurantOwner)
                {
                    RestaurantMain restMain = new RestaurantMain(ServerConnection);
                    restMain.Show();
                    this.Hide();
                }
            }
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            if(clientReg.IsChecked == false && runningBoyReg.IsChecked == false && restaurantReg.IsChecked == false)
            {
                errorText.Text = "Válassza ki a regisztráció típusát!";
            }
            else
            {
                if(clientReg.IsChecked == true)
                {
                    registryType = 0;
                    registerUser regUser = new registerUser();
                    regUser.Show();
                    this.Hide();
                }
                else if (restaurantReg.IsChecked == true)
                {
                    registryType = 1;
                    RestaurantMain restMain = new RestaurantMain(ServerConnection);
                    restMain.Show();
                    this.Close();
                }
                else if (runningBoyReg.IsChecked == true)
                {
                    registryType = 2;
                    //TODO
                }
                // Server plíz register me
            }
        }
    }
}
