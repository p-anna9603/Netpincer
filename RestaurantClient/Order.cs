using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
    class Order
    {
        int orderID;
        int orderStatus;
        string orderTime;
        string endorderTime;
        string customer;
        double totalPrice;
        int deliveryPersonId;
        List<Food> orderedFoodList;
        string statusString;
        string foods;

        public Order(int oId, int status, string orderTim, string endOrderTime, string cust, double price, string foods)
        {
            orderID = oId;
            orderStatus = status;
            orderTime = orderTim;
            customer = cust;
            totalPrice = price;
         //   deliveryPersonId = deliveryID;
            // TODO : Parsolni foodsot vesszők szerint és adott foodID-ra megnézni mi a FOOD - szerverről, és ezeket belepakolni a List<Food>-ba
            orderedFoodList = list;
            endorderTime = endOrderTime;
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
            foods = "\tRendelt ételek\n\t\t";
            for(int i = 0; i < orderedFoodList.Count; ++i)
            {
                foods += orderedFoodList[i].Name + "\n\t\t";
            }
        }

        public int OrderID { get => orderID; set => orderID = value; }
        public int OrderStatus { get => orderStatus; set => orderStatus = value; }
        public string OrderTime { get => orderTime; set => orderTime = value; }
        public string Customer { get => customer; set => customer = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
        public List<Food> OrderedFoodList { get => orderedFoodList; set => orderedFoodList = value; }
        public string StatusString { get => statusString; set => statusString = value; }


        public string Details
        {
            get
            {
                return foods;
            }
        }
    }
}
