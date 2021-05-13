using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class DeliveryBoyList
    {
        private List<DeliveryBoy> listDevliveryboy;

        public DeliveryBoyList() { }

        public List<DeliveryBoy> ListDevliveryboy { get => listDevliveryboy; set => listDevliveryboy = value; }
    }
}

