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
    /// Interaction logic for Summary.xaml
    /// </summary>
    public partial class Summary : UserControl
    {
        RestaurantMain restaurantMain;
        OrderList listFromServer;
        List<Order> orders = new List<Order>();
        int countAllOrders = 0;
        int year, month, day;
        DateTime date;
        Dictionary<string, int> categoryCounter = new Dictionary<string, int>(); // map[categoryName] = counter
        Dictionary<string, int> foodCounter = new Dictionary<string, int>(); // map[foodName] = counter;
        Dictionary<int, String> categoryNames = new Dictionary<int, string>(); // map[categoryid] = category name
        List<string> categories = new List<string>();
        Categories cat;
        double totalGrossIncome = 0, totalNetIncome = 0, costOfDelivery = 0;
        int costPerOrder = 1000;
        public Summary(Window restMain)
        {
            InitializeComponent();
            restaurantMain = (RestaurantMain)restMain;
            int month = DateTime.Now.Month;
            FromDate.SelectedDate = new DateTime(DateTime.Now.Year, month-1, DateTime.Now.Day);
            ToDate.SelectedDate = DateTime.Now;
            listFromServer = restaurantMain.ServerConnection.getOrders(restaurantMain.CurrUser.restaurantID);
            cat = restaurantMain.ServerConnection.getCategories(restaurantMain.CurrUser.restaurantID);
            addExistingCategories();
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(120, 102, 102, 255);
            border.Background = color;
            foodScrollView.Background = color;
        }
        private void addExistingCategories()
        {
            Console.WriteLine("########## GET categs ###################");
            for (int i = 0; i < cat.ListOfCategoryIDs.Count; ++i)
            {
                categoryNames.Add(Int32.Parse(cat.ListOfCategoryIDs[i]), cat.ListOfCategoryNames[i]);
                if(!categoryCounter.ContainsKey(cat.ListOfCategoryNames[i]))
                {
                    categoryCounter[cat.ListOfCategoryNames[i]] = 0;
                }
              //  Category categ = new Category(Int32.Parse(cat.ListOfCategoryIDs[i]), cat.ListOfCategoryNames[i], "");
            }
            Console.WriteLine("########## categs taken ###################");
        }
            private void OK_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (FromDate.SelectedDate > DateTime.Now || ToDate.SelectedDate > DateTime.Now)
            {
                errorText.Text = "Adjon meg dátumot a mai napig bezárólag";
            }
            else
            {
                Console.WriteLine("ok gomb megnyomva");
                init();
                getInfo();
                setInfo();
                errorText.Text = "";
            }
        }
        
        public void init()
        {
            Console.WriteLine("initelés");
            countAllOrders = 0;
            totalGrossIncome = 0;
            totalNetIncome = 0;
            costOfDelivery = 0;
            foreach(KeyValuePair<string, int> c in foodCounter.ToList())
            {
                foodCounter[c.Key] = 0;
            }
          
            foreach (KeyValuePair<string, int> c in categoryCounter.ToList())
            {
                categoryCounter[c.Key] = 0;
            }
        }
        public void getInfo()
        {
            Console.WriteLine("########## GET INFO ###################");
            if (listFromServer.ListOrder != null)
            {
                for (int i = 0; i < listFromServer.ListOrder.Count; ++i)
                {
                    Console.WriteLine("summary ord: ########" + listFromServer.ListOrder[i].User.Username);
                    listFromServer.ListOrder[i].RestMain = restaurantMain;
                    listFromServer.ListOrder[i].setFoods();
                    string[] orderTime = listFromServer.ListOrder[i].OrderTime.Split('.');
                    year = Int32.Parse(orderTime[0]);
                    month = Int32.Parse(orderTime[1]);
                    day = Int32.Parse(orderTime[2]);
                    date = new DateTime(year, month, day);
                    Console.WriteLine("order dátum: " + date);
                    Console.WriteLine("from dátum: " + FromDate.SelectedDate);
                    Console.WriteLine("to dátum: " + ToDate.SelectedDate);

                    if (listFromServer.ListOrder[i].OrderStatus == 4 && date >= FromDate.SelectedDate && date <= ToDate.SelectedDate) // Kiszállított
                    {
                        Order order = listFromServer.ListOrder[i];
                        String[] foodNums = order.Foods.Split(',');
                        orders.Add(order);
                        Console.WriteLine("kiszállított foglalás: " + order.User.Username);
                        //restaurantMain.CheckedNewOrders.Add(listFromServer.ListOrder[i]);
                        restaurantMain.CheckedNewOrdersID.Add(order.OrderID);
                        countAllOrders++;
                        totalGrossIncome += order.TotalPrice;
                        for (int z = 0; z < foodNums.Length; ++z)
                        {
                            int foodID;
                            string categName;
                            foodID = Int32.Parse(foodNums[i]);
                            Food food = restaurantMain.ServerConnection.getFoodByID(foodID);
                            categName = categoryNames[food.CategoryID];
                            categoryCounter[categName]++;
                            if(!foodCounter.ContainsKey(food.Name))
                            {
                                foodCounter.Add(food.Name, 0);
                            }
                            foodCounter[food.Name]++;
                        }
                    }
                }
            }
            Console.WriteLine("########## INFO TAKEN ###################");
        }
        public void setInfo()
        {
            var orderedCategory = categoryCounter.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            var orderedFood = foodCounter.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            if(orderedCategory.Count != 0)
            {
                BestCategory.Text = orderedCategory.ElementAt(0).Key;
                bestCategCount.Text = "  (" + orderedCategory.ElementAt(0).Value.ToString() + "x)";

                WorstCategory.Text = orderedCategory.Last().Key;
                WorstCategoryCnt.Text = "  (" + orderedCategory.Last().Value.ToString() + "x)";
            }
            else
            {
                BestCategory.Text = "n/a";
                WorstCategory.Text = "n/a";
            }

            if (orderedFood.Count != 0)
            {
                BestFood.Text = orderedFood.ElementAt(0).Key;
                BestFoodCount.Text = "  (" + orderedFood.ElementAt(0).Value.ToString() + "x)";
                WorstFood.Text = orderedFood.Last().Key;
                WorstFoodCnt.Text = "  (" + orderedFood.Last().Value.ToString() + "x)";
            }
            else
            {
                BestFood.Text = "n/a";
                WorstFood.Text = "n/a";
            }
            if(countAllOrders < 2)
            {
             //   BestFood.Text = "n/a";
                WorstFood.Text = "n/a";
            }
            totalCounter.Text = countAllOrders.ToString();
            grossInc.Text = totalGrossIncome.ToString();
            int toPay = countAllOrders * costPerOrder;
            costDelivery.Text = toPay.ToString();
            netInc.Text = (totalGrossIncome - toPay).ToString();
        }
    }
}
