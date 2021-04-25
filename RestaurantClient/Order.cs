﻿using FoodOrderClient;
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
        List<Food> orderedFoodList = new List<Food>();
        string statusString;
        string foods;
        RestaurantMain restMain;
        string foodsDesc = "";
        public Order(int oId, int status, string orderTim, string endOrderTime, string cust, double price, string foodsString)
        {
            orderID = oId;
            orderStatus = status;
            orderTime = orderTim;
            customer = cust;
            totalPrice = price;
            //   deliveryPersonId = deliveryID;
            // TODO : Parsolni foodsot vesszők szerint és adott foodID-ra megnézni mi a FOOD - szerverről, és ezeket belepakolni a List<Food>-ba
            foods = foodsString;
            /*
            string[] foodNums = foods.Split(',');
            for(int i = 0; i < foodNums.Length; ++i)
            {
                int foodID;
                foodID = Int32.Parse(foodNums[i]);
                Food food = restMain.ServerConnection.getFood(foodID);
                orderedFoodList.Add(food);
            }
            orderedFoodList = list;*/
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
            //string foodsDesc = "\tRendelt ételek\n\t\t";
            //for(int i = 0; i < orderedFoodList.Count; ++i)
            //{
            //    foodsDesc += orderedFoodList[i].Name + "\n\t\t";
            //}
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
                return foodsDesc;
            }
        }

        public void setFoods()
        {
            string[] foodNums = foods.Split(',');
            for (int i = 0; i < foodNums.Length; ++i)
            {
                int foodID;
                foodID = Int32.Parse(foodNums[i]);
                /* Test if restMain works with server connection */
                Categories cats = restMain.ServerConnection.getCategories(1);
               Console.WriteLine("cats count: " + cats.ListOfCategoryNames.Count);

                //Food food = restMain.ServerConnection.getFood(foodID);
                /* Delete if above function exists : */
                List<string> dummyAllergs = new List<string>();
                dummyAllergs.Add("glutén");
                Food food = new Food(foodID, "Paprikás pizza", 1200, 3, 0, dummyAllergs, 2, 2, "2021.03.11", "2022.01.01");

                orderedFoodList.Add(food);
            }
            foodsDesc = "\tRendelt ételek\n\t\t";
            for (int i = 0; i < orderedFoodList.Count; ++i)
            {
                foodsDesc += orderedFoodList[i].Name + "\n\t\t";
            }
        }
        public RestaurantMain RestMain { get => restMain; set => restMain = value; }
    }
}