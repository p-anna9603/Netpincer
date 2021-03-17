using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
    class Food
    {
        int foodID;
        string name;
        double price;
        double rating;
        int pictureID;
        public Food(int fID, string nam, double prc, double rate, int picID)
        {
            foodID = fID;
            name = nam;
            price = prc;
            rating = rate;
            pictureID = picID;
        }
    }
}
