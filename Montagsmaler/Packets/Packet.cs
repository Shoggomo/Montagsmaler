using Lidgren.Network;
using System.Collections.Generic;
using System.Drawing;
using System.Net;

namespace Montagsmaler
{
    public abstract class Packet
    {
        public NetDeliveryMethod DeliveryMethod { get; protected set; }
        public PacketTypes PacketType { get; protected set; }
        public EndPoint Sender { get; private set; }

        public abstract NetOutgoingMessage ToMessage(NetOutgoingMessage message);

        /// <summary>
        /// Converts a message into a packet.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Packet ToPacket(NetIncomingMessage message)
        {
            int pType;
            if(message.ReadInt32(out pType))
            {
                switch((PacketTypes)pType)
                {
                    case PacketTypes.PlayerList:
                        return ToPlayerListPacket(message);

                    case PacketTypes.MouseMove:
                        return ToMouseMovePacket(message);

                    case PacketTypes.GameStart:
                        return ToGameStartPacket(message);

                    case PacketTypes.ChatMessage:
                        return ToChatMessagePacket(message);

                    case PacketTypes.NewTurn:
                        return ToNewTurnPacket(message);

                    case PacketTypes.GameEnd:
                        return ToGameEndPacket(message);
                }
            }
            else
            {
                throw new System.ArgumentException(string.Format(Properties.strings.packetUnknownPacketType, pType));
            }
            return null;
        }

        #region ToPacket Methods

        /// <summary>
        /// Converts a message into a NewTurnPacket.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static Packet ToNewTurnPacket(NetIncomingMessage message)
        {
            int guesserPlayerIndex = message.ReadInt32();
            int drawerPlayerIndex = message.ReadInt32();
            var newTurnPacket = new NewTurnPacket(guesserPlayerIndex, drawerPlayerIndex) { Sender = message.SenderEndPoint };
            return newTurnPacket;
        }

        /// <summary>
        /// Converts a message into a PlayerListPacket.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static Packet ToPlayerListPacket(NetIncomingMessage message)
        {
            int listSize = message.ReadInt32();
            var playerList = new List<IPEndPoint>();
            for (int i = 0; i < listSize; i++)
                playerList.Add(message.ReadIPEndPoint());
            var playerListPacket = new PlayerListPacket(playerList) {Sender = message.SenderEndPoint};
            return playerListPacket;
        }

        /// <summary>
        /// Converts a message into a MouseMovePacket.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static Packet ToMouseMovePacket(NetIncomingMessage message)
        {
            int x1 = message.ReadInt32();
            int y1 = message.ReadInt32();
            int x2 = message.ReadInt32();
            int y2 = message.ReadInt32();
            var p1 = new Point(x1, y1);
            var p2 = new Point(x2, y2);
            var mouseMovePacket = new MouseMovePacket(p1, p2) {Sender = message.SenderEndPoint};
            return mouseMovePacket;
        }

        /// <summary>
        /// Converts a message into a GameStartPacket.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static Packet ToGameStartPacket(NetIncomingMessage message)
        {
            var gameStartPacket = new GameStartPacket { Sender = message.SenderEndPoint };
            return gameStartPacket;
        }

        /// <summary>
        /// Converts a message into a GameEndPacket.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static Packet ToGameEndPacket(NetIncomingMessage message)
        {
            var gameEndPacket = new GameEndPacket() { Sender = message.SenderEndPoint };
            return gameEndPacket;
        }

        /// <summary>
        /// Converts a message into a ChatMessagePacket.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static Packet ToChatMessagePacket(NetIncomingMessage message)
        {
            var chatMessagePacket = new ChatMessagePacket(message.ReadString()) {Sender = message.SenderEndPoint};
            return chatMessagePacket;
        }

        #endregion

    }
}
