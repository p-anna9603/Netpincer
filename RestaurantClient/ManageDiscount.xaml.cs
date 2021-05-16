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
    /// Interaction logic for ManageDiscount.xaml
    /// </summary>
    public partial class ManageDiscount : UserControl
    {
        RestaurantMain restaurantMain;
        /* Categories */
        Categories cat;
        List<Category> categories = new List<Category>();
        Dictionary<int, Category> categoryWindows = new Dictionary<int, Category>();
        Dictionary<int, String> categoryNames = new Dictionary<int, string>(); // map[categoryid] = category name
        Dictionary<int, Image> imgNames = new Dictionary<int, Image>(); //        map[categoryid] = Image
        Dictionary<StackPanel, int> categPanels = new Dictionary<StackPanel, int>();

        /* Foods */
        Dictionary<int, Food> newFoodWindows = new Dictionary<int, Food>(); // foodID - Food
        FoodList listFromServer;
        List<Food> foods = new List<Food>();
        Dictionary<int, String> foodNames = new Dictionary<int, string>(); // map[foodId] = food name
        Dictionary<StackPanel, int> foodPanels = new Dictionary<StackPanel, int>(); // stackpanel - foodID

        public ManageDiscount(Window restrantMain)
        {
            InitializeComponent();
            restaurantMain = (RestaurantMain)restrantMain;
            cat = restaurantMain.ServerConnection.getCategories(restaurantMain.CurrUser.restaurantID);
            addExistingCategories();

            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(50, 204, 0, 102);
            categScroll.Background = color;
            foodScrollView.Background = color;
        }

        private void addExistingCategories()
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

            /* Setting button */
            Button settingBtn = new Button();
            settingBtn.Name = "settingButton";
            settingBtn.Height = 20;
            settingBtn.Width = 20;
            settingBtn.HorizontalAlignment = HorizontalAlignment.Right;
            settingBtn.VerticalAlignment = VerticalAlignment.Bottom;
            settingBtn.Background = Brushes.Transparent;
            settingBtn.BorderBrush = Brushes.DarkBlue;
            settingBtn.Click += menuSetting_Click;
            settingBtn.Margin = new Thickness(0, 0, 0, 0);
            ToolTip tTip = new ToolTip();
            tTip.Content = "Módosítás";
            settingBtn.ToolTip = tTip;
            Image setImg = new Image();
            setImg.Name = "settingImg";
            setImg.Source = new BitmapImage(new Uri("Assets/img_setting.png", UriKind.Relative));
            setImg.Stretch = Stretch.Fill;

            settingBtn.Content = setImg;
            panel2.Children.Add(img);
            panel2.Children.Add(settingBtn);

            border.Child = newText;

            newCategory.MouseDown += categ_MouseDown;
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
        private void menuSetting_Click(object sender, EventArgs e)
        {
            Console.WriteLine("only settiiiings");
            Button btn = sender as Button;
            StackPanel p1 = (StackPanel)btn.Parent;
            StackPanel p2 = (StackPanel)p1.Parent;
            int categID = 0;
            categID = categPanels[p2];
            clickedCategID = categID;
            getFoodsForCategory(clickedCategID);
            Console.WriteLine("clicked onL " + clickedCategID);
            Console.WriteLine("categID onL " + categID);
            Category categ = categoryWindows[clickedCategID];
            CategDiscount m = new CategDiscount(restaurantMain, categ, foods);
            m.ShowDialog();
            restaurantMain.refreshRequested(this);
            //if (m.IsSaved)
            //{
            //    categ.CategName = m.CategoryName;
            //    categ.CategImg = m.CategoryImg;
            //    EnumVisual(this, p2, categ);
            //}
            categ_MouseDown(p2, new RoutedEventArgs());
        }
        public void init()
        {
            foodWrapPanel.Children.Clear();
            foodPanels.Clear();
            foods.Clear();
            newFoodWindows.Clear();
        }
        public void getFoodsForCategory(int categoryID)
        {
            init();
            listFromServer = restaurantMain.ServerConnection.getFoods(restaurantMain.CurrUser.restaurantID, categoryID);
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
                    addFoodPanel(i.Name, i.FoodID, i.Price, i.Discount);
                }
            }

        }
        StackPanel clickedStackPanel;
        private void categ_MouseDown(object sender, RoutedEventArgs e)        // List the foods that the category contains 
        {
            StackPanel stackP = sender as StackPanel;
            int categID = 0;
            categID = categPanels[stackP];
            clickedCategID = categID;
            clickedStackPanel = stackP;
            Console.WriteLine("clicked onL " + clickedCategID);
            Console.WriteLine("categID onL " + categID);
            getFoodsForCategory(clickedCategID);
        }

        private void addFoodPanel(string foodName, int foodId, double foodPrice, double? discount)
        {
            Console.WriteLine("addfood");
            int realPrice = 0;
            if (discount != null)
            {
                if (discount >= 1)
                {
                    realPrice = (int)(foodPrice * discount);
                }
                else if (discount < 1)
                {
                    realPrice = (int)(foodPrice * (1 - discount));
                }
            }
            else
            {
                realPrice = (int)foodPrice;
            }
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
            panel2.Width = 260;

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
            newText2.Text = realPrice.ToString() + " Ft";
            newText2.TextWrapping = TextWrapping.Wrap;

            panel2.Children.Add(newText);
            panel2.Children.Add(newText2);

            /* Stack to hold the picture */
            StackPanel panel3 = new StackPanel();
            panel3.Orientation = Orientation.Vertical;
            panel3.HorizontalAlignment = HorizontalAlignment.Right;
            panel3.Width = 65;

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
            img.Margin = new Thickness(0, 8, 0, 0);

            imgNames[foodId] = img;

            /* Setting button */
            Button settingBtn = new Button();
            settingBtn.Name = "settingButton";
            settingBtn.Height = 20;
            settingBtn.Width = 20;
            settingBtn.HorizontalAlignment = HorizontalAlignment.Right;
            settingBtn.VerticalAlignment = VerticalAlignment.Bottom;
            settingBtn.Background = Brushes.Transparent;
            settingBtn.BorderBrush = Brushes.DarkBlue;
            settingBtn.Click += foodSetting_Click;
            settingBtn.Margin = new Thickness(0, 0, 0, 0);
            ToolTip tTip = new ToolTip();
            tTip.Content = "Módosítás";
            settingBtn.ToolTip = tTip;
            Image setImg = new Image();
            setImg.Name = "settingImg";
            setImg.Source = new BitmapImage(new Uri("Assets/img_setting.png", UriKind.Relative));
            setImg.Stretch = Stretch.Fill;
            settingBtn.Content = setImg;

            panel3.Children.Add(img);
            panel3.Children.Add(settingBtn);

            newCategory.Children.Add(panel2);
            newCategory.Children.Add(panel3);
      //      newCategory.MouseDown += foodPanel_MouseDown;
            //      newCategory.Style = (Style)Application.Current.Resources["bckgStyle"]; 66 does not work
            newCategory.MouseEnter += NewCategory_MouseEnter;
            newCategory.MouseLeave += NewCategory_MouseLeave;
            //newCategory.Children.Add(img);
            foodPanels[newCategory] = foodId;
            foodWrapPanel.Children.Add(newCategory);
        }

        private void foodSetting_Click(object sender, EventArgs e)
        {
            Console.WriteLine("only settiiiings");
            Button btn = sender as Button;
            StackPanel p1 = (StackPanel)btn.Parent;
            StackPanel p2 = (StackPanel)p1.Parent;
            int foodId = 0;
            foodId = foodPanels[p2];

            Console.WriteLine("clicked onL " + clickedCategID);
            Food food = newFoodWindows[foodId];

            FoodDiscount m = new FoodDiscount(restaurantMain, food);
            m.ShowDialog();
            restaurantMain.refreshRequested(this);
            //if (m.IsSaved)
            //{
            //    categ.CategName = m.CategoryName;
            //    categ.CategImg = m.CategoryImg;
            //    EnumVisual(this, p2, categ);
            //}

        }
    }
}
