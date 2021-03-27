using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
    class Categories
    {
        [Newtonsoft.Json.JsonProperty]
        private int type;
        [Newtonsoft.Json.JsonProperty]
        private int clientID;
        [Newtonsoft.Json.JsonProperty]
        private List<string> listOfCategories;

        public Categories(int clientID_, List<string> cat)
        {
            type = 8;
            clientID = clientID_;
            listOfCategories = cat;
        }

        public int getType() { return type; }

        public List<string> getListOfCategories() { return listOfCategories; }


    }
}
