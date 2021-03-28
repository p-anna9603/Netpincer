using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
    public class Categories
    {
        [Newtonsoft.Json.JsonProperty]
        private List<string> listOfCategoryIDs;
        [Newtonsoft.Json.JsonProperty] 
        private List<string> listOfCategoryNames;

        public Categories() { }

        public Categories(List<string> IDs, List<string> names)
        {
            listOfCategoryIDs = IDs;
            listOfCategoryNames = names;
        }

        public List<string> ListOfCategoryIDs { get => listOfCategoryIDs; set => listOfCategoryIDs = value; }
        public List<string> ListOfCategoryNames { get => listOfCategoryNames; set => listOfCategoryNames = value; }
    }
}
