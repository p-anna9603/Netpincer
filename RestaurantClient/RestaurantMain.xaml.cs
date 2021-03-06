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
        ManageDiscount manageDiscount;
        Summary summary;
        public UserControl child;
        private ConnectToServer serverConnection;
        Restaurant currUser;
        startupWindow parent;
        List<Order> newOrders = new List<Order>();
        List<int> newOrdersID = new List<int>();
        OrderList ordersList = new OrderList();
        List<Order> checkedNewOrders = new List<Order>();
        List<int> checkedNewOrdersID = new List<int>();

        public ConnectToServer ServerConnection { get => serverConnection; set => serverConnection = value; }
        public Restaurant CurrUser { get => currUser; set => currUser = value; }
        internal List<Order> CheckedNewOrders { get => checkedNewOrders; set => checkedNewOrders = value; }
        internal List<int> CheckedNewOrdersID { get => checkedNewOrdersID; set => checkedNewOrdersID = value; }
        public List<int> NewOrdersID { get => newOrdersID; set => newOrdersID = value; }

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
            userNameTextBox.Text = "Felhaszn??l??: " + currUser.owner;
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

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e) // men?? ??sszecsuk??sa
        {
            img_bg.Opacity = 1;
            childWindow.Opacity = 1;
            Console.WriteLine("tgbutn unchecked");
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e) // men?? megnyit??sa
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
                case 4:
                    childWindow.Content = null;
                    manageDiscount = new ManageDiscount(this);
                    manageDiscount.Width = childWindow.Width;
                    manageDiscount.Height = childWindow.Height;

                    childWindow.Content = manageDiscount;
                    child = manageDiscount;
                    break;
                case 5:
                    childWindow.Content = null;
                    summary = new Summary(this);
                    summary.Width = childWindow.Width;
                    summary.Height = childWindow.Height;

                    childWindow.Content = summary;
                    child = summary;
                    break;
                case 6:
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
            string message = "Biztosan kiszeretne l??pni?";
            string caption = "Kil??p??s";
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
            if(child.GetType() == typeof(Orders))
            {
                Console.WriteLine("equaaaal");
                orders = new Orders(this);
                orders.Width = childWindow.Width;
                orders.Height = childWindow.Height;

                childWindow.Content = orders;
                child = orders;
            }
            else if (child.GetType() == typeof(ManageDiscount))
            {
                Console.WriteLine("equaaaal 2");
                manageDiscount = new ManageDiscount(this);
                manageDiscount.Width = childWindow.Width;
                manageDiscount.Height = childWindow.Height;

                childWindow.Content = manageDiscount;
                child = manageDiscount;
            }
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
                Thread.Sleep(30000);
                ordersList = serverConnection.getOrders(currUser.restaurantID);
                if(ordersList.ListOrder == null)
                {
                    return;
                }
                for (int i = 0; i < ordersList.ListOrder.Count; ++i)
                {
                    if(ordersList.ListOrder[i].OrderStatus == 0 && !(checkedNewOrdersID.Contains(ordersList.ListOrder[i].OrderID)))
                    {
                        Console.WriteLine("hozz??ad??s");
                        //newOrders.Add(ordersList[i]);
                        NewOrdersID.Add(ordersList.ListOrder[i].OrderID);
                    }
                }
                if(NewOrdersID.Count != 0)
                {
                    // alert the restaurant that there are not checked new orders
                    Console.WriteLine("Alert");
                    newOrderCount.Dispatcher.Invoke(
                        new UpdateTextCallback(this.UpdateText),
                        new object[] { NewOrdersID.Count.ToString() });
                }
                newOrders.Clear();
                NewOrdersID.Clear();
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
