using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    public class FoodList
    {
        public int Type;
        public List<Food> listFood;

        public FoodList(List<Food> food) { listFood = food; Type = 9; }
    }
}
