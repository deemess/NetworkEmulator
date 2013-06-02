using System;

namespace NetworkEmulator
{
	/// <summary>
	/// Description of Packet.
	/// </summary>
	public class Packet
	{
        public string SourceIP { get; set; }
        public string DestinationIP { get; set; }
        public string Message { get; set; }
        public uint Size { get; set; }
        public uint TTL { get; set; }
		
        /// <summary>
        /// Создание сетевого пакета
        /// </summary>
        /// <param name="srcIP">IP-адрес источника</param>
        /// <param name="destIP">IP-адрес получателя</param>
        /// <param name="msg">Сообщение</param>
        /// <param name="size">Размер пакета в килобайтах</param>
		public Packet(string srcIP, string destIP, string msg, uint size = 64, uint ttl = 50)
		{
			SourceIP = srcIP;
			DestinationIP = destIP;
			Message = msg;
            Size = size;
            TTL = ttl;
		}
	}
}
