using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEmulator
{
	
	
	interface ILink<out TNetworkController> where TNetworkController: INetworkController
    {
        TNetworkController Point1 { get;  }
        TNetworkController Point2 { get;  }
        void RecievePacket(Packet p, INetworkController iface);
    }
}
