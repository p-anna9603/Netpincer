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

        public MainWindow(string username)
        {
            uname = username;
            InitializeComponent();
            //ListaElem
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

        }

        private void kiBt_Click(object sender, EventArgs e)
        {

        }

        private void noBt_Click(object sender, EventArgs e)
        {

        }

        private void munkaBt_Click(object sender, EventArgs e)
        {
            WorkWindow work = new WorkWindow(uname);
            work.Show();
            this.Hide();
        }

        private void rausBt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
