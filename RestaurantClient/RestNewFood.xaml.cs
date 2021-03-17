using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for RestNewFood.xaml
    /// </summary>
    public partial class RestNewFood : Window
    {
        Regex regex = new Regex("[^0-9]+");
        int foodID;

        string foodName;
        double foodPrice;
        double rating = 0;
        int pictureID = 0; // TODO get the last id
        int allergenID = 0;

        Byte[] ImageByteArray;
        
        string imgFilePath;
        List<String> allergenes = new List<String>();

        private bool isSaved = false;

        public bool IsSaved { get => isSaved; set => isSaved = value; }
        public double FoodPrice { get => foodPrice; set => foodPrice = value; }
        public string FoodName { get => foodName; set => foodName = value; }

        public RestNewFood(int foodid)
        {
            InitializeComponent();
            foodID = foodid;
        }

        private void selectedAllergene(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)combo.SelectedItem;
            Console.WriteLine(item.Content.ToString());
            allergeneListBox.Items.Add(item.Content.ToString());
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if(textBoxName.Text.Length == 0)
            {
                errorMessage.Text = "Adja meg az étel nevét!";
            }
            else if(textBoxPrice.Text.Length == 0)
            {
                errorMessage.Text = "Adja meg az étel árát!";
            }
            else
            {
                foodName = textBoxName.Text;
                foodPrice = Double.Parse(textBoxPrice.Text);
                setAllergens();
                Food food = new Food(foodID, foodName, foodPrice, rating, pictureID);
                //TODO add new food to database
                savePictureToDatabase();
                IsSaved = true;
                this.Close();
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if(allergeneListBox.Items.Count == 0)
            {
                return;
            }
            else if(allergeneListBox.SelectedIndex >= 0)
            {
                int toDelete = allergeneListBox.SelectedIndex;
                allergeneListBox.Items.RemoveAt(toDelete);
            }
        }

        private void priceKeyDown(object sender, TextCompositionEventArgs e)
        {
            TextBox ad = sender as TextBox;
            e.Handled = regex.IsMatch(e.Text);
        }

        void windowClosing(object sender, CancelEventArgs e)
        {
            if (IsSaved != true)
            {
                string msg = "Data is not saved. Close without saving?";
                MessageBoxResult result =
                    MessageBox.Show(
                    msg,
                    "Closing",
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

        public void setAllergens()
        {
            string allergName;
            foreach(ListBoxItem i in allergeneListBox.Items)
            {
                allergName = i.Content.ToString();
                //TODO get allergen id from database
                // Upload foodID to database to the given allergenID
               //  Allergen al = new Allergen(allergenID, allergName, foodID);
            }
        }

        private void imgUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imgFood.Stretch = Stretch.Fill;
                ImageSource imgSrc = new BitmapImage(new Uri(op.FileName));
                imgFood.Source = imgSrc;
                imgFilePath = op.FileName;
            }
        }
        private void savePictureToDatabase()
        {
            //TODO
            pictureID++;
        }
    }
}
