using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NetworkEmulator
{
    class EthernetController: INetworkController
    {
        
               
        private string _mac;
        
        public INetworkDevice Device {get;set;}
        //public WiredLink Link {get;set;}
        public ILink<INetworkController> Link {get;set;}
     
        /// <summary>
        /// Создается сетевой интерфейс с параметрами по умолчанию(автоматическое получение настроек)
        /// </summary>
        public EthernetController()
        {
            this.IP = "";
            this.Netmask = "";
            this.Gateway = "";
            this.DynamicIP = true;
            this.Speed = 100;
            this._mac = GenerateMACAddress();
            
            //dev.onReceiveEvent += 
        }


        /// <summary>
        /// Создается проводной сетевой интерфейс с заданными параметрами
        /// </summary>
        /// <param name="ip">IP-адрес</param>
        /// <param name="netmask">Маска подсети</param>
        /// <param name="_gateway">Основной шлюз</param>
        public EthernetController(string ip, string netmask, string _gateway = "")
        {
            this.IP = ip;
            this.Netmask = netmask;
            this.Gateway = _gateway;
            this.DynamicIP = false;
            this._mac = GenerateMACAddress();
        }

        public string IP { get; set; }
        public string Netmask { get; set; }
        public string Gateway { get; set; }
        public Boolean DynamicIP { get; set; }
        public int Speed { get; set; }
        public string MACAddr
        {
            get
            {
                return this._mac;
            }
        }
        
        public void ReceivePacket(Packet p)
        {
        	// пакет дошел
            if (p.DestinationIP == IP)
                switch (p.Type)
                {
                    case PacketType.Ping:
                    	p.TTL--; //FIXME
                      	p.Message += "\nPING: Packet reached its destination " + p.DestinationIP + " at ... TTL " + p.TTL.ToString();
						p.Message += "\nSending packet back to " + p.SourceIP; 
						p.Dump();
						Packet rp = new Packet(p.DestinationIP, p.Message, PacketType.Pong, p.Size, 50);
						rp.TTL--; //FIXME
                        this.Device.Lan.SendPacket(rp, this);
                        break;
                    case PacketType.Pong:
                        p.TTL--; //FIXME
                        p.Message += "\nPONG: Packet returned back to " + p.DestinationIP + " at ... TTL " + p.TTL.ToString();                   
                        p.Dump();
                        break;
                    case PacketType.Message:
                        p.TTL--; //FIXME
                        p.Dump();
                        break;
                    default:
                        p.TTL--; //FIXME
                        System.Windows.Forms.MessageBox.Show("Неизвестный пакет");
                        break;
                }
                
                else
                    SendPacket(p);
        }
        
        public void SendPacket(Packet p)
        {
        	p.TTL--;
        	if (p.TTL > 0)
        		this.Link.RecievePacket(p, this);
	            //Device.SendPacket(p, this);
        	
        	
        }

        private string GenerateMACAddress()
        {
            Random r = new Random();
            string temp_block;
            string _mac = "";
            for (int i = 0; i < 6; i++)
            {
                temp_block = r.Next(0, 255).ToString("X");
                if (temp_block.Length == 1)
                    temp_block = "0" + temp_block;
                _mac = _mac + temp_block + ":";
            }
            _mac = _mac.Remove(_mac.Length - 1);

            return _mac;
        }
    }
}
