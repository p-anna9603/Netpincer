using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    class DeliveryBoy
    {
        int deliveryBoyID;
        string name;
        workingSchedule working;
        List<Order> orders;

        public DeliveryBoy(int dID, string nam, workingSchedule work)
        {
            deliveryBoyID = dID;
            name = nam;
            working = work;
            //orders = ords;
        }

        public int DeliveryBoyID { get => deliveryBoyID; set => deliveryBoyID = value; }
        public string Name { get => name; set => name = value; }
        public List<Order> Orders { get => orders; set => orders = value; }
        //internal List<Order> Orders { get => orders; set => orders = value; }
        internal workingSchedule Working { get => working; set => working = value; }
    }
}
