using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
    public class Category
    {
        int categId;
        string categImg;
        string categName;
        public Category(int cId, string cName, string cImg)
        {
            categId = cId;
            categName = cName;
            categImg = cImg;
        }

        public int CategId { get => categId; set => categId = value; }
        public string CategImg { get => categImg; set => categImg = value; }
        public string CategName { get => categName; set => categName = value; }
    }
}
