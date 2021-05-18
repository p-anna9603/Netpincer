using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace delivery_
{
    public partial class MainWindow : Form
    {
        string uname;
        OrderList rendelista;
        LoginWindow login;
        User deliveryUser;
        public MainWindow(LoginWindow login, User deliveryUser)
        {
            //uname = username;
            this.login = login;
            this.deliveryUser = deliveryUser;
            InitializeComponent();

            string vnevKnev= deliveryUser.getFirstName() + " " + deliveryUser.getLastName();
            labelVnevKnev.Text = vnevKnev;

            //LSTA FELTÖLTÉSE
            ServerConnection server = new ServerConnection();
            rendelista = server.getOrders(deliveryUser.DeliveryPersonID);
            if (!rendelista.empty)
                ListaElem();
            else
                orderlista.Items.Clear();
        }

        private void ListaElem()
        {
            string elem = "";
            for (int i = 0; i < rendelista.ListOrder.Count; i++)
            {
                elem = "";
                elem += rendelista.ListOrder[i].getFullName;
                elem += rendelista.ListOrder[i].Address;                
                elem += rendelista.ListOrder[i].TotalPrice;
                elem += rendelista.ListOrder[i].RestaurantName;
                orderlista.Items.Add(elem);
            }

        }

        private void felBt_Click(object sender, EventArgs e)
        {
            if (rendelista.empty)
                return;
            int orderID = 0;
            orderID = Int32.Parse(orderlista.SelectedIndex.ToString());
            Console.WriteLine("KISZÁLLÍTÁS ALATT ORDER #{0}", orderID);
            ServerConnection server = new ServerConnection();
            server.updateOrderState(orderID, 3);
        }

        private void kiBt_Click(object sender, EventArgs e)
        {
            if (rendelista.empty)
                return;
            int orderID = 0;
            orderID = Int32.Parse(orderlista.SelectedIndex.ToString());
            Console.WriteLine("KISZÁLLÍTVA ORDER #{0}", orderID);
            ServerConnection server = new ServerConnection();
            server.updateOrderState(orderID, 4);
        }

        private void noBt_Click(object sender, EventArgs e)
        {
            if (rendelista.empty)
                return;
            int orderID = 0;
            orderID = Int32.Parse(orderlista.SelectedIndex.ToString());
            Console.WriteLine("DELETE ORDER #{0}", orderID);
            ServerConnection server = new ServerConnection();
            server.removeOrderFromDeliveryBoy(deliveryUser.DeliveryPersonID,orderID);
        }

        private void munkaBt_Click(object sender, EventArgs e)
        {
            WorkWindow work = new WorkWindow(deliveryUser.Username, this);
            work.Show();
            this.Hide();
        }

        private void rausBt_Click(object sender, EventArgs e)
        {
            login.Show();
            this.Close();
        }
    }
}
