using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delivery_
{
    public class OrderList
    {
        public bool empty =false;
        private List<Order> listOrder;

        public List<Order> ListOrder { get => listOrder; set => listOrder = value; }

        public OrderList(List<Order> lo)
        {
            listOrder = lo;
        }
        public OrderList() { }

        public OrderList(bool empty) { this.empty = empty; }
    }
}
