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
        int categoryID;
        int restaurantID;
        string availableFrom;
        string availableTo;
        double discount;        //átneveztem
        public Food() { }
        public Food(int fID, string nam, double prc, double rate, int picID, List<String> allergs,
            int categoryID, int restID,     //CATEGORYID, RESTID
           string availableFrom,
           string availableTo, double discount_ = 1)
        {
            foodID = fID;
            name = nam;
            price = prc;
            rating = rate;
            pictureID = picID;
            allergenes = allergs;
            restaurantID = restID;
            this.categoryID = categoryID;
            this.availableFrom = availableFrom;
            this.availableTo = availableTo;
            discount = discount_;
        }

        public int FoodID { get => foodID; set => foodID = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public double Rating { get => rating; set => rating = value; }
        public int PictureID { get => pictureID; set => pictureID = value; }
        public List<string> Allergenes { get => allergenes; set => allergenes = value; }
        public string AvailableFrom { get => availableFrom; set => availableFrom = value; }
        public string AvailableTo { get => availableTo; set => availableTo = value; }
        public int RestaurantID { get => restaurantID; set => restaurantID = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
        public double Discount { get => discount; set => discount = value; }
    }
}
