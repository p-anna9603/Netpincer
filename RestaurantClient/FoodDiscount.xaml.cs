using FoodOrderClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RestaurantClient
{
    /// <summary>
    /// Interaction logic for FoodDiscount.xaml
    /// </summary>
    public partial class FoodDiscount : Window
    {
        Food food;
        RestaurantMain restaurantMain;
        Regex regex = new Regex("[^0-9]+");
        double percent = 0;
        int status = 0;

        public FoodDiscount(Window restrantMain, Food food_)
        {
            InitializeComponent();
            restaurantMain = (RestaurantMain)restrantMain;
            food = food_;
            this.Title += " - " + food.Name;
        }
        private void percentage_TextInput(object sender, TextCompositionEventArgs e)
        {
            System.Windows.Controls.TextBox ad = sender as System.Windows.Controls.TextBox;
            e.Handled = regex.IsMatch(e.Text);
        }

        double newPrice;
        private void addButton_Click(object sender, EventArgs e)
        {
            if (percentage.Text.Length == 0)
            {
                errorText.Text = "Adja meg a százalékot";
            }
            else
            {
                percent = Double.Parse(percentage.Text);
                double percentAsNumber;
                percentAsNumber = percent / 100;
                if (percent < 100)
                {
                    newPrice = food.Price * ((100 - percent) / 100);
                }
                else if (percent > 100)
                {
                    string message = "Biztos, hogy növelni akarja az árat?";
                    string caption = "Ár növelése";
                    MessageBoxButtons button = (MessageBoxButtons)MessageBoxButton.YesNo;
                    DialogResult result;
                    result = (DialogResult)System.Windows.MessageBox.Show(message, caption, (MessageBoxButton)button);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        newPrice = food.Price * percent / 100;
                    }
                    else
                    {
                        return;
                    }
                }
                Console.WriteLine("percentage: " + percentAsNumber);
                Console.WriteLine("new percentage: " + percentAsNumber);
                restaurantMain.ServerConnection.setDiscount(food.FoodID, percentAsNumber);

                //   status = restaurantMain.ServerConnection.discountFood(food.FoodID, newPrice); // TODO
                this.Close();
            }
        }
    }
}
