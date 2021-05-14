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

            #region KLAU TESZT 04.25
            //ServerConnection.getOrders(1);        //OK
            //ServerConnection.updateOrderState(2, 3);    //OK
            //ServerConnection.getFoodByID(1);      //OK
            #endregion

            #region KLAU TESZT 05.12.
            /*List<string> list = new List<string>();
            ServerConnection.updateFood(new Food(13,"Keny",500,3.2,0,list,1,1,"2020.01.01","2020.01.01"));
           */

            #endregion


            #region KLAU TESZT 05.13.
            //ServerConnection.addOrderToDeliveryBoy(1,2);
            //ServerConnection.removeOrderFromDeliveryBoy(1,2);

            #endregion

            #region KLAU TESZT 05.14.
            ServerConnection.registerRestaurant(new Restaurant("Veszprem","8200","asd utca","ketto",8,30,22,15,"Szep etterem esku","minden jo es szep", "stilusos","etteremvezetoVokTscoo","+3620156165",60,-1,"Pilisi","pista","wgeee","asdn@asnds.com"));
            //ServerConnection.removeOrderFromDeliveryBoy(1,2);

            #endregion

            //ServerConnection.getFoods(7, 1);
            //ServerConnection.getRestaurantsList();
            List<string> a = new List<string>();
            a.Add("Liszt");
            a.Add("Cukor");
           // ServerConnection.addFood(new Food(-1,"Kenyer3",200,4.7,0,a,2,8,"",""));
            a.Add("Gluten");
            //   ServerConnection.addFood(new Food(-1, "Kenyer2", 200, 4.7, 0, a, 2, 8, "2020.06.06.", "2020.05.05."));
            //   
            /* For dummy restaurant sign in pls delete before pushing */

            //ServerConnection.setDiscount(1, 0.96);
            //ServerConnection.setApproximateDeliveryTime(5, 12);
            textBoxUserName.Text = "marica";
            textBoxPassword.Password = "marica";

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
                /*
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
                */
                signInType = UserType.RestaurantOwner;
                userName = textBoxUserName.Text;
                password = textBoxPassword.Password;
               
                Console.WriteLine("username: " + userName + ", pass: " + password + ", sign in: " + signInType);
                User user = ServerConnection.getUser(userName, password, signInType);
                if (ServerConnection.UserSignIn == 1)
                {
                    if (user.GetUserType == UserType.RestaurantOwner)
                    {
                        Console.WriteLine("name: " + user.Username);
                        Restaurant rest = ServerConnection.getRestaurant(user.Username);
                        RestaurantMain restMain = new RestaurantMain(ServerConnection, rest, this);
                        restMain.Show();
                       // restaurantSign.IsChecked = false;
                        this.Hide();
                    }
                    else if (user.GetUserType == UserType.Customer)
                    {
                        UserMain usrMain = new UserMain(ServerConnection, user, this);
                        usrMain.Show();
                      //  clientSign.IsChecked = false;
                        this.Hide();
                    }
                    else if (user.GetUserType == UserType.DeliveryPerson)
                    {
                        // TODO delivery person interface
                        //runningBoySign.IsChecked = false;
                    }
                    textBoxPassword.Password = "";
                    textBoxUserName.Text = "";
                    errorText.Text = "";
                    ServerConnection.UserSignIn = 0;
                }
                else
                {
                    errorText.Text = "Nincs ilyen felhasználó.";
                }
            }
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            registryType = 0;
            registerUser regUser = new registerUser(ServerConnection, this);
            regUser.Show();
            this.Hide();
            /*
            if (clientReg.IsChecked == false && runningBoyReg.IsChecked == false && restaurantReg.IsChecked == false)
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
            */
        }
    }
}
