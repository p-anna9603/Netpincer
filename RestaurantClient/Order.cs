using FoodOrderClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
    public class Order
    {
        int orderID;
        int orderStatus;
        string orderTime;
        string endorderTime;
        string approxDeliveryTime;
        //string customer;
        string address = "8200, Veszprém, Egyetem utca 12.";
        double totalPrice;
        int deliveryPersonId;
        List<Food> orderedFoodList = new List<Food>();
        string statusString;
        string foods;
        RestaurantMain restMain;
        string foodsDesc = "";
        string foodDescToAssignment = "";
        int restaurantID;
        string restaurantName;
        string eta;

        User user;
        public Order(int oId, int status, string orderTim, string endOrderTime, double price, string foodsString, User user_, int restID, string restName = "")
        {
            orderID = oId;
            orderStatus = status;
            orderTime = orderTim;
            //customer = cust;
            totalPrice = price;
            foods = foodsString;
            endorderTime = endOrderTime;
            user = user_;
            restaurantID = restID;
            restaurantName = restName;
            //   approxDeliveryTime = approxDeliveryTime_; // TODO

            //string foodsDesc = "\tRendelt ételek\n\t\t";
            //for(int i = 0; i < orderedFoodList.Count; ++i)
            //{
            //    foodsDesc += orderedFoodList[i].Name + "\n\t\t";
            //}
        }
       
        public int OrderID { get => orderID; set => orderID = value; }
        public int OrderStatus 
        { 
            get => orderStatus;
            set
            {
                orderStatus = value;
             //   Console.WriteLine("#############test id: " + orderID + ", " + orderStatus);
                if (orderStatus == 0)
                {
                    statusString = "Új";
                }
                else if (orderStatus == 1)
                {
                    statusString = "Fogadva";
                }
                else if (orderStatus == 2)
                {
                    statusString = "Kiszállításra kész";
                }
                else if (orderStatus == 3)
                {
                    statusString = "Kiszállítás alatt";
                }
                else if (orderStatus == 4)
                {
                    statusString = "Kiszállítva";
                }
            }     
        }
        public string OrderTime { get => orderTime; set => orderTime = value; }
        //public string Customer { get => customer; set => customer = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
        public List<Food> OrderedFoodList { get => orderedFoodList; set => orderedFoodList = value; }
        public string StatusString { get => statusString; set => statusString = value; }


        public string Details
        {
            get
            {
                return foodsDesc;
            }
        }
        public string Details2
        {
            get
            {
                return foodDescToAssignment;
            }
        }
        public string getFullName
        {
            get
            {
                return user.getLastName() + " " + user.getFirstName();
            }
        }
        public void setFoods(RestaurantMain restMain_)
        {
            Console.WriteLine("kaják: " + foods);
            Console.WriteLine("statusString: " + statusString);
            restMain = restMain_;
            String[] foodNums = foods.Split(',');
            for (int i = 0; i < foodNums.Length; ++i)
            {
                int foodID;
                foodID = Int32.Parse(foodNums[i]);
                /* Test if restMain works with server connection */
                /*
                  Categories cats = restMain.ServerConnection.getCategories(1);
                  Console.WriteLine("cats count: " + cats.ListOfCategoryNames.Count);
                */

                Food food = restMain.ServerConnection.getFoodByID(foodID);
                /* Delete if above function exists : */
                /*
                     List<string> dummyAllergs = new List<string>();
                     dummyAllergs.Add("glutén");
                     Food food = new Food(foodID, "Paprikás pizza", 1200, 3, 0, dummyAllergs, 2, 2, "2021.03.11", "2022.01.01");
                */
                orderedFoodList.Add(food);
            }
            foodsDesc = "\tRendelt ételek\n\t\t";
            for (int i = 0; i < orderedFoodList.Count; ++i)
            {
                foodsDesc += orderedFoodList[i].Name + "\n\t\t";
            }
            foodDescToAssignment = foodsDesc;
           foodDescToAssignment += "\r\tSzállítási cím: " + user.getLine1() + " " + user.getLine2();
            foodDescToAssignment += "\r\tTelefonszám: " + user.getPhoneNumber();
        }

        public void setDescriptions()
        {
            foodsDesc = "\tRendelt ételek\n\t\t";
            for (int i = 0; i < orderedFoodList.Count; ++i)
            {
                foodsDesc += orderedFoodList[i].Name + "\n\t\t";
            }
            foodDescToAssignment = foodsDesc;
            foodDescToAssignment += "\r\tSzállítási cím: " + user.getLine1() + " " + user.getLine2();
            foodDescToAssignment += "\r\tTelefonszám: " + user.getPhoneNumber();
        }
        public RestaurantMain RestMain { get => restMain; set => restMain = value; }
        public string EndorderTime { get => endorderTime; set => endorderTime = value; }
        public string Foods { get => foods; set => foods = value; }
        public string Address { get => address; set => address = value; }
        public User User { get => user; set => user = value; }
        public int RestaurantID { get => restaurantID; set => restaurantID = value; }
        public string RestaurantName { get => restaurantName; set => restaurantName = value; }
        public string Eta { get => eta; set => eta = value; }
    }
}
