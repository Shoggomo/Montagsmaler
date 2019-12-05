using Lidgren.Network;

namespace Montagsmaler
{
    class NewTurnPacket : Packet
    {
        //Order of saved information in this packet:
        //[PacketType][PlayerIndex who guessed the word][PlayerIndex who drew]

        //Packet to announce a new turn.

        public int GuesserPlayerIndex { get; private set; }
        public int DrawerPlayerIndex { get; private set; }

        public NewTurnPacket(int guesserPlayer, int drawerPlayer)
        {
            DeliveryMethod = NetDeliveryMethod.ReliableOrdered;
            PacketType = PacketTypes.NewTurn;
            GuesserPlayerIndex = guesserPlayer;
            DrawerPlayerIndex = drawerPlayer;
        }


        /// <summary>
        /// Converts this packet into a message.
        /// </summary>
        /// <param name="message">A message to write in.</param>
        /// <returns>A message to send.</returns>
        public override NetOutgoingMessage ToMessage(NetOutgoingMessage message)
        {
            message.Write((int)PacketType);
            message.Write(GuesserPlayerIndex);
            message.Write(DrawerPlayerIndex);
            return message;
        }
        
            
    }
}
