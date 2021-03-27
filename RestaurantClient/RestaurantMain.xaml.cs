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
using RestaurantClient;

namespace FoodOrderClient
{
    /// <summary>
    /// Interaction logic for RestaurantMain.xaml
    /// </summary>
    public partial class RestaurantMain : Window
    {
        RestaurantMenus menus;
        public UserControl child;
        private ConnectToServer serverConnection;

        public ConnectToServer ServerConnection { get => serverConnection; set => serverConnection = value; }

        public RestaurantMain(ConnectToServer ServerCon)
        {
            InitializeComponent();
            Console.WriteLine(settingImg.Source.ToString());
            serverConnection = ServerCon;
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

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e) // menü összecsukása
        {
            img_bg.Opacity = 1;
            Console.WriteLine("tgbutn unchecked");
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e) // menü megnyitása
        {
            img_bg.Opacity = 0.3;
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
                    //menus.Width = GridPrincipal.Width;
                    //menus.Height = GridPrincipal.Height;

                    childWindow.Content = menus;

                    child = menus;
                    break;
                default:
                    break;
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
    }
}
