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
        string categoryName = "Kategória";
        int categID;
        int foodID;
        List<Food> foods = new List<Food>();
        Dictionary<int, String> foodNames = new Dictionary<int, string>(); // map[foodId] = food name
        Dictionary<int, Image> imgNames = new Dictionary<int, Image>(); //        map[id] = Image
        Dictionary<StackPanel, int> foodPanels = new Dictionary<StackPanel, int>();
        public RestaurantCategFoods(int cID)
        {
            InitializeComponent();
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(120, 102, 102, 255);
            scrollView.Background = color;
            categID = cID;
            Console.WriteLine("category: " + categID);
            //TODO get category name from db by id categoryName = categNameFromDb
            categName.Text = categoryName;
            //TODO create new Food from existing ones from db
                    // Food f = new Food(..); -->  foods.Add(f);
            // fill imgNames
            //get last foodID
            addExistingFoods();
        }

        /* Adding new food to the list */
        private void NewFood_Click(object sender, EventArgs e)
        {
            Console.WriteLine("új hozzáadása");
            RestNewFood newFood = new RestNewFood();
            newFood.ShowDialog();
            newFood.Closed += NewFood_Closed;
            Console.WriteLine("új végén");
            if (newFood.IsSaved)
            {
                //newMenus.Add(newMen);
                //categoryNames[categoryId] = newMen.CategoryName;
                //categoryId++;

                addFoodPanel(newFood.FoodName, newFood.FoodID, newFood.FoodPrice);
            }
        }

        private void NewFood_Closed(object sender, EventArgs e)
        {
            RestNewFood newFood = sender as RestNewFood;
            Console.WriteLine("new food closed");
            if (newFood.IsSaved)
            {
                //newMenus.Add(newMen);
                //categoryNames[categoryId] = newMen.CategoryName;
                //categoryId++;
     
                addFoodPanel(newFood.FoodName, newFood.FoodID, newFood.FoodPrice);
            }
        }

        private void addExistingFoods()
        {
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

            //Border border = new Border();
            //border.BorderBrush = new SolidColorBrush(Colors.AliceBlue);
            //border.BorderThickness = new Thickness(1);

            StackPanel panel2 = new StackPanel();
            panel2.Orientation = Orientation.Vertical;
            panel2.HorizontalAlignment = HorizontalAlignment.Center;
            panel2.Width = 263;

            /* Name of the food */
            TextBlock newText = new TextBlock();
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
            //newCategory.Children.Add(img);
            foodPanels[newCategory] = foodId;
            FoodList.Children.Add(newCategory);
        }
        /* Pop up info about the food when mouse clicks on it */
        private void foodPanel_MouseDown(object sender, RoutedEventArgs e)
        {
          //  StackPanel fe2 = (StackPanel)e.Source;
            StackPanel categ = sender as StackPanel;
            int foodId = 0;
            Console.WriteLine("kattintott: " + categ + " vs " + foodPanels.ElementAt(0));
            foodId = foodPanels[categ]; // TODO
          //  RestaurantCategFoods categFood = new RestaurantCategFoods(categID);
        }

    }
}
