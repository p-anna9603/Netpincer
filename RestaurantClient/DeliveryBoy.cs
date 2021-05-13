using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
    public class DeliveryBoy
    {
        int deliveryBoyID;
        string name;
        workingSchedule working;
        List<Order> orders;

        public DeliveryBoy(int dID, string nam, workingSchedule work, List<Order> ords = null)
        {
            deliveryBoyID = dID;
            name = nam;
            working = work;
            orders = ords;
        }

        public int DeliveryBoyID { get => deliveryBoyID; set => deliveryBoyID = value; }
        public string Name { get => name; set => name = value; }
        public List<Order> Orders { get => orders; set => orders = value; }
        public workingSchedule Working { get => working; set => working = value; }
    }
}
