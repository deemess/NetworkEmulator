using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEmulator
{
    interface INetworkDevice
    {
    	ILocalAreaNetwork Lan {get;set;}
    	void AddInterface(INetworkController iface);
    	void SendPacket(Packet p, INetworkController src);
    	string Hostname {get;set;}
    }
}
