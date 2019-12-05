using Lidgren.Network;
using System.Drawing;

namespace Montagsmaler
{
    class MouseMovePacket : Packet
    {
        //Order of saved information in this packet:
        //[PacketType][Point1.X][Point1.Y][Point2.X][Point2.Y]

        //Packet to transmit the previous and current position of a mouse.

        public Point P1 { get; private set; }
        public Point P2 { get; private set; }

        public MouseMovePacket(Point p1, Point p2)
        {
            DeliveryMethod = NetDeliveryMethod.UnreliableSequenced;
            PacketType = PacketTypes.MouseMove;
            this.P1 = p1;
            this.P2 = p2;
        }


        /// <summary>
        /// Converts this packet into a message.
        /// </summary>
        /// <param name="message">A message to write in.</param>
        /// <returns>A message to send.</returns>
        public override NetOutgoingMessage ToMessage(NetOutgoingMessage message)
        {
            message.Write((int)PacketType);
            message.Write(P1.X);
            message.Write(P1.Y);
            message.Write(P2.X);
            message.Write(P2.Y);
            return message;
        }
    }
}
