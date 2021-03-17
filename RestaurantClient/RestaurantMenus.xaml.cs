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
    /// Interaction logic for RestaurantMenus.xaml
    /// </summary>
    public partial class RestaurantMenus : UserControl
    {
        //    List<String> categoryNames = new List<String>();
        RestaurantMain restaurantMain;
        List<newMenu> newMenus = new List<newMenu>();
    
        Dictionary<int, String> categoryNames = new Dictionary<int, string>(); // map[id] = category name
        Dictionary<int, Image> imgNames = new Dictionary<int, Image>(); //        map[id] = Image
        Dictionary<StackPanel, int> categPanels = new Dictionary<StackPanel, int>();
        int categoryId = 0;
        newMenu newMen;
        public RestaurantMenus(Window parent)
        {
            InitializeComponent();
            restaurantMain = (RestaurantMain)parent;
            // get latest categoryId
            // TODO: fill categoryNames with existing categories from Database
        }

        private void NewMenu_Click(object sender, EventArgs e)
        {
            newMen = new newMenu();
            newMen.Show();
            newMen.Closed += NewMen_Closed;
        }

        private void NewMen_Closed(object sender, EventArgs e)
        {
            newMenu newMen = sender as newMenu;
            if (newMen.IsSaved)
            {
                newMenus.Add(newMen);
                //   categoryNames[categoryId] = newMen.CategoryName;

                //        newMenus2.Add(new newMenuItem(newMen.CategoryName));
                addCategoryPanel();           
            }
        }

        private void addCategoryPanel()
        {
            Console.WriteLine("addcateg");
            StackPanel newCategory = new StackPanel();
            newCategory.Orientation = Orientation.Vertical;
            newCategory.HorizontalAlignment = HorizontalAlignment.Center;
            newCategory.Width = 200;
            newCategory.Height = 128;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(50, 255, 204, 153);
            newCategory.Background = color;

            Border border = new Border();
            border.BorderBrush = new SolidColorBrush(Colors.AliceBlue);
            border.BorderThickness = new Thickness(1);

            TextBlock newText = new TextBlock();
            newText.VerticalAlignment = VerticalAlignment.Top;
            newText.HorizontalAlignment = HorizontalAlignment.Center;
            newText.TextAlignment = TextAlignment.Center;
            newText.Width = 200;
            newText.Height = 50;
            newText.FontFamily = new FontFamily("Century");
            newText.FontSize = 16;
            newText.TextWrapping = TextWrapping.Wrap;
            newText.Padding = new Thickness(2, 10, 2, 0);
            newText.Text = newMen.CategoryName;
        
            Image img = new Image();
            //img.Source = new BitmapImage(new Uri("pack://application:,,,/RestaurantClient;component/Assets/img_setting.png")); // or
            img.Source = new BitmapImage(new Uri("Assets/menu.png", UriKind.Relative));
            img.Stretch = Stretch.None;
            img.Margin = new Thickness(20, 5, 20, 0);
            img.Height = 70;
            img.MouseDown += categImg_MouseDown;
        //    img.Name = (categoryId - 1).ToString() + " img";
            imgNames[categoryId] = img;
            border.Child = newText;
            
            newCategory.Children.Add(border);
            newCategory.Children.Add(img);
            categPanels[newCategory] = categoryId;
            menuList.Children.Add(newCategory);

            if (categoryId != 0)
            {
                categoryId++;
            }
        }
        /* Click event on the category panel */
        private void categImg_MouseDown(object sender, RoutedEventArgs e)        
        {
            StackPanel categ = sender as StackPanel;
            int categID = 0;
            //categID = categPanels[categ]; // TODO
       //     Int32.Parse(img.Name)
            RestaurantCategFoods categFood = new RestaurantCategFoods(categID);
            restaurantMain.childWindow.Content = null;
           
            /* Set the content window to new child */
            restaurantMain.childWindow.Content = categFood;
            restaurantMain.child = categFood;
            restaurantMain.LV.SelectedIndex = -1;
        }
    }
}
