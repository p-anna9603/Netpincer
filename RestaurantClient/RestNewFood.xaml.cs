using FoodOrderClient;
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
        Food food;
        Regex regex = new Regex("[^0-9]+");
        int foodID = 0;
        string foodName;
        double foodPrice;
        double rating = 0;
        int pictureID = 0; // TODO get the last id
        int allergenID = 0;
        int isAlwaysAvailable = 0; // 1 - always, 0 - only in a specific period
        Byte[] ImageByteArray;
        int modifyingWindow = 0; // 0 - for new food, 1 - existing food just modifying
        
        string imgFilePath;
        List<String> allergenes = new List<String>();
        List<String> existingAllergenes;

        private bool isSaved = false;
        private bool isModified = false;
        public bool IsSaved { get => isSaved; set => isSaved = value; }
        public double FoodPrice { get => foodPrice; set => foodPrice = value; }
        public string FoodName { get => foodName; set => foodName = value; }
        public int FoodID { get => foodID; set => foodID = value; }
        public List<string> Allergenes { get => allergenes; set => allergenes = value; }
        internal Food Food { get => food; set => food = value; }
        RestaurantMain restaurantMain;
        public RestNewFood(Window restrantMain, Food f = null)
        {
            InitializeComponent();
            restaurantMain = (RestaurantMain)restrantMain;
            if (f != null) // modifying open
            {
                food = f;
                setElementsData();
                modifyingWindow = 1;
            }
        }
        private void setElementsData()
        {
            textBoxName.Text = food.Name;
            textBoxPrice.Text = food.Price.ToString();
            /* Add allergens */
            existingAllergenes = food.Allergenes;
            if(existingAllergenes != null)
            {
                for(int i = 0; i < existingAllergenes.Count; ++i)
                {
                    allergeneListBox.Items.Add(existingAllergenes[i]);
                    for(int j = 0; j < combo.Items.Count; ++j)
                    {
                        if(combo.Items.GetItemAt(j).ToString() == existingAllergenes[i])
                        {
                            combo.Items.RemoveAt(j);
                        }
                    }
                }
            }

            //TODO set availability and img
            Submit.Content = "MÓDOSÍTÁS";
            mainPanel.IsEnabled = false;
        }
        private void selectedAllergene(object sender, SelectionChangedEventArgs e)
        {
            if (combo.SelectedItem != null)
            {
                ComboBoxItem item = (ComboBoxItem)combo.SelectedItem;
                int index = combo.SelectedIndex;
                Console.WriteLine(item.Content.ToString());
                allergeneListBox.Items.Add(item.Content.ToString());
                Console.WriteLine("item as hozzáadás után");
                combo.Items.RemoveAt(index);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string startdate;
            string enddate;
            if (modifyingWindow == 1 && mainPanel.IsEnabled == false)
            {
                mainPanel.IsEnabled = true;
                Submit.Content = "MENTÉS";
                isModified = true;
            }
            else // adding new food / modified food
            {
                if (textBoxName.Text.Length == 0)
                {
                    errorMessage.Text = "Adja meg az étel nevét!";
                }
                else if (textBoxPrice.Text.Length == 0)
                {
                    errorMessage.Text = "Adja meg az étel árát!";
                }
                else if (always_RadioButton.IsChecked == false && period_RadioButton.IsChecked == false)
                {
                    errorMessage.Text = "Adja meg az étel elérehtőségét!";
                }
                else if (period_RadioButton.IsChecked == true && (fromPeriod.SelectedDate == null || toPeriod.SelectedDate == null))
                {
                    if (fromPeriod.SelectedDate == null && toPeriod.SelectedDate == null)
                    {
                        errorMessage.Text = "Adja meg az étel elérehtőségének kezdő és záró dátumát!";
                    }
                    else if (fromPeriod.SelectedDate != null || toPeriod.SelectedDate == null)
                    {
                        errorMessage.Text = "Adja meg az étel elérehtőségének záró dátumát!";
                    }
                    else if (fromPeriod.SelectedDate == null || toPeriod.SelectedDate != null)
                    {
                        errorMessage.Text = "Adja meg az étel elérehtőségének kezdő dátumát!";
                    }

                }
                else if (period_RadioButton.IsChecked == true && !(DateTime.Now.Date.ToShortDateString().Equals(fromPeriod.SelectedDate.Value.ToShortDateString()))
                    && DateTime.Compare((DateTime)fromPeriod.SelectedDate.Value, DateTime.Now) < 0)
                {
                    errorMessage.Text = "Kezdő dátum nem lehet korábban mint a jelenlegi!";
                }
                else if (period_RadioButton.IsChecked == true && DateTime.Compare((DateTime)fromPeriod.SelectedDate, (DateTime)toPeriod.SelectedDate) > 0)
                {
                    errorMessage.Text = "Záró dátum kisebb mint a kezdő!";
                }
                else
                {
                    foodName = textBoxName.Text;
                    foodPrice = Double.Parse(textBoxPrice.Text);
                    setAllergens(); // TODO save to DB
                    if (period_RadioButton.IsChecked == true)
                    {
                        startdate = fromPeriod.SelectedDate.Value.ToShortDateString();
                        enddate = toPeriod.SelectedDate.Value.ToShortDateString();
                    }
                    else
                    {
                        isAlwaysAvailable = 1;
                    }
                    if (modifyingWindow == 1)
                    {
                        // TODO modify food / picture in database (with just the changed values)
                    }
                    else
                    {
                        food = new Food(foodID, foodName, foodPrice, rating, pictureID, Allergenes);
                        //TODO add new food to database
                        //TODO get the latest foodID from db foodID = foodid;
                        // TODO upload availability to DB
                        savePictureToDatabase(); // TODO save to DB                       
                    }
                    IsSaved = true;
                    this.Close();
                }
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
                string cont = allergeneListBox.Items.GetItemAt(toDelete).ToString();
                ComboBoxItem item = new ComboBoxItem();
                item.Content = cont;
                allergeneListBox.Items.RemoveAt(toDelete);
                combo.Items.Add(item);
            }
        }

        private void priceKeyDown(object sender, TextCompositionEventArgs e)
        {
            TextBox ad = sender as TextBox;
            e.Handled = regex.IsMatch(e.Text);
        }

        void windowClosing(object sender, CancelEventArgs e)
        {
            if (IsSaved != true && isModified == true)
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

        public void setAllergens()
        {
            string allergName;
            Console.WriteLine("0 allergéneknél");
            for(int i = 0; i < allergeneListBox.Items.Count; ++i)
            {
                allergName = allergeneListBox.Items[i].ToString();
                Console.WriteLine(allergName);
                //  TODO get allergen id from database
                //  Upload foodID to database to the given allergenID
                //  Allergen al = new Allergen(allergenID, allergName, foodID);
            }
        }

        private void imgUpload_Click(object sender, RoutedEventArgs e)
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
                imgFilePath = op.FileName;
            }
        }
        private void savePictureToDatabase()
        {
            //TODO
            // get inserted picture id
        }
    }
}
