﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SocketServer
{
    public class Order
    {
        int orderID;
        int orderStatus;
        string orderTime;
        string endorderTime;
        User user;
        double totalPrice;
        List<Food> orderedFoodList = new List<Food>();
        //string statusString;
        string foods;
        public Order(int oId, int status, string orderTim, string endOrderTime, User _user, double price, string foodsString)
        {
            orderID = oId;
            orderStatus = status;
            orderTime = orderTim;
            user = _user;
            totalPrice = price;
            this.endorderTime = endOrderTime;
            foods = foodsString;
            
        }

        public int OrderID { get => orderID; set => orderID = value; }
        public int OrderStatus { get => orderStatus; set => orderStatus = value; }
        public string OrderTime { get => orderTime; set => orderTime = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
      
        public string EndorderTime { get => endorderTime; set => endorderTime = value; }
        public string Foods { get => foods; set => foods = value; }
        public User User { get => user; set => user = value; }
    }
}
