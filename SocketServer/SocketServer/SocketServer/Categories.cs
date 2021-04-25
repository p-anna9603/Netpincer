using System;
using System.Collections.Generic;

namespace SocketServer
{
    class Categories
    {
        public int Type;
        public List<string> listOfCategoryNames;
        public List<string> listOfCategoryIDs;

        public Categories(List<string> IDs, List<string> cat)
        {
            Type = 7;
            listOfCategoryNames = cat;
            listOfCategoryIDs = IDs;
        }
    }
}
