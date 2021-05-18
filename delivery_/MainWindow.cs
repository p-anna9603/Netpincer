using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        int z = 0; // ciklus változó a thread-elésben
        List<int> elemek = new List<int>();
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
                elem += rendelista.ListOrder[i].getFullName + " ";
                elem += rendelista.ListOrder[i].Address + " ";                
                elem += rendelista.ListOrder[i].TotalPrice + " Ft";
                elem += " ( " + rendelista.ListOrder[i].RestaurantName + " )";
                orderlista.Items.Add(elem);
                elemek.Add(rendelista.ListOrder[i].OrderID);
            }
        }

        private void felBt_Click(object sender, EventArgs e)
        {
            if (rendelista.empty)
                return;
            int orderID = 0;
            orderID = rendelista.ListOrder[Int32.Parse(orderlista.SelectedIndex.ToString())].OrderID;
            Console.WriteLine("KISZÁLLÍTÁS ALATT ORDER #{0}", orderID);
            ServerConnection server = new ServerConnection();
            server.updateOrderState(orderID, 3);
        }

        private void kiBt_Click(object sender, EventArgs e)
        {
            if (rendelista.empty)
                return;
            int orderID = 0;
            int selectedIndex = orderlista.SelectedIndex;
            orderID = rendelista.ListOrder[Int32.Parse(orderlista.SelectedIndex.ToString())].OrderID;
            Console.WriteLine("KISZÁLLÍTVA ORDER #{0}", orderID);
            ServerConnection server = new ServerConnection();
            server.updateOrderState(orderID, 4);
            this.Refresh();
            orderlista.Items.RemoveAt(selectedIndex);
            elemek.Remove(orderID);
        }

        private void noBt_Click(object sender, EventArgs e)
        {
            if (rendelista.empty)
                return;
            int selectedIndex = orderlista.SelectedIndex;
            int orderID = 0;
            orderID = rendelista.ListOrder[Int32.Parse(orderlista.SelectedIndex.ToString())].OrderID;
            Console.WriteLine("DELETE ORDER #{0}", orderID);
            ServerConnection server = new ServerConnection();
            server.removeOrderFromDeliveryBoy(deliveryUser.DeliveryPersonID,orderID);
            elemek.Remove(orderID);
            this.Refresh();
            orderlista.Items.RemoveAt(selectedIndex);
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

        private void MainWindow_Load(object sender, EventArgs e)
        {
            new Thread(refreshData).Start();
        }

        List<int> fromRendelista = new List<int>();
        List<int> tempElemek = new List<int>();
        public void refreshData()
        {
            while(z == 0)
            {
                Thread.Sleep(5000);
                ServerConnection server = new ServerConnection();
                rendelista = server.getOrders(deliveryUser.DeliveryPersonID);
                if (!rendelista.empty)
                {

                    //ListaElem();
                    string elem = "";
                    for (int i = 0; i < rendelista.ListOrder.Count; i++)
                    {
                        elem = "";
                        elem += rendelista.ListOrder[i].getFullName + " ";
                        elem += rendelista.ListOrder[i].Address + " ";
                        elem += rendelista.ListOrder[i].TotalPrice + " Ft";
                        elem += " ( " + rendelista.ListOrder[i].RestaurantName + " )";
                        fromRendelista.Add(rendelista.ListOrder[i].OrderID);
                        //orderlista.Items.Add(elem);
                        if (!elemek.Contains(rendelista.ListOrder[i].OrderID)) // már tartalmazza az ordert
                        {
                            orderlista.Invoke((MethodInvoker)delegate
                            {
                                orderlista.Items.Add(elem);
                            });                          
                            elemek.Add(rendelista.ListOrder[i].OrderID);
                        }     
                    }

                    for(int z = 0; z < elemek.Count; ++z)
                    {
                        tempElemek.Add(elemek[z]);
                    }
                    //Console.WriteLine("tempElemek count: " + tempElemek.Count);
                    //Console.WriteLine("elemek count 0: " + elemek.Count);
                    //for(int z = 0; z < fromRendelista.Count; ++z)
                    //{
                    //    Console.WriteLine("fromrendelista elem: " + fromRendelista[z]);
                    //}
                    for (int i = 0; i < elemek.Count; ++i) // ha időközben az étterem törölt volna valamit a futártól
                    {                    

                        if(!(fromRendelista.Contains(elemek[i])))
                        {
                            Console.WriteLine("torles volt " + elemek[i]);
                            tempElemek.Remove(elemek[i]);
                            elem = "";
                            elem += rendelista.ListOrder[i].getFullName + " ";
                            elem += rendelista.ListOrder[i].Address + " ";
                            elem += rendelista.ListOrder[i].TotalPrice + " Ft";
                            elem += " ( " + rendelista.ListOrder[i].RestaurantName + " )";
                            orderlista.Invoke((MethodInvoker)delegate
                            {
                                orderlista.Items.Remove(elem);
                            });
                        }
                    }
                    elemek.Clear();
                    for (int z = 0; z < tempElemek.Count; ++z)
                    {
                        elemek.Add(tempElemek[z]);
                    }
                    //Console.WriteLine("elemek count 1: " + elemek.Count);
                }
                else
                {

                    //orderlista.Invoke(
                    //    new UpdateTextCallback(this.UpdateList));
                    elemek.Clear();
                    orderlista.Invoke((MethodInvoker)delegate
                    {
                        orderlista.Items.Clear();
                    });
                }

                /*  orderlista.Items.Clear();*/
                fromRendelista.Clear();
                tempElemek.Clear();
                //Console.WriteLine("elemek count 2: " + elemek.Count);
            }
        }
    }
}
