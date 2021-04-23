using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
    class DeliveryBoy
    {
        int deliveryBoyID;
        string name;
        List<Order> orders; // ongoing deliveries

        public DeliveryBoy(int dID, string nam, List<Order> ords = null)
        {
            deliveryBoyID = dID;
            name = nam;
            orders = ords;
        }

        public int DeliveryBoyID { get => deliveryBoyID; set => deliveryBoyID = value; }
        public string Name { get => name; set => name = value; }
        internal List<Order> Orders { get => orders; set => orders = value; }
    }
}
