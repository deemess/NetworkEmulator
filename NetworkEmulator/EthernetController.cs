﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NetworkEmulator
{
    class EthernetController: INetworkController
    {
        private string _ip;
        private string _netmask;
        private string _gateway;
        private Boolean _dynamic_ip;
        private int _speed;
        private string _mac;
        
        public INetworkDevice Device {get;set;}
         
        
       
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
            this._dynamic_ip = false;
            this._mac = GenerateMACAddress();
        }

        public string IP
        {
            get
            {
                return _ip;
            }
            set
            {
                this._ip = value;
            }
        }

        public string Netmask
        {
            get
            {
                return _netmask;
            }
            set
            {
                this._netmask = value;
            }
        }

        public string Gateway
        {
            get
            {
                return _gateway;
            }
            set
            {
                this._gateway = value;
            }
        }

        public Boolean DynamicIP
        {
            get
            {
                return _dynamic_ip;
            }
            set
            {
                this._dynamic_ip = value;
            }
        }

        public int Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                this._speed = value;
            }
        }

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
                switch (p.Message)
                {
                    case "ping":                       
                        p.Message = "pong";
                        p.DestinationIP = p.SourceIP;
                        p.SourceIP = this.IP;
                        p.TTL--;
                        this.Device.Lan.SendPacket(p, this);
                        break;
                    case "pong":
                        System.Windows.Forms.MessageBox.Show("PING: \n" + p.DestinationIP + " -> " + p.SourceIP);
                        break;
                    default:
                        System.Windows.Forms.MessageBox.Show("Неизвестный пакет");
                        break;
                }
                
                else
                    SendPacket(p);
        }
        
        public void SendPacket(Packet p)
        {
            Device.SendPacket(p, this);
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
