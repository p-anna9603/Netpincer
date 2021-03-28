using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantClient
{
    public class FoodList
    {
        private List<Food> listFood;

        public FoodList(List<Food> food) { listFood = food; }
        public FoodList() { }
        public List<Food> ListFood { get => this.listFood; set => this.listFood = value; }
    }
}
