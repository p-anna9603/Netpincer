using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{

    public class ListOfRestaurants
    {
        private List<Restaurant> restaurantList;

        public ListOfRestaurants() { }
        public ListOfRestaurants(List<Restaurant> resList) { restaurantList = resList; }

        public List<Restaurant> RestaurantList { get => restaurantList; set => restaurantList = value; }
    }
}
