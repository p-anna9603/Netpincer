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
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        List<Order> orders;
        OrderList listFromServer; 

        RestaurantMain restaurantMain;
        List<Order> newOrders = new List<Order>();
        List<Order> acceptedOrders = new List<Order>();
        List<Order> readyForDelivery = new List<Order>();
        List<Order> underDeliveryOrders = new List<Order>();
        List<List<Order>> ordersLists = new List<List<Order>>();

        Order dummyOrder;
        Order dummyOrder2;
        Order dummyOrder3;
        Order dummyOrder4;
        Order dummyOrder5;
        List<string> dummyAllergs = new List<string>();
        List<Food> oneOrdersFoods = new List<Food>();
        Food food;
        Food food2;
        stateChecker stateCheck;
        int newState;

        public Orders(Window restrantMain)
        {
            InitializeComponent();
            restaurantMain = (RestaurantMain)restrantMain;
            //     listFromServer = restaurantMain.ServerConnection.getOrders(restaurantMain.CurrUser.restaurantID); // TODO
            addExistingOrders();

            dummyAllergs.Add("glutén");
            food = new Food(1, "Paprikás pizza", 1200, 3, 0, dummyAllergs, 2, 2, "2021.03.11", "2022.01.01");
            oneOrdersFoods.Add(food);
            dummyOrder = new Order(1, 0, "2021.04.22 12:22", "", "Anna", 2000, oneOrdersFoods);
            newOrders.Add(dummyOrder);

            food2 = new Food(2, "Magyaros pizza", 1400, 3, 0, dummyAllergs, 2, 2, "2021.03.11", "2022.01.01");
            oneOrdersFoods.Add(food2);
            dummyOrder2 = new Order(2, 1, "2021.04.22 14:22","", "Pista", 2000, oneOrdersFoods);
            acceptedOrders.Add(dummyOrder2);

            dummyOrder3 = new Order(3, 2, "2021.04.22 14:22", "", "Zoli", 2000, oneOrdersFoods);
            readyForDelivery.Add(dummyOrder3);

            food2 = new Food(3, "Tészta leves", 700, 3, 0, dummyAllergs, 2, 2, "2021.03.11", "2022.01.01");
            oneOrdersFoods.Add(food2);
            dummyOrder4 = new Order(4, 3, "2021.04.22 14:22", "", "Réka", 2000, oneOrdersFoods);
            underDeliveryOrders.Add(dummyOrder4);

            dummyOrder5 = new Order(4, 0, "2021.04.22 14:22", "", "János", 2000, oneOrdersFoods);
            newOrders.Add(dummyOrder5);
            //Button button = new Button();
            //more_Click(button, (RoutedEventArgs)EventArgs.Empty);
            //more_Click(button, (RoutedEventArgs)EventArgs.Empty);
            more_Click(null, null);
            more_Click(null, null);
        }
    
        public void addExistingOrders()
        {
            // TODO fill the three list from the listFromServerConnection
            /*
            if (listFromServer.ListOrder != null)
            {
                for (int i = 0; i < listFromServer.ListOrder.Count; ++i)
                {
                    if(listFromServer.ListOrder[i].OrderStatus == 0) // Új
                    {
                        newOrders.Add(listFromServer.ListOrder[i]);
                        restaurantMain.CheckedNewOrders.Add(listFromServer.ListOrder[i]);
                    }
                    else if (listFromServer.ListOrder[i].OrderStatus == 1) // Fogadva
                    {
                        acceptedOrders.Add(listFromServer.ListOrder[i]);
                    }
                    else if(listFromServer.ListOrder[i].OrderStatus == 2) // Kiszállításra kész
                    {
                        underDeliveryOrders.Add(listFromServer.ListOrder[i]);
                    }
                    else if(listFromServer.ListOrder[i].OrderStatus == 3) // Kiszállítás alatt
                    {
                        underDeliveryOrders.Add(listFromServer.ListOrder[i]);
                    }
            
                  //  newFoodWindows.Add(listFromServer.ListFood[i].FoodID, listFromServer.ListFood[i]);
                }
            }*/
            restaurantMain.newOrderCount.Visibility = Visibility.Hidden;
            ordersLists.Add(newOrders);
            ordersLists.Add(acceptedOrders);
            ordersLists.Add(readyForDelivery);
            ordersLists.Add(underDeliveryOrders);
            // TODO add the three list into the datagrid
            table.ItemsSource = ordersLists;
            // table.Items.Add(newOrders);
           // table.ItemsSource = newOrders;
        }

        private void more_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("click on button!!: " + table.RowDetailsVisibilityMode);
            if (table.RowDetailsVisibilityMode == DataGridRowDetailsVisibilityMode.Collapsed)
            {
                table.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
            }
            else if(table.RowDetailsVisibilityMode == DataGridRowDetailsVisibilityMode.VisibleWhenSelected)
            {
                table.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Collapsed;
            }
        }

        private void setting_Click(object sender, RoutedEventArgs e)
        {
            List<Order> listorder = (List<Order>)table.SelectedItem;
            Order selected = listorder[0]; // needed to initialize it
            int orderID = Int32.Parse((table.SelectedCells[0].Column.GetCellContent(listorder) as TextBlock).Text);
            for(int i = 0; i < listorder.Count; ++i)
            {
                if(listorder[i].OrderID == orderID)
                {
                    selected = listorder[i];
                }      
                else
                {
                    selected = null;
                }
            }
            if (selected == null)
            {
                return;
            }
            Console.WriteLine("id.0: " + (table.SelectedCells[0].Column.GetCellContent(listorder) as TextBlock).Text);
            Console.WriteLine("selected: " + selected.OrderID);
            //      Order selected = (Order)table.SelectedItem;  // eredeti mikor egy lista volt az ItemsSource!
            stateCheck = new stateChecker();
            stateCheck.ShowDialog();
            if(stateCheck.NewState == -1) // nothing has changed
            {
                return;
            }
            else
            {
                newState = stateCheck.NewState;
                //  restaurantMain.ServerConnection.updateState(selected.OrderID, newState); TODO
                selected.OrderStatus = newState;
                Console.WriteLine(selected.OrderID + " új állapota: " + selected.OrderStatus);
                restaurantMain.refreshRequested(this);                
            } 
        }

        private void table_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            return;
            Console.WriteLine("click on rooow!!");

            DataGridRow row = sender as DataGridRow;
            if (row != null)
            {
                //  row.DetailsVisibility = row.IsSelected ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
                //   row.DetailsVisibility = Visibility.Collapsed;
                Console.WriteLine("click on table!! + " + row.DetailsVisibility);
                if (row.IsSelected && row.DetailsVisibility == Visibility.Visible)
                {
                    row.DetailsVisibility = Visibility.Visible;
                }
                if (row.IsSelected && row.DetailsVisibility == Visibility.Collapsed)
                {
                    row.DetailsVisibility = Visibility.Collapsed;
                }
            }
        }

        private void table_onClick(object sender, MouseButtonEventArgs e)
        {
            return;
            DataGridRow row = sender as DataGridRow;
      
            if (row != null)
            {
              //  table.Columns[1].
            }
        }

        private void rowDetails_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("visiblility changed");
            Console.WriteLine("sender: " + sender);
            Console.WriteLine("e: " + e.ToString());
        }
    }
}
