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
        DeliveryBoyList listFromServer; // DeliveryBoyList
        List<DeliveryBoy> boys = new List<DeliveryBoy>();
        Dictionary<StackPanel, int> boysPanels = new Dictionary<StackPanel, int>(); // stackpanel, boyID
        Dictionary<Button, Order> xPanels = new Dictionary<Button, Order>(); // xButton, Order

        OrderList orderslistFromServer;
        List<Order> waitingForDeliveryOrders = new List<Order>();
        List<Order> underDeliveryOrders = new List<Order>();
        List<Order> dummyAssign = new List<Order>();

        DateTime dateTime;

        Order dummyOrder;
        Order dummyOrder2;
        Order dummyOrder3;
        Order dummyOrder4;
        workingSchedule dummySchedule;
        workingSchedule dummySchedule2;
        workingSchedule dummySchedule3;
        List<string> dummyAllergs = new List<string>();
        List<Food> oneOrdersFoods = new List<Food>();
        Food food;
        Food food2;

        DeliveryBoy dummyBoy;
        DeliveryBoy dummyBoy2;
        DeliveryBoy dummyBoy3;

        public AssignDelivery(Window restrantMain)
        {
            InitializeComponent();
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(120, 102, 102, 255);
            scrollView.Background = color;
            tableGrid.Background = color;

            restaurantMain = (RestaurantMain)restrantMain;
            //listFromServer = restaurantMain.ServerConnection.getDeliveryBoys(restaurantMain.CurrUser.restaurantID); // TODO
            orderslistFromServer = restaurantMain.ServerConnection.getOrders(restaurantMain.CurrUser.restaurantID);

            addOrdersToTable();

            //dummySchedule = new workingSchedule(1, 8, 16, 30, 0, "1,2,3");
            //dummySchedule2 = new workingSchedule(2, 8, 16, 30, 0, "2,4,5");
            //dummySchedule3 = new workingSchedule(3, 8, 22, 30, 0, "5,6,7");

            dummyAllergs.Add("glutén");
            food = new Food(1, "Paprikás pizza", 1200, 3, 0, dummyAllergs, 2, 2, "2021.03.11", "2022.01.01");
            oneOrdersFoods.Add(food);
            //##KLAU## dummyOrder = new Order(1, 0, "2021.04.22 12:22", "", "Anna", 2000, "2");
          //  waitingForDeliveryOrders.Add(dummyOrder);

            food2 = new Food(3, "Magyaros pizza", 1200, 3, 0, dummyAllergs, 2, 2, "2021.03.11", "2022.01.01");
            oneOrdersFoods.Add(food2);
            //##KLAU## dummyOrder2 = new Order(2, 1, "2021.04.22 14:22", "", "Pista", 2000, "1");
            //   waitingForDeliveryOrders.Add(dummyOrder2);
            dummyAssign.Add(dummyOrder2);

            oneOrdersFoods.Add(food2);
            //##KLAU##  dummyOrder3 = new Order(3, 2, "2021.04.22 14:22", "", "Lilla", 2000, "1,2");
            //   waitingForDeliveryOrders.Add(dummyOrder3);

            oneOrdersFoods.Add(food2);
            //##KLAU## dummyOrder4 = new Order(4, 3, "2021.04.22 14:22", "", "Zoli", 2000,  "2");
            //   waitingForDeliveryOrders.Add(dummyOrder4);
            dummyAssign.Add(dummyOrder4);

           // dummyBoy = new DeliveryBoy(1, "David", dummySchedule);
           //// boys.Add(dummyBoy);
           // dummyBoy2 = new DeliveryBoy(2, "Ákos", dummySchedule2, dummyAssign);
           // //   boys.Add(dummyBoy2);
           // dummyBoy3 = new DeliveryBoy(3, "Fruzsi", dummySchedule3, dummyAssign);

            ConnectToServer server = new ConnectToServer();

            listFromServer = server.getDeliveryBoys();
            //listFromServer.ListDevliveryboy = new List<DeliveryBoy>();
            //listFromServer.ListDevliveryboy.Add(dummyBoy);
            //listFromServer.ListDevliveryboy.Add(dummyBoy2);
            //listFromServer.ListDevliveryboy.Add(dummyBoy3);
            addExistingDeliveryBoys();
        }
        public void addExistingDeliveryBoys()
        {
            // TODO fill the list from the listFromServerConnection
            
            if (listFromServer.ListDevliveryboy != null)
            {
                for (int i = 0; i < listFromServer.ListDevliveryboy.Count; ++i) // loop through the delivery boys
                {
                    Console.WriteLine("delivery booooy: " + listFromServer.ListDevliveryboy[i].Name);
                    int isDayOk = 0;
                    dateTime = DateTime.Now;
                    int day = (int)dateTime.DayOfWeek; // current day of the week in number
                    if(day == 0)
                    {
                        day = 7;
                    }
                    int hour = dateTime.Hour;
                    int min = dateTime.Minute;
                    workingSchedule workTime = listFromServer.ListDevliveryboy[i].Working;
                    for (int j = 0; j < workTime.WorkingDaysInInt.Count; ++j)
                    {
                        if(workTime.WorkingDaysInInt[j] == day)
                        {
                            Console.WriteLine("delivery booooy: 2 ");
                            isDayOk = 1;
                            break;
                        }
                    }
                    if(isDayOk == 1 && workTime.FromHour <= hour  && 
                       ( workTime.ToHour > hour || workTime.ToHour == hour && workTime.ToMinute  >= min))
                    {
                        Console.WriteLine("delivery booooy: 3");
                        boys.Add(listFromServer.ListDevliveryboy[i]);
                        //addBoyPanel(boys[i].Name, boys[i].DeliveryBoyID, boys[i].Orders);
                    }
                    /* Don't know if this neccessary
                    List<Order> orders = listFromServer.ListDevliveryboy[i].Orders;
                    for(int z = 0; z < orders.Count; ++z)
                    {
                        if(orders[z].OrderStatus == 3) // kiszállítás alatt
                        {
                            addOngoingDeliveryToDeliveryBoy(orders[z], listFromServer.ListDevliveryboy[i]);
                        }
                    }
                    */

                //    newFoodWindows.Add(listFromServer.ListFood[i].FoodID, listFromServer.ListFood[i]);
                }
            }
            
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
            if (orderslistFromServer.ListOrder != null)
            {
                for (int i = 0; i < orderslistFromServer.ListOrder.Count; ++i)
                {
                    if(orderslistFromServer.ListOrder[i].OrderStatus == 2) // only those orders which are ready for delivery
                    {
                        orderslistFromServer.ListOrder[i].RestMain = restaurantMain;
                        orderslistFromServer.ListOrder[i].setFoods();
                        waitingForDeliveryOrders.Add(orderslistFromServer.ListOrder[i]);
                        Console.WriteLine("szállítási címe: " + orderslistFromServer.ListOrder[i].Address);
                      //  newFoodWindows.Add(listFromServer.ListFood[i].FoodID, listFromServer.ListFood[i]);
                    }
                    /*
                    else if(orderslistFromServer.ListOrder[i].OrderStatus == 3) // under delivery for further inspection (in addBoyPanel)
                    {
                        underDeliveryOrders.Add(orderslistFromServer.ListOrder[i]);
                    }
                    */
                }
            }            
            table.ItemsSource = waitingForDeliveryOrders;
        }

        private void addBoyPanel(string boyName, int boyID, List<Order> orders)
        {
            Console.WriteLine("addpanel 2 " + boyName);
            StackPanel newCategory = new StackPanel();
            newCategory.Orientation = Orientation.Vertical;
            newCategory.HorizontalAlignment = HorizontalAlignment.Left;
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
            border.VerticalAlignment = VerticalAlignment.Center;

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
            panel2.Name = "firstChildOfScroll";

            StackPanel panel3 = new StackPanel(); // hold the textblock and the x button

            /* Block of the orders the delivery boy is delivering */
            TextBlock newText2;
            TextBlock newTextNum;
            int counter = 0;
            if (orders != null)
            {
                for (int i = 0; i < orders.Count; ++i)
                {
                    if (orders[i].OrderStatus == 3)
                    {
                        counter++;
                        newTextNum = new TextBlock();
                        newTextNum.Margin = new Thickness(0, 3, 0, 0);
                        newTextNum.Name = "counter";
                        newTextNum.FontFamily = new FontFamily("Century");
                        newTextNum.VerticalAlignment = VerticalAlignment.Top;
                        newText2 = new TextBlock();
                        newText2.Name = "foodPrice" + i;
                        newText2.VerticalAlignment = VerticalAlignment.Top;
                        newText2.HorizontalAlignment = HorizontalAlignment.Center;
                        newText2.FontFamily = new FontFamily("Century");
                        newText2.FontSize = 12;
                        newText2.TextWrapping = TextWrapping.Wrap;
                        newText2.Margin = new Thickness(0, 3, 0, 0);
                        newTextNum.Text = counter + ". ";
                        newText2.Text = orders[i].OrderID.ToString() + ". id -jú rendelés";
                        //  ordersTextBlock.Add(newText2);
                        //   panel2.Children.Add(newText2);
                        panel3 = new StackPanel(); // hold the textblock and the x button
                        panel3.Orientation = Orientation.Horizontal;
                        panel3.HorizontalAlignment = HorizontalAlignment.Center;

                        panel3.Children.Add(newTextNum);
                        panel3.Children.Add(newText2);
                        
                        if(orders[i].RestMain.CurrUser.restaurantID == restaurantMain.CurrUser.restaurantID) // if it is another reasurants order do not allow to x
                         {
                                /* x button */
                                Button xButton = new Button();
                                xButton.Margin = new Thickness(10, 2, 0, 0);
                                xButton.Width = 18;
                                xButton.Height = 18;
                                xButton.VerticalContentAlignment = VerticalAlignment.Center;
                                xButton.Background = System.Windows.Media.Brushes.Transparent;
                                xButton.BorderBrush = System.Windows.Media.Brushes.Transparent;
                                xButton.Click += XButton_Click;

                                /* x image */
                                Image img = new Image();
                                img.Source = new BitmapImage(new Uri("Assets/exit.png", UriKind.Relative));
                                img.Stretch = Stretch.Fill;
                                xButton.Content = img;

                                panel3.Children.Add(xButton);
                                xPanels.Add(xButton, orders[i]);
                            }
                        panel2.Children.Add(panel3);
                    }
                }
            }
            scrollArea.Content = panel2;


            newCategory.Children.Add(border);
            newCategory.Children.Add(scrollArea);
            newCategory.MouseEnter += NewCategory_MouseEnter;
            newCategory.MouseLeave += NewCategory_MouseLeave;
       //     boysPanels[newCategory] = boyID;
            boysPanels[panel2] = boyID;
            futarLista.Children.Add(newCategory);
        }

        private void XButton_Click(object sender, RoutedEventArgs e)
        {
            Button xButton = sender as Button;
            StackPanel stack = (StackPanel)xButton.Parent; // panel3
            StackPanel panel2 = (StackPanel)stack.Parent; // panel2
            int boyID = boysPanels[panel2];
            panel2.Children.Remove(stack);
          
            Order order = xPanels[xButton];
            order.OrderStatus = 2;
            order.StatusString = "Kiszállításra kész";
            waitingForDeliveryOrders.Add(order);
            table.ItemsSource = null;
            table.ItemsSource = waitingForDeliveryOrders;
            xPanels.Remove(xButton);

            Console.WriteLine("boy: " + boyID + ", o:  " + order.OrderID);
            restaurantMain.ServerConnection.updateOrderState(order.OrderID, 2);
            //   restaurantMain.ServerConnection.removeOrderFromDeliveryBoy(boyID, order.OrderID); // TODO

            /* Újra számozás */
            //    List<StackPanel> children = panel2.Children;
            int counter = 0;
            for(int i = 0; i < panel2.Children.Count; ++i)
            {
                StackPanel panel = (StackPanel)panel2.Children[i]; // panel3
                for(int j = 0; j < panel.Children.Count; j++)
                {
                    if(panel.Children[j].GetType() == typeof(TextBlock))
                    {
                        TextBlock t = (TextBlock)panel.Children[j];
                        if(t.Name.Equals("counter"))
                        {
                            counter++;
                            t.Text = counter + ". ";
                        }                        
                    }
                }
            }
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
            startPoint = e.GetPosition(null);
        }

        private void table_MouseMove(object sender, MouseEventArgs e)
        {
            // No drag operation
         //   Console.WriteLine("table_MouseMove");
            if (startPoint == null || sender == null)
                return;
          //  Console.WriteLine("move. " + sender.GetType().ToString());
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
            var dgSrc = e.Data.GetData("DragSource") as DataGrid;
            var data = e.Data.GetData(typeof(Order));
            if (dgSrc == null || data == null) return;
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
            StackPanel panel3 = new StackPanel(); // hold the textblock and the x button
            panel3.Orientation = Orientation.Horizontal;
            panel3.HorizontalAlignment = HorizontalAlignment.Center;

            int count = stackPanel.Children.Count;
            /* Counter */
            TextBlock newTextNum = new TextBlock();
            newTextNum = new TextBlock();
            newTextNum.Margin = new Thickness(0, 3, 0, 0);
            newTextNum.VerticalAlignment = VerticalAlignment.Top;
            newTextNum.Name = "counter";
            newTextNum.Text = ++count + ". ";

            Console.WriteLine("stack name: " + stackPanel.Name);
            TextBlock newText2 = new TextBlock();
            newText2.Name = "foodPrice";
            newText2.VerticalAlignment = VerticalAlignment.Top;
            newText2.HorizontalAlignment = HorizontalAlignment.Center;
            newText2.FontFamily = new FontFamily("Century");
            newText2.FontSize = 12;
            newText2.TextWrapping = TextWrapping.Wrap;
            newText2.Margin = new Thickness(0, 3, 0, 0);
            newText2.Text = order.OrderID.ToString() + ". id -jú rendelés";

            panel3.Children.Add(newTextNum);
            panel3.Children.Add(newText2);

            /* x button */
            Button xButton = new Button();
            xButton.Margin = new Thickness(10, 2, 0, 0);
            xButton.Width = 18;
            xButton.Height = 18;
            xButton.VerticalContentAlignment = VerticalAlignment.Center;
            xButton.Background = System.Windows.Media.Brushes.Transparent;
            xButton.BorderBrush = System.Windows.Media.Brushes.Transparent;
            xButton.Click += XButton_Click;

            /* x image */
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("Assets/exit.png", UriKind.Relative));
            img.Stretch = Stretch.Fill;
            xButton.Content = img;

            panel3.Children.Add(xButton);
            stackPanel.Children.Add(panel3);

            xPanels.Add(xButton, order);

            Console.WriteLine("kulcs előtt");
            int boyID = boysPanels[stackPanel];
            Console.WriteLine("kulcs után");
            order.OrderStatus = 3; // change order state to under delivery
            order.StatusString = "Kiszállítás alatt";
          //  table.Items.Remove(order);
            addOrderToDeliveryBoy(order, boyID);
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
                    if(t.Name.Equals("firstChildOfScroll"))
                    {
                        return t;
                    }                    
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
        string approxDeliveryTime;
        private void addOrderToDeliveryBoy(Order order, int boyID)
        {
            Console.WriteLine("update to server\n");
            //  restaurantMain.ServerConnection.addOrderToDeliveryBoy(boyID, order.OrderID); //TODO
            restaurantMain.ServerConnection.updateOrderState(order.OrderID, 3);
            Console.WriteLine("update to server 2\n");
            for (int i = 0; i < boys.Count; ++i)
            {
                if (boys[i].DeliveryBoyID == boyID)
                {
                    if (boys[i].Orders == null)
                    {
                        Console.WriteLine("update to server 3\n");
                        boys[i].Orders = new List<Order>();
                    }
                    boys[i].Orders.Add(order);
                    Console.WriteLine("update to server 4\n");
                }
            }
            waitingForDeliveryOrders.Remove(order);
            table.ItemsSource = null;
            table.ItemsSource = waitingForDeliveryOrders;

            dateTime = DateTime.Now;
            DateTime d2 = dateTime.AddMinutes(10);
            approxDeliveryTime = d2.ToShortDateString().Trim() + " " + d2.ToShortTimeString();
            Console.WriteLine(approxDeliveryTime);
            //restaurantMain.ServerConnection.setApproximateDeliveryTime(order.OrderID, restaurantMain.CurrUser.restaurantID, approxDeliveryTime); // TODO
            restaurantMain.ServerConnection.setApproximateDeliveryTime(order.OrderID, restaurantMain.CurrUser.restaurantID); // TODO (vagy ez)

            //  table.Items.Remove(order);
        }

        private void addOngoingDeliveryToDeliveryBoy(Order order, DeliveryBoy deliveryBoy)
        {

        }
    }
}
