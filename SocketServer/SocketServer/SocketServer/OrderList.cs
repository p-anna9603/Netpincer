using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class OrderList
    {
        public List<Order> listOrder;

        public OrderList(List<Order> lo)
        {
            listOrder = lo;
        }
        public OrderList() { }
    }
}
