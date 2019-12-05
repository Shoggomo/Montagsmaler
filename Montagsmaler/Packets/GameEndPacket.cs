using Lidgren.Network;

namespace Montagsmaler
{
    class GameEndPacket : Packet
    {
        //Order of saved information in this packet:
        //[PacketType]

        //Packet to announce the end of the game

        public GameEndPacket()
        {
            DeliveryMethod = NetDeliveryMethod.ReliableOrdered;
            PacketType = PacketTypes.GameEnd;
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
