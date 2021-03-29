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
    /// Interaction logic for UserRestCategs.xaml
    /// </summary>
    public partial class UserRestCategs : UserControl
    {
        UserMain parent;
        Restaurant restaurant;

        Categories cat;
        List<Category> categories = new List<Category>();
        Dictionary<int, Category> categoryWindows = new Dictionary<int, Category>();

        Dictionary<int, String> categoryNames = new Dictionary<int, string>(); // map[categoryid] = category name
        Dictionary<int, Image> imgNames = new Dictionary<int, Image>(); //        map[categoryid] = Image
        Dictionary<StackPanel, int> categPanels = new Dictionary<StackPanel, int>();
        int categoryId = 0;
        newMenu newMen;

        public UserRestCategs(UserMain par, Restaurant rest)
        {
            InitializeComponent();
            parent = par;
            restaurant = rest;
            cat = parent.ServerConnection.getCategories(restaurant.restaurantID);
            addCategories();
            restNameText.Text = restaurant.name;
        }

        private void addCategories()
        {
            if (cat != null)
            {
                for (int i = 0; i < cat.ListOfCategoryIDs.Count; ++i)
                {
                    categoryNames.Add(Int32.Parse(cat.ListOfCategoryIDs[i]), cat.ListOfCategoryNames[i]);
                    Category categ = new Category(Int32.Parse(cat.ListOfCategoryIDs[i]), cat.ListOfCategoryNames[i], "");
                    categoryWindows.Add(Int32.Parse(cat.ListOfCategoryIDs[i]), categ);
                }
                if (categoryNames.Count != 0)
                {
                    foreach (KeyValuePair<int, string> i in categoryNames)
                    {
                        addCategoryPanel(i.Value, i.Key);
                    }
                }
            }
        }
        private void addCategoryPanel(string categName, int categID)
        {
            Console.WriteLine("addcateg");
            StackPanel newCategory = new StackPanel();
            newCategory.Orientation = Orientation.Vertical;
            newCategory.HorizontalAlignment = HorizontalAlignment.Center;
            newCategory.Width = 200;
            newCategory.Height = 132;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(50, 255, 204, 153);
            newCategory.Background = color;
            newCategory.Margin = new Thickness(5, 5, 0, 0);

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
            newText.Text = categName;

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
            imgNames[categID] = img;
        
            panel2.Children.Add(img);

            border.Child = newText;

            newCategory.MouseDown += categImg_MouseDown;
            newCategory.Children.Add(border);
            newCategory.Children.Add(panel2);
            //    newCategory.Children.Add(img);
            newCategory.MouseEnter += NewCategory_MouseEnter;
            newCategory.MouseLeave += NewCategory_MouseLeave;
            categPanels[newCategory] = categID;
            menuList.Children.Add(newCategory);
        }

        private void NewCategory_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(50, 255, 204, 153);
            panel.Background = color;
        }

        private void NewCategory_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(100, 255, 204, 153);
            panel.Background = color;
        }

        int clickedCategID;
        StackPanel clickedStackPanel;
        /* Click event on the category panel */
        private void categImg_MouseDown(object sender, RoutedEventArgs e)        // List the foods that the category contains 
        {
            StackPanel stackP = sender as StackPanel;
            int categID = 0;
            categID = categPanels[stackP];
            clickedCategID = categID;
            clickedStackPanel = stackP;
            Console.WriteLine("clicked onL " + clickedCategID);
            Console.WriteLine("categID onL " + categID);
            //if (e.Source != settingBtn)
            //{
            UserRestCategFoods categFood = new UserRestCategFoods(parent, categID, categoryNames[categID], restaurant);
            parent.childWindow.Content = null;

            ///* Set the content window to new child */
            parent.childWindow.Content = categFood;
            parent.child = categFood;
            parent.LV.SelectedIndex = -1;
            //}
        }
    }
}
