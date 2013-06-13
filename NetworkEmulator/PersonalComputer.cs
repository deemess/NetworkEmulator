using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEmulator
{
    class PersonalComputer: INetworkDevice
    {

        public string Hostname {get;set;}
        public List<INetworkController> Interfaces {get;set;}
        private INetworkController defaultIface = null;
        public ILocalAreaNetwork Lan {get;set;}
   

        public PersonalComputer(string hostname = "")
        {
            Interfaces = new List<INetworkController>();
            Hostname = (hostname.Length > 0) ? hostname : GetRandomHostname();
            	
        }
        
        // восьмисимвольный
        private string GetRandomHostname()
        {
        	Random r = new Random((int)DateTime.Now.Ticks);
        	StringBuilder builder = new StringBuilder();
	        char ch;
	        
	        for (int i = 0; i < 8; i++)
	        {
	            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * r.NextDouble() + 65)));                 
	            builder.Append(ch);
	        }
	
	        return builder.ToString();        	
        }
        
        public void AddInterface(INetworkController iface)
        {
        	string gw = iface.Gateway;
        	if (gw.Length > 0)
        	{
        		foreach (var i in Interfaces)
        			if (i.Gateway.Length > 0)
        				throw new GatewayAlreadyDefined("Шлюз по-умолчанию для этого устройства уже задан.");
        		
        		defaultIface = iface;
        	}
        	
        	iface.Device = this;
        	iface.ReceiveEvent += HandleReceive;
        	Interfaces.Add(iface);
        }
        
        public void SendPacket(Packet p, INetworkController src = null)
        {
        	
        	// перебор всех интерфейсов, поиск того, с которого можно отправить
        	INetworkController sender = null;
        	foreach (var i in Interfaces)
        	{
        		if (IP.CompareBySubnets(p.DestinationIP, i.IP, i.Netmask))
        		{
        			sender = i;
        			break;
        		}
        	}


            if (sender != null && src != sender)
            {
                p.SourceIP = sender.IP;
                //sender.Link.RecievePacket(p, sender);
                //Lan.SendPacket(p, sender);
                sender.SendPacket(p);

                
            }
            else
            {
                if (defaultIface == null)
                {
                    // пакет никуда не уходит
                    // сообщить что-нибудь
                    //Lan.SendPacket(p, null);
                }
                else
                {
                    if (src != defaultIface)
                    {
                        p.SourceIP = defaultIface.IP;
                       // Lan.SendPacket(p, defaultIface);
                       defaultIface.SendPacket(p);
                    }
                }
            }
        }
        
        private void HandleReceive(object sender, InterfaceArgs e)
        {
        	Packet p = e.Packet;
        	INetworkController iface = (INetworkController)sender;
        	// пакет дошел
            if (p.DestinationIP == iface.IP)
            {
            	//p.TTL--;
                switch (p.Type)
                {
                    case PacketType.Ping:
                      	p.Message += "\nPING: Packet reached its destination " + p.DestinationIP + " at ... TTL " + p.TTL.ToString();
						p.Message += "\nSending packet back to " + p.SourceIP; 
						p.Dump();
						Packet rp = new Packet(p.SourceIP, p.Message, PacketType.Pong, p.Size, 50);
						//rp.TTL--; //FIXME
                        SendPacket(rp);
                        //this.Device.Lan.SendPacket(rp, this);
                        break;
                    case PacketType.Pong:
                        p.Message += "\nPONG: Packet returned back to " + p.DestinationIP + " at ... TTL " + p.TTL.ToString();                   
                        p.Dump();
                        break;
                    case PacketType.Message:
                        p.Dump();
                        break;
                    default:
                        System.Windows.Forms.MessageBox.Show("Неизвестный пакет");
                        break;
                }
            }
            else
				SendPacket(p, iface);
        }
        
    }
}
