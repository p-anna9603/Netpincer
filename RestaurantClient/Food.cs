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

        public int FoodID { get => foodID; set => foodID = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public double Rating { get => rating; set => rating = value; }
        public int PictureID { get => pictureID; set => pictureID = value; }
    }
}
