using System.Collections.Generic;
using Lidgren.Network;
using System.Net;

namespace Montagsmaler
{
    class PlayerListPacket : Packet
    {
        //Order of saved information in this packet:
        //[PacketType][ListSize][PlayerList]

        public List<IPEndPoint> PlayerList { get; private set; }

        public PlayerListPacket(List<IPEndPoint> playerList)
        {
            DeliveryMethod = NetDeliveryMethod.ReliableOrdered;
            PacketType = PacketTypes.PlayerList;
            PlayerList = playerList;
        }


        /// <summary>
        /// Converts this packet into a message.
        /// </summary>
        /// <param name="message">A message to write in.</param>
        /// <returns>A message to send.</returns>
        public override NetOutgoingMessage ToMessage(NetOutgoingMessage message)
        {
            message.Write((int)PacketType);
            message.Write(PlayerList.Count);
            foreach(IPEndPoint player in PlayerList)
                message.Write(player);
            return message;
        }
    }
}
