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
    /// Interaction logic for CategDiscount.xaml
    /// </summary>
    public partial class CategDiscount : Window
    {
        Category category;
        RestaurantMain restaurantMain;
        List<Food> foodList;
        Regex regex = new Regex("[^0-9]+");
        int percent = 0;
        int status = 0;
        public CategDiscount(Window restrantMain, Category c, List<Food> foodList_)
        {
            InitializeComponent();
            this.Title += " - " + c.CategName;
            restaurantMain = (RestaurantMain)restrantMain;
            foodList = foodList_;
            foreach(Food f in foodList)
            {
                Console.WriteLine("food: " + f.Name);
            }
        }
        private void percentage_TextInput(object sender, TextCompositionEventArgs e)
        {
            System.Windows.Controls.TextBox ad = sender as System.Windows.Controls.TextBox;
            e.Handled = regex.IsMatch(e.Text);
        }
        int newPrice;
        private void addButton_Click(object sender, EventArgs e)
        {
            if (percentage.Text.Length == 0)
            {
                errorText.Text = "Adja meg a százalékot";
            }
            else
            {
                percent = Int32.Parse(percentage.Text);
                double percentAsNumber;
                percentAsNumber = percent / 100;
                for(int i = 0; i < foodList.Count; ++i)
                {
                    if(percent < 100)
                    {
                        newPrice = (int)foodList[i].Price * ((100 - percent) / 100);
                    }
                    else if(percent > 100)
                    {
                        string message = "Biztos, hogy növelni akarja az árat?";
                        string caption = "Ár növelése";
                        MessageBoxButtons button = (MessageBoxButtons)MessageBoxButton.YesNo;
                        DialogResult result;
                        result = (DialogResult)System.Windows.MessageBox.Show(message, caption, (MessageBoxButton)button);
                        if(result == System.Windows.Forms.DialogResult.Yes)
                        {
                            newPrice = (int)foodList[i].Price * percent / 100;
                        }
                        else
                        {
                            return;
                        }
                    }
                //    restaurantMain.ServerConnection.discountFood(foodList[i].FoodID, percentAsNumber); // TODO
                    //   status = restaurantMain.ServerConnection.discountFood(foodList[i].FoodID, newPrice); // TODO
                }
                this.Close();
            }
        }
    }
}
