using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NetworkEmulator
{
	/// <summary>
	/// Description of Switch.
	/// </summary>
	class Switch: INetworkDevice
	{
		public List<EthernetController> Interfaces {get;set;}
		public ILocalAreaNetwork Lan {get;set;}
		public string Hostname {get;set;}
		
        /// <summary>
        /// Создание сетевого-хаба. 
        /// </summary>
        /// <param name="ports">Количество портов</param>
        /// <param name="hostname">Имя хоста</param>
		public Switch(uint ports,string hostname = "")
		{
			Interfaces = new List<EthernetController>();
			Hostname = hostname.Length > 0 ? hostname : GetRandomHostname();
            for (int i = 0; i < ports; i++)
                AddInterface(new EthernetController());
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
			if (!(iface is EthernetController))
				throw new ArgumentException("Для данного типа устройств поддерживаются только Ethernet-контроллеры");
			
			iface.Device = this;
			Interfaces.Add(iface as EthernetController);
		}
		
		public void SendPacket(Packet p, INetworkController src)
		{
			// пусть это будет хаб - посылает пакет на все порты, кроме исходного
			foreach (var i in Interfaces)
				if (i != src)
					Lan.SendPacket(p, i);
		}
	}
}
