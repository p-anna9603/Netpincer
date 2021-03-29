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
            //ServerConnection.getFoods(7, 1);
            ServerConnection.getRestaurantsList();
            //ServerConnection.getCategories(1);
            //ServerConnection.getRestaurant("egy").toString();
            /* ServerConnection.addCategory("Leves", "Hiiiii");            //Category didn't exist, now added to menu
             ServerConnection.addCategory("Leves", "Hiiiii");            //Category already exists and is part of menu
             ServerConnection.addCategory("Leves", "AsztalVok1149");     //Category already exists but added to menu
             ServerConnection.addCategory("Leves", "Teszto1");           //Restaurant not found
             ServerConnection.addCategory("Alma", "Teszto11");*/           //Restaurant not found
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
                    Console.WriteLine("name: " + user.Username);
                    Restaurant rest = ServerConnection.getRestaurant(user.Username);
                    RestaurantMain restMain = new RestaurantMain(ServerConnection, rest, this);
                    restMain.Show();
                    restaurantSign.IsChecked = false;
                    this.Hide();
                }
                else if(user.GetUserType == UserType.Customer)
                {
                    UserMain usrMain = new UserMain(ServerConnection, user, this);
                    usrMain.Show();
                    clientSign.IsChecked = false;
                    this.Hide();
                }
                else if (user.GetUserType == UserType.DeliveryPerson)
                {
                    // TODO delivery person interface
                    //runningBoySign.IsChecked = false;
                }
                textBoxPassword.Password = "";
                textBoxUserName.Text = "";
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
                    registerUser regUser = new registerUser(ServerConnection, this);
                    regUser.Show();
                    clientReg.IsChecked = false;
                    this.Hide();
                }
                else if (restaurantReg.IsChecked == true)
                {
                    registryType = 1;
                    RestaurantRegister restMain = new RestaurantRegister(ServerConnection, this);
                    restMain.Show();
                    restaurantReg.IsChecked = false;
                    this.Hide();
                }
                else if (runningBoyReg.IsChecked == true)
                {
                    registryType = 2;
                    //TODO
                    //runningBoyReg.IsChecked = false;
                }
                // Server plíz register me
            }
        }
    }
}
