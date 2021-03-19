using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace RestaurantClient
{
    /// <summary>
    /// Interaction logic for newMenu.xaml
    /// </summary>
    public partial class newMenu : Window
    {
        Category category;
        int modifyingWindow = 0;
        private String categoryName;
        int categoryID = 0;
        string categoryImg = "";
        private bool isSaved = false;
        public newMenu(Category c = null)
        {
            InitializeComponent();
            if(c != null)
            {
                category = c;
                setElementsData();
                modifyingWindow = 1;
            }
            
        }
        private void setElementsData()
        {
            categoryNameTextBox.Text = category.CategName;
            //textBoxPrice.Text = food.Price.ToString();
            //TODO set img
            Submit.Content = "MÓDOSÍTÁS";
            mainPanel.IsEnabled = false;
        }

        public bool IsSaved { get => isSaved; set => isSaved = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
        public Category Category { get => category; set => category = value; }
        public string CategoryImg { get => categoryImg; set => categoryImg = value; }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (modifyingWindow == 1 && mainPanel.IsEnabled == false)
            {
                mainPanel.IsEnabled = true;
                Submit.Content = "MENTÉS";
            }
            else if (categoryNameTextBox.Text.Length == 0)
            {
                errorText.Text = "Adja meg a kategória nevét";
            }
            else
            {
                categoryName = categoryNameTextBox.Text;
                if (modifyingWindow == 1)
                {
                    // TODO modify categ / picture in database (with just the changed values)
                }
                else
                {
                    category = new Category(CategoryID, categoryName, categoryImg);
                    //TODO upload to DB the new category (and pic)
                    // and get the latest categoryID

                }
                IsSaved = true;
                this.Close();
            }
        }
        private void imgUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Válasszon képet";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgFood.Stretch = Stretch.Fill;
                ImageSource imgSrc = new BitmapImage(new Uri(op.FileName));
                imgFood.Source = imgSrc;
             //   imgFilePath = op.FileName;
            }
        }

        void windowClosing(object sender, CancelEventArgs e)
        {
            if (IsSaved != true)
            {
                string msg = "Bezárja mentés nélkül?";
                MessageBoxResult result =
                    MessageBox.Show(
                    msg,
                    "Bezárás",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    // If user doesn't want to close, cancel closure
                    e.Cancel = true;
                }
                else
                {
                    IsSaved = false;
                    e.Cancel = false;
                }
            }
        }
    }
}
