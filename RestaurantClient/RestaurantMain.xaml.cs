using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RestaurantClient;

namespace FoodOrderClient
{
    /// <summary>
    /// Interaction logic for RestaurantMain.xaml
    /// </summary>
    public partial class RestaurantMain : Window
    {
        RestaurantMenus menus;
        Orders orders;
        AssignDelivery assignDelivery;
        public UserControl child;
        private ConnectToServer serverConnection;
        Restaurant currUser;
        startupWindow parent;
        List<Order> newOrders = new List<Order>();
        List<Order> ordersList = new List<Order>();
        List<Order> checkedNewOrders = new List<Order>();

        public ConnectToServer ServerConnection { get => serverConnection; set => serverConnection = value; }
        public Restaurant CurrUser { get => currUser; set => currUser = value; }
        internal List<Order> CheckedNewOrders { get => checkedNewOrders; set => checkedNewOrders = value; }

        Order dummyOrder;
        Order dummyOrder2;
        Order dummyOrder3;
        Order dummyOrder4;
        Order dummyOrder5;
        List<string> dummyAllergs = new List<string>();
        List<Food> oneOrdersFoods = new List<Food>();
        Food food;
        Food food2;
        public RestaurantMain(ConnectToServer ServerCon, Restaurant usr, Window start)
        {
            InitializeComponent();
            Console.WriteLine(settingImg.Source.ToString());
            serverConnection = ServerCon;
            currUser = usr;
            parent = (startupWindow)start;
            userNameTextBox.Text = "Felhasználó: " + currUser.owner;
            //   ServerConnection = new ConnectToServer();

            /*
                        //CONNECTING TO SERVER      --Not Gonna work without the database!

                        //Console.WriteLine(ServerConnection.getUser("testUser", "t3stpassword",UserType.Customer).toString());           //OK
                        //ServerConnection.registerUser(new User("userFromClient","ass","Flex","Elek","+3699145825","Veszprem","8200","Ass utca 6","2/A",1,"imel@gmail.com"));
                        //Console.WriteLine(ServerConnection.getUser("testUser", "t3stpassword", UserType.RestaurantOwner).toString());   //NOT FOUND RETURNS ERROR MESSAGE
                        //Console.WriteLine(ServerConnection.getUser("testRestaurantOwner", "r3staurant", UserType.RestaurantOwner).toString());  //OK
                        ServerConnection.registerRestaurant(new Restaurant("Veszprem", "8200", "Faradt vagyok utca v2.0", "3/A", 10, 00, 23, 00, "Utalom a C capat", "Hosszabb leiras arrol, mennyire utlaom a Csharpot", "C capa", "Hiiiii","Jelszoo","aasd@gmail.com","+36214563217","Pistavok","Tscoo"));
                        //ServerConnection.StopClient();
                        List<string> categories = new List<string>();
                        categories = ServerConnection.addCategory("Levesek", "Hiiiii", "Utalom a C capat", UserType.RestaurantOwner);
                        categories = ServerConnection.addCategory("Pizzak", "Hiiiii", "Utalom a C capat", UserType.RestaurantOwner);
                        categories = ServerConnection.addCategory("Sutemenyek", "Hiiiii", "Utalom a C capat", UserType.RestaurantOwner);
                        //categories = ServerConnection.addCategory("Test Category", "Hiiiii", "Utalom a C capat", UserType.RestaurantOwner);

                        for (int i=0;i<categories.Count;++i)
                            Console.WriteLine("CATEGORIES FOR UTALOM A C CAPAT: \n {0}", categories[i]);
            */
            dummyAllergs.Add("glutén");
            food = new Food(1, "Paprikás pizza", 1200, 3, 0, dummyAllergs, 2, 2, "2021.03.11", "2022.01.01");
            oneOrdersFoods.Add(food);
            dummyOrder = new Order(1, 0, "2021.04.22 12:22", "Anna", 2000, oneOrdersFoods);
            ordersList.Add(dummyOrder);

            dummyOrder3 = new Order(3, 2, "2021.04.22 14:22", "Zoli", 2000, oneOrdersFoods);
            ordersList.Add(dummyOrder3);

            dummyOrder4 = new Order(4, 0, "2021.04.22 14:22", "Réka", 2000, oneOrdersFoods);
            ordersList.Add(dummyOrder4);
        }
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_messages.Visibility = Visibility.Collapsed;
                tt_maps.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_messages.Visibility = Visibility.Visible;
                tt_maps.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e) // menü összecsukása
        {
            img_bg.Opacity = 1;
            childWindow.Opacity = 1;
            Console.WriteLine("tgbutn unchecked");
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e) // menü megnyitása
        {
            img_bg.Opacity = 0.3;
            childWindow.Opacity = 0.3;
            Console.WriteLine("tgbutn checked");
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = LV.SelectedIndex;
            //      MoveCursorMenu(index);
            Console.WriteLine("listview menu" + index);
            switch (index)
            {
                case 0:
                    //GridPrincipal.Children.Clear();
                    childWindow.Content = null;
                    Console.WriteLine("0 clear");
                    break;
                case 1:
                    //GridPrincipal.Children.Clear();
                    Console.WriteLine("1 clear");
                    childWindow.Content = null;
                    menus = new RestaurantMenus(this);
                    //GridPrincipal.Children.Add(menus);
                    menus.Width = childWindow.Width;
                    menus.Height = childWindow.Height;

                    childWindow.Content = menus;
                    child = menus;
                    break;
                case 2:
                    childWindow.Content = null;
                    orders = new Orders(this);
                    orders.Width = childWindow.Width;
                    orders.Height = childWindow.Height;
                    
                    childWindow.Content = orders;
                    child = orders;
                    break;
                case 3:
                    childWindow.Content = null;
                    assignDelivery = new AssignDelivery(this);
                    assignDelivery.Width = childWindow.Width;
                    assignDelivery.Height = childWindow.Height;

                    childWindow.Content = assignDelivery;
                    child = assignDelivery;
                    break;
                case 5:
                    windowClosing();
                    break;
                default:
                    break;
            }
        }
        private void exit_Click(object sender, MouseEventArgs e)
        {
            windowClosing();
        }
        private void windowClosing()
        {
            string message = "Biztosan kiszeretne lépni?";
            string caption = "Kilépés";
            var result = MessageBox.Show(message, caption,
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                parent.Show();
                this.Close();
            }
        }
        private void window_sizeChanged(object sender, SizeChangedEventArgs e)
        {

            if (child != null)
            {
                //child.Width = childWindow.Width;
                //child.Height = childWindow.Height;
                //Console.WriteLine("child. " + child.Width + ", " + child.Height);
                //Console.WriteLine("GridPrincipal. " + childWindow.Width + ", " + childWindow.Height);
            }
        }

