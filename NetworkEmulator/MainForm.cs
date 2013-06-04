using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkEmulator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LocalAreaNetwork lan1 = new LocalAreaNetwork();
            
            PersonalComputer pc1 = new PersonalComputer("pc1");
            pc1.AddInterface(new EthernetController("192.168.100.1", "255.255.255.0"));
            
            PersonalComputer pc2 = new PersonalComputer("pc2");
            pc2.AddInterface(new EthernetController("192.168.100.2", "255.255.255.0"));

            PersonalComputer pc3 = new PersonalComputer("pc3");
            pc3.AddInterface(new EthernetController("192.168.100.3", "255.255.255.0"));

          
            Switch sw = new Switch(3, "sw");
            
            lan1.AddDevice(pc1);
            lan1.AddDevice(pc2);
            lan1.AddDevice(pc3);
            lan1.AddDevice(sw);

            lan1.AddLink(new WiredLink(pc1.Interfaces[0] as EthernetController, sw.Interfaces[0] as EthernetController));
            lan1.AddLink(new WiredLink(pc2.Interfaces[0] as EthernetController, sw.Interfaces[1] as EthernetController));
            lan1.AddLink(new WiredLink(pc3.Interfaces[0] as EthernetController, sw.Interfaces[2] as EthernetController));

            

            Packet p = new Packet("192.168.100.1", "192.168.100.3", "", PacketType.Ping);
            pc1.SendPacket(p);
            
            

        }
    }
}
