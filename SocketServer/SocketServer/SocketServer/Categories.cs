using System;
using System.Collections.Generic;

namespace SocketServer
{
    class Categories
    {
        public int type;
        public int clientID;
        public List<string> listOfCategories;

        public Categories(int clientID_, List<string> cat)
        {
            type = 8;
            clientID = clientID_;
            listOfCategories = cat;
        }
    }
}
