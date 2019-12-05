using Lidgren.Network;

namespace Montagsmaler
{
    class ChatMessagePacket : Packet
    {
        //Order of saved information in this packet:
        //[PacketType][message]

        //Packet to transmit chat massages in game

        public string ChatMessage{ get; private set; }

        public ChatMessagePacket(string chatMessage)
        {
            DeliveryMethod = NetDeliveryMethod.ReliableOrdered;
            PacketType = PacketTypes.ChatMessage;
            ChatMessage = chatMessage;
        }

        /// <summary>
        /// Converts this packet into a message.
        /// </summary>
        /// <param name="message">A message to write in.</param>
        /// <returns>A message to send.</returns>
        public override NetOutgoingMessage ToMessage(NetOutgoingMessage message)
        {
            message.Write((int)PacketType);
            message.Write(ChatMessage);
            return message;
        }

    }
}
