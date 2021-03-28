using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
    public class Food
    {
        int foodID;
        string name;
        double price;
        double rating;
        int pictureID;
        List<string> allergenes;
        public Food(int fID, string nam, double prc, double rate, int picID, List<String> allergs)
        {
            foodID = fID;
            name = nam;
            price = prc;
            rating = rate;
            pictureID = picID;
            allergenes = allergs;
        }

        public int FoodID { get => foodID; set => foodID = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public double Rating { get => rating; set => rating = value; }
        public int PictureID { get => pictureID; set => pictureID = value; }
        public List<string> Allergenes { get => allergenes; set => allergenes = value; }
    }
}
