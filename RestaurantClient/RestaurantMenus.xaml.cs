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
using RestaurantClient;

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

        List<Category> categories = new List<Category>();
        Dictionary<int, Category> categoryWindows = new Dictionary<int, Category>();

        Dictionary<int, String> categoryNames = new Dictionary<int, string>(); // map[categoryid] = category name
        Dictionary<int, Image> imgNames = new Dictionary<int, Image>(); //        map[categoryid] = Image
        Dictionary<StackPanel, int> categPanels = new Dictionary<StackPanel, int>();
        int categoryId = 0;
        newMenu newMen;
        public ConnectToServer ServerConnection;

        Categories cat;
        public RestaurantMenus(Window parent)
        {
            InitializeComponent();
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(120, 102, 102, 255);
            scrollView.Background = color;
            restaurantMain = (RestaurantMain)parent;
            cat = restaurantMain.ServerConnection.getCategories(restaurantMain.CurrUser.restaurantID);
            // get latest categoryId
            // TODO: fill categoryNames with existing categories from Database
            // TODO: fill imgNames with existing images according to the categoryID from db
                // Category c = new Category(...) --> categories.Add(c);
                // categoryWindows[categoryID] = c;
            addExistingCategories();
        }

        private void NewMenu_Click(object sender, EventArgs e)
        {
            newMen = new newMenu(restaurantMain);
            newMen.ShowDialog();
            newMen.Closed += NewMen_Closed;
            if (newMen.IsSaved)
            {
                newMenus.Add(newMen);
                categoryNames[newMen.CategoryID] = newMen.CategoryName;
                categories.Add(newMen.Category);
                categoryWindows[newMen.CategoryID] = newMen.Category;
                addCategoryPanel(newMen.CategoryName, newMen.CategoryID);
            }
        }

        private void addExistingCategories()
        {
            for(int i = 0; i < cat.ListOfCategoryIDs.Count; ++i)
            {
                categoryNames.Add(Int32.Parse(cat.ListOfCategoryIDs[i]), cat.ListOfCategoryNames[i]);
                Category categ = new Category(Int32.Parse(cat.ListOfCategoryIDs[i]), cat.ListOfCategoryNames[i], "");
                categoryWindows.Add(Int32.Parse(cat.ListOfCategoryIDs[i]), categ);
            }
            if(categoryNames.Count != 0)
            {
                foreach(KeyValuePair<int, string> i in categoryNames)
                {
                    addCategoryPanel(i.Value, i.Key);
                }
            }
        }
        private void NewMen_Closed(object sender, EventArgs e)
        {
            newMenu newMen = sender as newMenu;
            if (newMen.IsSaved)
            {
                newMenus.Add(newMen);
                categoryNames[categoryId] = newMen.CategoryName;

                //        newMenus2.Add(new newMenuItem(newMen.CategoryName));
                addCategoryPanel(newMen.CategoryName, newMen.CategoryID);           
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
            if (e.Source != settingButton)
            {
                RestaurantCategFoods categFood = new RestaurantCategFoods(restaurantMain, categID, categoryNames[categID]);
                restaurantMain.childWindow.Content = null;

                /* Set the content window to new child */
                restaurantMain.childWindow.Content = categFood;
                restaurantMain.child = categFood;
                restaurantMain.LV.SelectedIndex = -1;
            }
        }

        private void menuSetting_Click(object sender, EventArgs e)
        {
            Console.WriteLine("only settiiiings");
            Button btn = sender as Button;
            StackPanel p1 = (StackPanel)btn.Parent;
            StackPanel p2 = (StackPanel)p1.Parent;
            int categID = 0;
            categID = categPanels[p2];
            clickedCategID = categID;
            Console.WriteLine("clicked onL " + clickedCategID);
            Console.WriteLine("categID onL " + categID);
            Category categ = categoryWindows[clickedCategID];
            newMenu m = new newMenu(restaurantMain, categ);
            m.ShowDialog();
            if (m.IsSaved)
            {
                categ.CategName = m.CategoryName;
                categ.CategImg = m.CategoryImg;
                EnumVisual(this, p2, categ);
            }
        }

        static public void EnumVisual(Visual myVisual, StackPanel panel, Category c)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
            {
                // Retrieve child visual at specified index value.
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);

                // Do processing of the child visual object.
                if (childVisual.GetType() == typeof(TextBlock) && childVisual.IsDescendantOf(panel))
                {
                    Console.WriteLine("3 to change");
                    TextBlock t = (TextBlock)childVisual;
                    if (t.Name == "categName")
                    {
                        t.Text = c.CategName;
                        Console.WriteLine(c.CategName);
                    }
                }
                // TODO picture
                // Enumerate children of the child visual object.
                EnumVisual(childVisual, panel, c);
            }
        }
    }
}
