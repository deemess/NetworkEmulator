using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEmulator
{
	class WiredLink: ILink<EthernetController>
    {
        private EthernetController _point1;
        private EthernetController _point2;
        public int Speed { get; set; }
        public int Length { get; set; } 


        /// <summary>
        /// Конструктор класса WiredLink. Создается связь между двумя проводными сетевыми интерфейсами.
        /// </summary>
        /// <param name="p1">Сетевой интерфейс</param>
        /// <param name="p2">Сетевой интерфейс</param>
        /// <param name="length">Длина кабеля</param>
        public WiredLink(EthernetController p1, EthernetController p2, int length = 2)
        {
            this._point1 = p1;
            this._point2 = p2;
            this.Speed = this.MinSpeed();
            this.Length = length;
            p1.Link = this;
            p2.Link = this;
            
        }

        public EthernetController Point1
        {
            get
            {
                return _point1;
            }
        }

        public EthernetController Point2
        {
            get
            {
                return _point2;
            }
        }


        private int MinSpeed()
        {
            //Устанавливаем скорость канала равной минимальной скорости из двух интерфейсов
            if (this._point1.Speed < this._point2.Speed)
                return this._point1.Speed;
            else
                return this._point2.Speed;
        }
        
        public void RecievePacket(Packet p, INetworkController iface)
        {
        	this.SendPacket(p, iface);
        }
        
        private void SendPacket(Packet p, INetworkController iface)
        {
        	EthernetController target;
        	target = (iface == this.Point1) ? this.Point2 : this.Point1;
        	target.ReceivePacket(p);
        }

    }
}
