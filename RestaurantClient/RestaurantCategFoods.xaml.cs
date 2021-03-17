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
    /// Interaction logic for RestaurantCategFoods.xaml
    /// </summary>
    public partial class RestaurantCategFoods : UserControl
    {
        string categoryName = "";
        int categID;
        int foodID;

        Dictionary<int, String> categoryNames = new Dictionary<int, string>(); // map[foodId] = food name
        Dictionary<int, Image> imgNames = new Dictionary<int, Image>(); //        map[id] = Image
        public RestaurantCategFoods(int cID)
        {
            InitializeComponent();
            categID = cID;
            //TODO get category name from db by id
            categName.Text = categoryName;
            //TODO fill window with the foods of the category from Database
            //get last foodID
        }

        /* Adding new food to the list */
        private void NewFood_Click(object sender, EventArgs e)
        {
            RestNewFood newFood = new RestNewFood(++foodID);
            newFood.ShowDialog();
            newFood.Closed += NewFood_Closed;
        }

        private void NewFood_Closed(object sender, EventArgs e)
        {
            RestNewFood newFood = sender as RestNewFood;
            if (newFood.IsSaved)
            {
                //newMenus.Add(newMen);
                //categoryNames[categoryId] = newMen.CategoryName;
                //categoryId++;
     
                addFoodPanel();
                // TODO addToDatabase();
            }
        }

        private void addFoodPanel()
        {
            Console.WriteLine("addcateg");
            StackPanel newCategory = new StackPanel();
            newCategory.Orientation = Orientation.Horizontal;
            newCategory.HorizontalAlignment = HorizontalAlignment.Center;
            newCategory.Width = 330;
            newCategory.Height = 102;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(50, 255, 204, 153);
            newCategory.Background = color;

            //Border border = new Border();
            //border.BorderBrush = new SolidColorBrush(Colors.AliceBlue);
            //border.BorderThickness = new Thickness(1);

            //StackPanel panel2 = new StackPanel();
            //panel2.Orientation = Orientation.Horizontal;
            //panel2.HorizontalAlignment = HorizontalAlignment.Center;
            //panel2.Width = 330;
            //panel2.Height = 102;
            //SolidColorBrush color = new SolidColorBrush();
            //color.Color = Color.FromArgb(50, 255, 204, 153);
            //panel2.Background = color;

            //TextBlock newText = new TextBlock();
            //newText.VerticalAlignment = VerticalAlignment.Top;
            //newText.HorizontalAlignment = HorizontalAlignment.Center;
            //newText.TextAlignment = TextAlignment.Center;
            //newText.Width = 200;
            //newText.Height = 50;
            //newText.FontFamily = new FontFamily("Century");
            //newText.FontSize = 16;
            //newText.TextWrapping = TextWrapping.Wrap;
            //newText.Padding = new Thickness(2, 10, 2, 0);
            //newText.Text = newMen.CategoryName;

            //Image img = new Image();
            ////img.Source = new BitmapImage(new Uri("pack://application:,,,/RestaurantClient;component/Assets/img_setting.png")); // or
            //img.Source = new BitmapImage(new Uri("Assets/menu.png", UriKind.Relative));
            //img.Stretch = Stretch.None;
            //img.Margin = new Thickness(20, 5, 20, 0);
            //img.Height = 70;
            //img.MouseDown += categImg_MouseDown;
            ////    img.Name = (categoryId - 1).ToString() + " img";
            //imgNames[categoryId] = img;
            //border.Child = newText;

            //newCategory.Children.Add(border);
            //newCategory.Children.Add(img);
            //categPanels[newCategory] = categoryId;
            //menuList.Children.Add(newCategory);
        }


    }
}
