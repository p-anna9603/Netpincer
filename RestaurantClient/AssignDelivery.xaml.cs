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
    /// Interaction logic for AssignDelivery.xaml
    /// </summary>
    public partial class AssignDelivery : UserControl
    {

        RestaurantMain restaurantMain;
        // DeliveryBoyList listfromServer; // DeliveryBoyList
        List<DeliveryBoy> boys = new List<DeliveryBoy>();
        Dictionary<StackPanel, int> boysPanels = new Dictionary<StackPanel, int>();

        // OrderList orderslistfromServer; TODO
        List<Order> waitingForDeliveryOrders = new List<Order>();

        Order dummyOrder;
        Order dummyOrder2;
        Order dummyOrder3;
        Order dummyOrder4;
        List<string> dummyAllergs = new List<string>();
        List<Food> oneOrdersFoods = new List<Food>();
        Food food;
        Food food2;

        DeliveryBoy dummyBoy;
        DeliveryBoy dummyBoy2;

        public AssignDelivery(Window restrantMain)
        {
            InitializeComponent();
            restaurantMain = (RestaurantMain)restrantMain;
            //     listFromServer = restaurantMain.ServerConnection.getDeliveryBoys(restaurantMain.CurrUser.restaurantID); // TODO
            //     orderslistfromServer = restaurantMain.ServerConnection.getOrders(restaurantMain.CurrUser.restaurantID); // TODO

            addOrdersToTable();
            dummyAllergs.Add("glutén");
            food = new Food(1, "Paprikás pizza", 1200, 3, 0, dummyAllergs, 2, 2, "2021.03.11", "2022.01.01");
            oneOrdersFoods.Add(food);
            dummyOrder = new Order(1, 0, "2021.04.22 12:22", "Anna", 2000, -1, oneOrdersFoods);
            waitingForDeliveryOrders.Add(dummyOrder);

            food2 = new Food(3, "Magyaros pizza", 1200, 3, 0, dummyAllergs, 2, 2, "2021.03.11", "2022.01.01");
            oneOrdersFoods.Add(food2);
            dummyOrder2 = new Order(2, 1, "2021.04.22 14:22", "Pista", 2000, -1, oneOrdersFoods);
            waitingForDeliveryOrders.Add(dummyOrder2);

            oneOrdersFoods.Add(food2);
            dummyOrder3 = new Order(3, 2, "2021.04.22 14:22", "Pista", 2000, -1, oneOrdersFoods);
            waitingForDeliveryOrders.Add(dummyOrder3);

            oneOrdersFoods.Add(food2);
            dummyOrder4 = new Order(3, 3, "2021.04.22 14:22", "Pista", 2000, -1, oneOrdersFoods);
            waitingForDeliveryOrders.Add(dummyOrder4);

            dummyBoy = new DeliveryBoy(1, "David");
            boys.Add(dummyBoy);

            dummyBoy2 = new DeliveryBoy(1, "Ákos", waitingForDeliveryOrders);
            boys.Add(dummyBoy2);

            addExistingDeliveryBoys();
        }
        public void addExistingDeliveryBoys()
        {
            // TODO fill the list from the listFromServerConnection
            //if (listFromServer.ListFood != null)
            //{
            //    for (int i = 0; i < listFromServer.ListFood.Count; ++i)
            //    {
            //        orders.Add(listFromServer.ListFood[i]);
            //        newFoodWindows.Add(listFromServer.ListFood[i].FoodID, listFromServer.ListFood[i]);
            //    }
            //}

            // table.Items.Add(newOrders);
           // table.ItemsSource = newOrders;
            if (boys.Count != 0)
            {
                Console.WriteLine("addpanel 0");
                for (int i = 0; i < boys.Count; ++i)
                {
                    Console.WriteLine("addpanel 1");
                    addBoyPanel(boys[i].Name, boys[i].DeliveryBoyID, boys[i].Orders);
                }
            }
        }
        
        public void addOrdersToTable()
        {
            // TODO fill the list from the listFromServerConnection
            //if (listFromServer.ListFood != null)
            //{
            //    for (int i = 0; i < listFromServer.ListFood.Count; ++i)
            //    {
            //        waitingForDeliveryOrders.Add(listFromServer.ListFood[i]);
            //        newFoodWindows.Add(listFromServer.ListFood[i].FoodID, listFromServer.ListFood[i]);
            //    }
            //}
            table.ItemsSource = waitingForDeliveryOrders;
        }

        private void addBoyPanel(string boyName, int boyID, List<Order> orders)
        {
            Console.WriteLine("addpanel 2 " + boyName);
            StackPanel newCategory = new StackPanel();
            newCategory.Orientation = Orientation.Vertical;
            newCategory.HorizontalAlignment = HorizontalAlignment.Center;
            newCategory.Width = 200;
            newCategory.Height = 132;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(50, 255, 204, 153);
            newCategory.Background = color;
            newCategory.Margin = new Thickness(5, 5, 0, 0);

            Border border = new Border();
            //border.BorderBrush = new SolidColorBrush(Colors.AliceBlue);
            border.BorderThickness = new Thickness(1);
            border.HorizontalAlignment = HorizontalAlignment.Left;
            border.VerticalAlignment = VerticalAlignment.Top;

            DataTrigger dataTrigger = new DataTrigger();
            Binding binding = new Binding();
            binding.Path = new PropertyPath("IsMouseOver");
            dataTrigger.Binding = binding;


            dataTrigger.Value = true;
            Style style = new Style(typeof(Border));
            SolidColorBrush cs = new SolidColorBrush();
            cs.Color = Color.FromRgb(255, 255, 255);
            style.Setters.Add(new Setter(BorderBrushProperty, cs));
            border.Style = style;

            /* Name of the deliveryBoy */
            TextBlock newText = new TextBlock();
            newText.Name = "boyName";
            newText.VerticalAlignment = VerticalAlignment.Center;
            newText.HorizontalAlignment = HorizontalAlignment.Center;
            newText.TextAlignment = TextAlignment.Center;
            newText.Width = 200;
            newText.Height = 50;
            newText.FontFamily = new FontFamily("Century");
            newText.FontSize = 16;
            newText.TextWrapping = TextWrapping.Wrap;
            newText.Margin = new Thickness(0, 0, 0, 0);
            newText.Text = boyName;
            newText.TextWrapping = TextWrapping.Wrap;

            border.Child = newText;

            /* ScrollViewver */
            ScrollViewer scrollArea = new ScrollViewer();
            scrollArea.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scrollArea.HorizontalAlignment = HorizontalAlignment.Center;
            scrollArea.Height = 79;
            scrollArea.Width = 200;
            scrollArea.PreviewMouseUp += table_PreviewMouseUp;
            scrollArea.AllowDrop = true;
            scrollArea.Drop += DropRow_Drop;
            scrollArea.DragEnter += DropRow_DragEnter;

            List<TextBlock> ordersTextBlock = new List<TextBlock>();
            StackPanel panel2 = new StackPanel();

            /* Block of the orders the delivery boy is delivering */
            TextBlock newText2;
            int counter = 0;
            if (orders != null)
            {
                for (int i = 0; i < orders.Count; ++i)
                {
                    if (orders[i].OrderStatus == 3)
                    {
                        counter++;
                        newText2 = new TextBlock();
                        newText2.Name = "foodPrice" + i;
                        newText2.VerticalAlignment = VerticalAlignment.Top;
                        newText2.HorizontalAlignment = HorizontalAlignment.Center;
                        newText2.FontFamily = new FontFamily("Century");
                        newText2.FontSize = 12;
                        newText2.TextWrapping = TextWrapping.Wrap;
                        newText2.Margin = new Thickness(0, 3, 0, 0);
                        newText2.Text = counter + ". " + orders[i].OrderID.ToString() + ". id -jú rendelés";
                      //  ordersTextBlock.Add(newText2);
                        panel2.Children.Add(newText2);
                    }
                }
            }
            scrollArea.Content = panel2;
            newCategory.Children.Add(border);
            newCategory.Children.Add(scrollArea);
            newCategory.MouseEnter += NewCategory_MouseEnter;
            newCategory.MouseLeave += NewCategory_MouseLeave;
            boysPanels[newCategory] = boyID;
            futarLista.Children.Add(newCategory);
        }

        private void NewCategory_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(50, 255, 204, 153);
            panel.Background = color;
        }

        private void NewCategory_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(100, 255, 204, 153);
            panel.Background = color;
        }
        private void more_Click(object sender, RoutedEventArgs e)
        {

            Console.WriteLine("click on button!!: " + table.RowDetailsVisibilityMode);
            if (table.RowDetailsVisibilityMode == DataGridRowDetailsVisibilityMode.Collapsed)
            {
                table.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
            }
            else if (table.RowDetailsVisibilityMode == DataGridRowDetailsVisibilityMode.VisibleWhenSelected)
            {
                table.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Collapsed;
            }
        }
        private void table_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            return;
        }

        private void table_onClick(object sender, MouseButtonEventArgs e)
        {
            return;
            DataGridRow row = sender as DataGridRow;
        }


        private Point? startPoint;
        int rowIndex = -1;
        /* Drag and Drop functions */
        private void table_leftClick(object sender, MouseButtonEventArgs e)
        {
          //  // Store the mouse position
          //  rowIndex = table.SelectedIndex;
          ////  rowIndex = table.GetCurrentRowIndex(e.GetPosition);
          //  if (rowIndex < 0)
          //      return;
          //  table.SelectedIndex = rowIndex;
          //  Order selectedOrder = table.Items[rowIndex] as Order;
          //  if (selectedOrder == null)
          //      return;
          //  DragDropEffects dragdropeffects = DragDropEffects.Move;
          //  if (DragDrop.DoDragDrop(table, selectedOrder, dragdropeffects)
          //                      != DragDropEffects.None)
          //  {
          //      table.SelectedItem = selectedOrder;
          //  }

            startPoint = e.GetPosition(null);
        }

        private void table_MouseMove(object sender, MouseEventArgs e)
        {
            // No drag operation
            if (startPoint == null)
                return;
            Console.WriteLine("move. " + sender.GetType().ToString());
            var dg = sender as DataGrid;
            if (dg == null) return;
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint.Value - mousePos;
            // test for the minimum displacement to begin the drag
            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {

                // Get the dragged DataGridRow
                var DataGridRow =
                    FindAnchestor<DataGridRow>((DependencyObject)e.OriginalSource);

                if (DataGridRow == null)
                    return;
                // Find the data behind the DataGridRow
                var dataTodrop = (Order)dg.ItemContainerGenerator.
                    ItemFromContainer(DataGridRow);

                if (dataTodrop == null) return;

                // Initialize the drag & drop operation
                var dataObj = new DataObject(dataTodrop);
                dataObj.SetData("DragSource", sender);
                DragDrop.DoDragDrop(dg, dataObj, DragDropEffects.Copy);
                startPoint = null;
            }
        }
        private void table_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            startPoint = null;
        }

        // Helper to search up the VisualTree
        private static T FindAnchestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        // Drop
        private void DropRow_DragEnter(object sender, DragEventArgs e)
        {
            //if (!e.Data.GetDataPresent("myFormat") ||
            //    sender == e.Source)
            //{
            //    e.Effects = DragDropEffects.None;
            //}
        }
        private void DropRow_Drop(object sender, DragEventArgs e)
        {
            Console.WriteLine("drop " + sender.GetType().ToString());
            var dg = sender as ScrollViewer;
            if (dg == null) return;
            Console.WriteLine("drop 2");
            var dgSrc = e.Data.GetData("DragSource") as DataGrid;
            var data = e.Data.GetData(typeof(Order));
            if (dgSrc == null || data == null) return;
            Console.WriteLine("drop 3");
            // Implement move data here, depends on your implementation
            //     MoveDataFromSrcToDest(dgSrc, dg, data);
            // OR
            //     MoveDataFromSrcToDest(dgSrc.DataContext, dg.DataContext, data);
            Order dat = (Order)data;
            Console.WriteLine("draaag: " + dat.OrderID);
            DropNewLine(dat, dg);
        }
        StackPanel t;
        private void DropNewLine(Order order, ScrollViewer scroll)
        {
            StackPanel stackPanel = EnumVisual(this, scroll);
            stackPanel = t;
            int count = stackPanel.Children.Count;
           // List<TextBlock> tts = (List<TextBlock>)scroll.Content;
            //var StackPanel =
            //        FindAnchestor<DataGridRow>((DependencyObject)scroll);
            TextBlock newText2 = new TextBlock();
            newText2.Name = "foodPrice";
            newText2.VerticalAlignment = VerticalAlignment.Top;
            newText2.HorizontalAlignment = HorizontalAlignment.Center;
            newText2.FontFamily = new FontFamily("Century");
            newText2.FontSize = 12;
            newText2.TextWrapping = TextWrapping.Wrap;
            newText2.Margin = new Thickness(0, 3, 0, 0);
            newText2.Text = ++count + ". " + order.OrderID.ToString() + ". id -jú rendelés";
            //if(tts != null)
            //{
            //    tts.Add(newText2);
            //}
            //else
            //{
            //    tts = new List<TextBlock>();
            //    tts.Add(newText2);
            //}
            stackPanel.Children.Add(newText2);
        }

        public StackPanel EnumVisual(Visual myVisual, ScrollViewer categ)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
            {
                // Retrieve child visual at specified index value.
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);

                // Do processing of the child visual object.
                if (childVisual.GetType() == typeof(StackPanel) && childVisual.IsDescendantOf(categ))
                {
                    Console.WriteLine("3 to change");
                    t = (StackPanel)childVisual;
                    return t;
                }
                else
                {
                    EnumVisual(childVisual, categ);
                }
                // TODO picture
                // Enumerate children of the child visual object.
        
            }
            return null;
        }
    }
}