        private void childWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ContentControl window = sender as ContentControl;

            if (child != null)
            {
                child.Width = window.Width;
                child.Height = window.Height - 50;
                Console.WriteLine("child. " + child.Width + ", " + child.Height);
                Console.WriteLine("GridPrincipal. " + e.NewSize.Width + ", " + e.NewSize.Height);
            }
        }

        private void orders_Click(object sender, EventArgs e)
        {
            Console.WriteLine("cliiiick");
            if(LV.SelectedIndex == 2)
            {
                refreshRequested(orders);
            }
        }

        public void refreshRequested(UserControl child)
        {
            childWindow.Content = null;
            orders = new Orders(this);
            orders.Width = childWindow.Width;
            orders.Height = childWindow.Height;

            childWindow.Content = orders;
            child = orders;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            new Thread(refreshData).Start();
        }
        int z = 0;
        public delegate void UpdateTextCallback(string message);
        public void refreshData()
        {
            while(z == 0)
            {
                Thread.Sleep(10000);
                //ordersList = serverConnection.getOrders(currUser.restaurantID); TODO
                for (int i = 1; i < ordersList.Count; ++i)
                {
                    if(ordersList[i].OrderStatus == 0 && !(checkedNewOrders.Contains(ordersList[i])))
                    {
                        Console.WriteLine("hozzáadás");
                        newOrders.Add(ordersList[i]);
                    }
                }
                if(newOrders.Count != 0)
                {
                    // alert the restaurant that there are not checked new orders
                    Console.WriteLine("Alert");
                    //newOrderCount.Text = newOrders.Count.ToString();
                    //newOrderCount.Visibility = Visibility.Visible;
                    newOrderCount.Dispatcher.Invoke(
                        new UpdateTextCallback(this.UpdateText),
                        new object[] { newOrders.Count.ToString() });
                }
                newOrders.Clear();
                Console.WriteLine("10 mp eltelt");
            }
        }
        private void UpdateText(string message)
        {
            newOrderCount.Text = message;
            newOrderCount.Visibility = Visibility.Visible;
        }
    }
}
