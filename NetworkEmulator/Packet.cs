using System;
using System.Text;

namespace NetworkEmulator
{
	public enum PacketType
	{
		Ping,
		Pong,
		Message
	}
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
        public PacketType Type { get; set; }
		
        /// <summary>
        /// Создание сетевого пакета
        /// </summary>
        /// <param name="srcIP">IP-адрес источника</param>
        /// <param name="destIP">IP-адрес получателя</param>
        /// <param name="msg">Сообщение</param>
        /// <param name="size">Размер пакета в килобайтах</param>
		public Packet(string srcIP, string destIP, string msg, PacketType t, uint size = 64, uint ttl = 50)
		{
			SourceIP = srcIP;
			DestinationIP = destIP;
			Message = msg;
            Size = size;
            TTL = ttl;
            Type = t;
		}
		
		public void Dump()
		{
			StringBuilder b = new StringBuilder();
			b.Append("Source IP: ");
			b.AppendLine(SourceIP);
			b.Append("Destination IP: ");
			b.AppendLine(DestinationIP);
			b.Append("TTL: ");
			b.AppendLine(TTL.ToString());
			b.Append("Type: ");
			b.AppendLine(Type.ToString());
			b.Append("Message: ");
			b.Append(Message);
			System.Windows.Forms.MessageBox.Show(b.ToString());
		}
	}
}
