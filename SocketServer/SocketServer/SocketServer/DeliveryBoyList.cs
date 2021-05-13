using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    class DeliveryBoyList
    {
        private List<DeliveryBoy> listDevliveryboy;

        public DeliveryBoyList() { }

        internal List<DeliveryBoy> ListDevliveryboy { get => listDevliveryboy; set => listDevliveryboy = value; }
    }
}

