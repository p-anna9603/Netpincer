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
    /// Interaction logic for startupWindow.xaml
    /// </summary>
    public partial class startupWindow : Window
    {
        string userName;
        string password;

        int registryType;


        public startupWindow()
        {
            InitializeComponent();
            ConnectToServer ServerConnection = new ConnectToServer();
            ServerConnection.addCategory("Pizza");
        }
    }
}
