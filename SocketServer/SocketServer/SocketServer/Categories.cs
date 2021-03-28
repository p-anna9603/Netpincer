using System;
using System.Collections.Generic;

namespace SocketServer
{
    class Categories
    {
        public List<string> listOfCategoryNames;
        public List<string> listOfCategoryIDs;

        public Categories(List<string> IDs, List<string> cat)
        {
            listOfCategoryNames = cat;
            listOfCategoryIDs = IDs;
        }
    }
}
