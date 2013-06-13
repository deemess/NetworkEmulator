using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEmulator
{
    class LocalAreaNetwork: ILocalAreaNetwork
    {

        public List<ILink<INetworkController>> Links {get;set;}
        public List<INetworkDevice> Devices {get;set;}
 
        
        public LocalAreaNetwork()
        {
        	Links = new List<ILink<INetworkController>>();
        	Devices = new List<INetworkDevice>();
        }
        

        public void AddDevice(INetworkDevice dev)
        {
        	// TODO
        	// поиск устройств с одинаковыми именами
        	// ....
        	// поиск конфликта адресов
        	// ....
        	
        	dev.Lan = this;
        	Devices.Add(dev);
        }
        
        public void AddLink(ILink<INetworkController> l)
        {
        	// проверка корректности линка
        	// ....
        	Links.Add(l);
        }
        
//        public void SendPacket(Packet p, INetworkController iface)
//        {
//        	// пакет никуда не уходит, что-нибудь сообщить...
//        	if (iface == null)
//                ;
//        	
//        	//p.TTL--;
//        	
//        	// ищем интерфейс, с которым связан iface
//        	foreach (var i in Links)
//        	{
//        		if (i.Point1 == iface)
//        			i.Point2.ReceivePacket(p);
//        		else if (i.Point2 == iface)
//        			i.Point1.ReceivePacket(p);
//        	}
//        }
        
    }
}
