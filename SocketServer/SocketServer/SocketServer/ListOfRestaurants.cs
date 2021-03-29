using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class ListOfRestaurants
    {
        public List<Restaurant> restaurantList;

        public ListOfRestaurants() { }
        public ListOfRestaurants(List<Restaurant> resList) { restaurantList = resList; }


    }
}
