using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantClient
{
    public class DeliveryBoyList
    {
        private List<DeliveryBoy> listDevliveryboy;

        public DeliveryBoyList(List<DeliveryBoy> list) { listDevliveryboy = list; }

        public DeliveryBoyList() {  }

        public List<DeliveryBoy> ListDevliveryboy { get => listDevliveryboy; set => listDevliveryboy = value; }
    }
}
