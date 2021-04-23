using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
    class OrderList
    {
        private List<Order> listOrder;

        public OrderList()
        {

        }
        internal List<Order> ListOrder { get => listOrder; set => listOrder = value; }
    }
}
