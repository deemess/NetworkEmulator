using System;
using System.Text;

namespace NetworkEmulator
{
	/// <summary>
	/// Description of IP.
	/// </summary>
	public static class IP
	{
		static IP()
		{
			
		}
		
		public static bool CompareBySubnets(string ip1, string ip2, string netmask)
		{
			return GetSubnet(ip1, netmask) == GetSubnet(ip2, netmask);
		}
		
		public static string GetSubnet(string ip, string netmask)
		{
			uint uip = ToUInt(ip);
			uint unm = ToUInt(netmask);
			return ToString(uip & unm);
		}
		
		public static uint ToUInt(string ip)
		{
			string[] parts = ip.Split('.');
			
			uint u1 = Convert.ToUInt32(parts[3]);
			uint u2 = Convert.ToUInt32(parts[2]);
			uint u3 = Convert.ToUInt32(parts[1]);
			uint u4 = Convert.ToUInt32(parts[0]);
			
			return (u1 | (u2 << 8) | (u3 << 16) | (u4 << 24));
		}
		
		public static string ToString(uint ip)
		{
			StringBuilder b = new StringBuilder();
			
			b.Append((ip >> 24).ToString());
			b.Append(".");
			b.Append(((ip >> 16) & 0xFF).ToString());
			b.Append(".");
			b.Append(((ip >> 8) & 0xFF).ToString());
			b.Append(".");
			b.Append((ip & 0xFF).ToString());
			
			return b.ToString();
		}
	}
}
