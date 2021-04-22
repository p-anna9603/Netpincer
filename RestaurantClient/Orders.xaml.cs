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
        // OrderList listfromServer; TODO
        RestaurantMain restaurantMain;
        List<Order> newOrders = new List<Order>();
        List<Order> acceptedOrders;
        List<Order> underDeliveryOrders;
        Order dummyOrder;
        Order dummyOrder2;
        Order dummyOrder3;
        Order dummyOrder4;
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
            dummyOrder = new Order(1, 0, "2021.04.22 12:22", "Anna", 2000, -1, oneOrdersFoods);
            newOrders.Add(dummyOrder);

            food2 = new Food(3, "Magyaros pizza", 1200, 3, 0, dummyAllergs, 2, 2, "2021.03.11", "2022.01.01");
            oneOrdersFoods.Add(food2);
            dummyOrder2 = new Order(2, 1, "2021.04.22 14:22", "Pista", 2000, -1, oneOrdersFoods);
            newOrders.Add(dummyOrder2);

            food2 = new Food(3, "Magyaros pizza", 1200, 3, 0, dummyAllergs, 2, 2, "2021.03.11", "2022.01.01");
            oneOrdersFoods.Add(food2);
            dummyOrder3 = new Order(3, 2, "2021.04.22 14:22", "Pista", 2000, -1, oneOrdersFoods);
            newOrders.Add(dummyOrder3);

            food2 = new Food(3, "Magyaros pizza", 1200, 3, 0, dummyAllergs, 2, 2, "2021.03.11", "2022.01.01");
            oneOrdersFoods.Add(food2);
            dummyOrder4 = new Order(3, 3, "2021.04.22 14:22", "Pista", 2000, -1, oneOrdersFoods);
            newOrders.Add(dummyOrder4);
        }

        public void addExistingOrders()
        {
            // TODO fill the three list from the listFromServerConnection
            //if (listFromServer.ListFood != null)
            //{
            //    for (int i = 0; i < listFromServer.ListFood.Count; ++i)
            //    {
            //        orders.Add(listFromServer.ListFood[i]);
            //        newFoodWindows.Add(listFromServer.ListFood[i].FoodID, listFromServer.ListFood[i]);
            //    }
            //}

            // TODO add the three list into the datagrid
           // table.Items.Add(newOrders);
            table.ItemsSource = newOrders;
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
            Order selected = (Order)table.SelectedItem;       
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
    }
}
