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
using System.Windows.Shapes;

namespace RestaurantClient
{
    /// <summary>
    /// Interaction logic for stateChecker.xaml
    /// </summary>
    public partial class stateChecker : Window
    {
        int newState = -1;
        public int NewState { get => newState; set => newState = value; }

        public stateChecker()
        {
            InitializeComponent();
        }

        private void ok_onClick(object sender, RoutedEventArgs e)
        {
            if (newOrder.IsChecked == true)
            {
                newState = 0;
            }
            else if (accepted.IsChecked == true)
            {
                newState = 1;
            }
            else if (readyForDelivery.IsChecked == true)
            {
                newState = 2;
            }
            else if (underDelivery.IsChecked == true)
            {
                newState = 3;
            }
            else if (delivered.IsChecked == true)
            {
                newState = 4;
            }
            else
            {
                MessageBox.Show("Válasszon ki egy elemet a listából!");
                return;
            }
            this.Close();
        }
        private void cancel_onClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
