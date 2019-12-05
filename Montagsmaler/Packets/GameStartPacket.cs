using Lidgren.Network;

namespace Montagsmaler
{
    class GameStartPacket : Packet
    {
        //Order of saved information in this packet:
        //[PacketType]

        //Packet to announce the start of the game

        public GameStartPacket()
        {
            DeliveryMethod = NetDeliveryMethod.ReliableOrdered;
            PacketType = PacketTypes.GameStart;
        }


        /// <summary>
        /// Converts this packet into a message.
        /// </summary>
        /// <param name="message">A message to write in.</param>
        /// <returns>A message to send.</returns>
        public override NetOutgoingMessage ToMessage(NetOutgoingMessage message)
        {
            message.Write((int)PacketType);
            return message;
        }
        
            
    }
}
