using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEmulator
{
    interface ILocalAreaNetwork
    {
    	void AddDevice(INetworkDevice dev);
    	void AddLink(ILink<INetworkController> l);
    	void SendPacket(Packet p, INetworkController iface);
    }
}
