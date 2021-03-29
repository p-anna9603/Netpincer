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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestaurantClient
{
    /// <summary>
    /// Interaction logic for UserRestaurantList.xaml
    /// </summary>
    public partial class UserRestaurantList : UserControl
    {
        public ConnectToServer ServerConnection;
        UserMain userMain;
        ListOfRestaurants restListFromServer;
        List<Restaurant> restList = new List<Restaurant>();
        Dictionary<StackPanel, int> restPanels = new Dictionary<StackPanel, int>(); // map[panel] = restID

        Dictionary<int, Restaurant> restaurantWindows = new Dictionary<int, Restaurant>(); // map[restID] = Restaurant

        Dictionary<int, String> categoryNames = new Dictionary<int, string>(); // map[categoryid] = category name
        Dictionary<int, Image> imgNames = new Dictionary<int, Image>(); //        map[categoryid] = Image
    
        int categoryId = 0;

        public UserRestaurantList(Window parent)
        {
            InitializeComponent();
            userMain = (UserMain)parent;
            ServerConnection = userMain.ServerConnection;
            restListFromServer = ServerConnection.getRestaurantsList();
            // restList = szerver lista
            addExistingRestaurants();
        }

        public void addExistingRestaurants()
        {
            if (restListFromServer.RestaurantList != null)
            {
                for (int i = 0; i < restListFromServer.RestaurantList.Count; ++i)
                {
                    restList.Add(restListFromServer.RestaurantList[i]);
                    restaurantWindows.Add(restListFromServer.RestaurantList[i].restaurantID, restListFromServer.RestaurantList[i]);
                }
                for (int i = 0; i < restList.Count; ++i)
                {
                    addCategoryPanel(restList[i].name, restList[i].restaurantID);                  
                }
            }
        }
        private void addCategoryPanel(string restName, int restID)
        {
            Console.WriteLine("addcateg");
            StackPanel restaurantPanel = new StackPanel();
            restaurantPanel.Orientation = Orientation.Vertical;
            restaurantPanel.HorizontalAlignment = HorizontalAlignment.Center;
            restaurantPanel.Width = 200;
            restaurantPanel.Height = 132;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(50, 255, 204, 153);
            restaurantPanel.Background = color;
            restaurantPanel.Margin = new Thickness(5, 5, 0, 0);

            Border border = new Border();
            border.BorderBrush = new SolidColorBrush(Colors.AliceBlue);
            border.BorderThickness = new Thickness(1);

            TextBlock newText = new TextBlock();
            newText.Name = "categName";
            newText.VerticalAlignment = VerticalAlignment.Top;
            newText.HorizontalAlignment = HorizontalAlignment.Center;
            newText.TextAlignment = TextAlignment.Center;
            newText.Width = 200;
            newText.Height = 50;
            newText.FontFamily = new FontFamily("Century");
            newText.FontSize = 16;
            newText.TextWrapping = TextWrapping.Wrap;
            newText.Padding = new Thickness(2, 10, 2, 0);
            newText.Text = restName;

            StackPanel panel2 = new StackPanel();
            panel2.Orientation = Orientation.Horizontal;
            panel2.Height = 80;

            /* Category image */
            Image img = new Image();
            //img.Source = new BitmapImage(new Uri("pack://application:,,,/RestaurantClient;component/Assets/img_setting.png")); // or
            img.Source = new BitmapImage(new Uri("Assets/menu.png", UriKind.Relative)); // TODO pic from db
            img.Stretch = Stretch.Fill;
            img.Margin = new Thickness(30, 8, 11, 0);
            img.Height = 70;
            img.Width = 138;
            img.HorizontalAlignment = HorizontalAlignment.Center;
            img.VerticalAlignment = VerticalAlignment.Center;
        //    imgNames[categID] = img;

            panel2.Children.Add(img);
            border.Child = newText;

            restaurantPanel.MouseDown += restImg_MouseDown;
            restaurantPanel.Children.Add(border);
            restaurantPanel.Children.Add(panel2);
            //    restaurantPanel.Children.Add(img);
            restaurantPanel.MouseEnter += restaurant_MouseEnter;
            restaurantPanel.MouseLeave += restaurant_MouseLeave;
            restPanels[restaurantPanel] = restID;
            menuList.Children.Add(restaurantPanel);
        }
        private void restaurant_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(50, 255, 204, 153);
            panel.Background = color;
        }

        private void restaurant_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(100, 255, 204, 153);
            panel.Background = color;
        }

        private void restImg_MouseDown(object sender, RoutedEventArgs e)      // Get restaurant foods?
        {
            StackPanel stackP = sender as StackPanel;
            int restID = 0;
            restID = restPanels[stackP];
            Restaurant rest = restaurantWindows[restID];      
           
            //UserRestCategs restCategs = new UserRestCategs(userMain, rest);

            ///* Set the content window to new child */
            //userMain.childWindow.Content = null;
            //userMain.childWindow.Content = categFood;
            //userMain.child = categFood;
            //userMain.LV.SelectedIndex = -1;
           
        }
    }
}
