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
        private String categoryName;
        int categoryID = 0;
        private bool isSaved = false;
        public newMenu()
        {
            InitializeComponent();
        }

        public bool IsSaved { get => isSaved; set => isSaved = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (categoryNameTextBox.Text.Length == 0)
            {
                errorText.Text = "Adja meg a kategória nevét";
            }
            else
            {
                MessageBox.Show("Kategória hozzáadása sikeres", "Hozzáadás");
                IsSaved = true;
                categoryName = categoryNameTextBox.Text;
                //TODO upload to DB the new category (and pic)
                // and get the latest categoryID
                this.Close();
            }
        }
        private void imageUpButton_Click(object sender, EventArgs e)
        {

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
    }
}
