using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEmulator
{
	
	
    interface INetworkController
    {
        string IP { get; set; }
        string Netmask { get; set; }
        string Gateway { get; set; }
        Boolean DynamicIP { get; set; }
        string MACAddr { get; }
        int Speed { get; set; }
           
        INetworkDevice Device {get;set;}
        void ReceivePacket(Packet p);
        void SendPacket(Packet p);
    }
}
