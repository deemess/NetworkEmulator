using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NetworkEmulator
{
	class InterfaceArgs: EventArgs
	{
		public InterfaceArgs(Packet p)
        {
            packet = p;
        }
        private Packet packet;

        public Packet Packet
        {
            get { return packet; }
            set { packet = value; }
        }
	}
	
    class EthernetController: INetworkController
    {
        
               
        private string _mac;
        
        public INetworkDevice Device {get;set;}
        //public WiredLink Link {get;set;}
        //public ILink<INetworkController> Link {get;set;}
        
        public event EventHandler<InterfaceArgs> SendEvent;
        public event EventHandler<InterfaceArgs> ReceiveEvent;
     
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
        	OnReceivePacket(new InterfaceArgs(p));
        }
        
        public void SendPacket(Packet p)
        {
        	if (p.TTL > 0)
        	{
        		p.TTL--;
        		OnSendPacket(new InterfaceArgs(p));
        	}
        	else
        	{
        		// сообщить, что пакет дропнут по ттл
        	}
//        	p.TTL--;
//        	if (p.TTL > 0)
//                if (this.Device is Switch)
//                {
//                    foreach (var i in (this.Device as Switch).Interfaces)
//                    {
//                        if (i.Link != this.Link)
//                            i.Link.RecievePacket(p, i);      
//                    }
//                }
        		//this.Link.RecievePacket(p, this);
	            //Device.SendPacket(p, this);
        	
        	
        }
        
        protected virtual void OnSendPacket(InterfaceArgs e)
        {
        	EventHandler<InterfaceArgs> handler = SendEvent;
        	if (handler != null)
        	{
        		// этим вызовом дергаем подписчиков на событие (линки)
        		handler(this, e);
        	}
        }
        
        protected virtual void OnReceivePacket(InterfaceArgs e)
        {
        	EventHandler<InterfaceArgs> handler = ReceiveEvent;
        	if (handler != null)
        	{
        		// этим вызовом дергаем подписчиков на событие (сетевые устройства)
        		handler(this, e);
        	}
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
