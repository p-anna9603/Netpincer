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
    /// Interaction logic for UserRestCategFoods.xaml
    /// </summary>
    public partial class UserRestCategFoods : UserControl
    {
        string categoryName = "Kategória";
        int categID;
        int foodID;
        int picID;
        //Dictionary<int, RestNewFood> newFoodWindows = new Dictionary<int, RestNewFood>(); // foodID - RestNewFood window
        Dictionary<int, Food> newFoodWindows = new Dictionary<int, Food>(); // foodID - Food

        FoodList listFromServer;
        List<Food> foods = new List<Food>();
        Dictionary<int, String> foodNames = new Dictionary<int, string>(); // map[foodId] = food name
        Dictionary<int, Image> imgNames = new Dictionary<int, Image>(); //    map[foodId] = Image
        Dictionary<StackPanel, int> foodPanels = new Dictionary<StackPanel, int>(); // stackpanel - foodID
        UserMain parent;
        Restaurant rest;

        public ConnectToServer ServerConnection;
        public UserRestCategFoods(UserMain usrMain, int cID, string cName, Restaurant res)
        {
            InitializeComponent();
            categID = cID;
            parent = (UserMain)usrMain;
            rest = res;
            /* Get the list of foods from server */
            listFromServer = parent.ServerConnection.getFoods(rest.restaurantID, categID);

            categoryName = cName;
            categName.Text = categoryName;
            restNameText.Text = rest.name;
            //TODO create new Food from existing ones from db
            // Food f = new Food(..); -->  foods.Add(f);
            // newFoodWindows[foodID] = f;
            // fill imgNames
            //get last foodID
            addExistingFoods();
        }
        private void addExistingFoods()
        {
            if (listFromServer.ListFood != null)
            {
                for (int i = 0; i < listFromServer.ListFood.Count; ++i)
                {
                    foods.Add(listFromServer.ListFood[i]);
                    newFoodWindows.Add(listFromServer.ListFood[i].FoodID, listFromServer.ListFood[i]);
                }
            }
            if (foods.Count != 0)
            {
                foreach (Food i in foods)
                {
                    addFoodPanel(i.Name, i.FoodID, i.Price);
                }
            }
        }
        private void addFoodPanel(string foodName, int foodId, double foodPrice)
        {
            Console.WriteLine("addfood");
            StackPanel newCategory = new StackPanel();
            newCategory.Orientation = Orientation.Horizontal;
            newCategory.HorizontalAlignment = HorizontalAlignment.Center;
            newCategory.Width = 330;
            newCategory.Height = 102;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(50, 255, 204, 153);
            newCategory.Background = color;
            newCategory.Margin = new Thickness(5, 5, 0, 0);

            Border border = new Border();
            //border.BorderBrush = new SolidColorBrush(Colors.AliceBlue);
            border.BorderThickness = new Thickness(1);
            border.HorizontalAlignment = HorizontalAlignment.Left;
            border.VerticalAlignment = VerticalAlignment.Top;

            DataTrigger dataTrigger = new DataTrigger();
            Binding binding = new Binding();
            binding.Path = new PropertyPath("IsMouseOver");
            //binding.Source = food;
            //     binding.ElementName = "food";
            dataTrigger.Binding = binding;


            dataTrigger.Value = true;
            Style style = new Style(typeof(Border));
            SolidColorBrush cs = new SolidColorBrush();
            cs.Color = Color.FromRgb(255, 255, 255);
            style.Setters.Add(new Setter(BorderBrushProperty, cs));
            border.Style = style;


            StackPanel panel2 = new StackPanel();
            panel2.Orientation = Orientation.Vertical;
            panel2.HorizontalAlignment = HorizontalAlignment.Center;
            panel2.Width = 263;

            /* Name of the food */
            TextBlock newText = new TextBlock();
            newText.Name = "foodName";
            newText.VerticalAlignment = VerticalAlignment.Top;
            newText.HorizontalAlignment = HorizontalAlignment.Left;
            newText.TextAlignment = TextAlignment.Left;
            newText.Width = 272;
            newText.Height = 56;
            newText.FontFamily = new FontFamily("Century");
            newText.FontSize = 16;
            newText.TextWrapping = TextWrapping.Wrap;
            newText.Padding = new Thickness(10, 10, 2, 0);
            newText.Margin = new Thickness(0, 0, 0, 0);
            newText.Text = foodName;
            newText.TextWrapping = TextWrapping.Wrap;

            /* Price of the food */
            TextBlock newText2 = new TextBlock();
            newText2.Name = "foodPrice";
            newText2.VerticalAlignment = VerticalAlignment.Top;
            newText2.HorizontalAlignment = HorizontalAlignment.Left;
            newText2.TextAlignment = TextAlignment.Left;
            newText2.Width = 272;
            newText2.Height = 56;
            newText2.FontFamily = new FontFamily("Century");
            newText2.FontSize = 16;
            newText2.TextWrapping = TextWrapping.Wrap;
            newText2.Padding = new Thickness(10, 10, 2, 0);
            newText2.Margin = new Thickness(0, 0, 0, 0);
            newText2.Text = foodPrice.ToString() + " Ft";
            newText2.TextWrapping = TextWrapping.Wrap;

            panel2.Children.Add(newText);
            panel2.Children.Add(newText2);
            /* Stack to hold the picture */
            StackPanel panel3 = new StackPanel();
            panel3.Orientation = Orientation.Horizontal;
            panel3.HorizontalAlignment = HorizontalAlignment.Right;
            panel3.Width = 76;

            /* Picture of the food */
            Image img = new Image();
            img.Name = "foodImg";
            //img.Source = new BitmapImage(new Uri("pack://application:,,,/RestaurantClient;component/Assets/img_setting.png")); // or
            img.Source = new BitmapImage(new Uri("Assets/meal.png", UriKind.Relative)); // TODO picture
            img.Stretch = Stretch.Fill;
            img.HorizontalAlignment = HorizontalAlignment.Center;
            img.VerticalAlignment = VerticalAlignment.Center;
            img.Width = 76;
            img.Height = 66;
            img.Margin = new Thickness(0, 18, 0, 0);

            imgNames[foodId] = img;

            panel3.Children.Add(img);
            newCategory.Children.Add(panel2);
            newCategory.Children.Add(panel3);
            newCategory.MouseDown += foodPanel_MouseDown;
            //      newCategory.Style = (Style)Application.Current.Resources["bckgStyle"]; 66 does not work
            newCategory.MouseEnter += StackPanel_MouseEnter;
            newCategory.MouseLeave += StackPanel_MouseLeave;
            //newCategory.Children.Add(img);
            foodPanels[newCategory] = foodId;
            FoodList.Children.Add(newCategory);
        }
        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            //Color A = "50" R = "255" G = "204" B = "153" ></ Color >
            StackPanel panel = sender as StackPanel;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(100, 255, 204, 153);
            panel.Background = color;

        }
        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(50, 255, 204, 153);
            panel.Background = color;
        }
        /* Pop up info about the food when mouse clicks on it - modification */
        private void foodPanel_MouseDown(object sender, RoutedEventArgs e)
        {
            StackPanel categ = sender as StackPanel;
            int foodId = 0; // TODO
            foodId = foodPanels[categ];
            Console.WriteLine("error 2");
            Food food = newFoodWindows[foodId];

            //RestNewFood f = new RestNewFood(restaurantMain, this, food);
            //f.ShowDialog();
            //Console.WriteLine("modositas utan");
            //if (f.IsSaved)
            //{
            //    food.Name = f.FoodName;
            //    food.Price = f.FoodPrice;
            //    //food.PictureID = ;
            //    // food availability
            //    EnumVisual(this, categ, food);
            //}
        }
    }
}
