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
    /// Interaction logic for Summary.xaml
    /// </summary>
    public partial class Summary : UserControl
    {
        RestaurantMain restaurantMain;
        public Summary(Window restMain)
        {
            InitializeComponent();
            restaurantMain = (RestaurantMain)restMain;
            int month = DateTime.Now.Month;
            FromDate.SelectedDate = new DateTime(DateTime.Now.Year, month-1, DateTime.Now.Day);
            ToDate.SelectedDate = DateTime.Now;
        }

        private void OK_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (FromDate.SelectedDate > DateTime.Now || ToDate.SelectedDate > DateTime.Now)
            {
                errorText.Text = "Adjon meg dátumot a mai napig bezárólag";
            }
            else
            {
                getInfo();
            }
        }

        public void getInfo()
        {

        }
    }
}
