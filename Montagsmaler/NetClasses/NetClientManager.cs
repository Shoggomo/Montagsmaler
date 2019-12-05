using System;
using System.Collections.Generic;
using System.Net;
using System.Diagnostics;
using System.Threading;
using Lidgren.Network;
using System.Drawing;

namespace Montagsmaler
{
    public class NetClientManager : NetManager
    {
        private readonly NetClient client;

        public event EventHandler<IPEndPoint> OnDiscover;
        public event EventHandler<List<IPEndPoint>> OnPlayerListChanged;
        public event EventHandler OnDisconnect;
        public event EventHandler OnConnect;
        public event EventHandler OnGameStart;
        public event EventHandler<MouseMoveEventArgs> OnMouseMove;
        public event EventHandler<string> OnNewChatMessage;
        public event EventHandler<NewTurnEventArgs> OnNewTurn;
        public event EventHandler OnGameEnd;

        /// <summary>
        /// Configures and starts a new client and listenerThread.
        /// </summary>
        public NetClientManager()
        {
            Config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);
            Config.ConnectionTimeout = Variables.ClientManagerTimeout;   //Timeout after 7 Seconds
            
            client = new NetClient(Config);
            client.Start();
            Debug.WriteLine(Properties.strings.clientManagerStarted);
            Listener = new NetListener(client, this);
            new Thread(Listener.ProcessMessages).Start();
        }

        /// <summary>
        /// Broadcasts a discovery message.
        /// </summary>
        public void Discover()
        {
            client.DiscoverLocalPeers(Variables.ServerPort);
        }

        /// <summary>
        /// Called when a server responds to a discover message.
        /// </summary>
        /// <param name="msg"></param>
        public void OnDiscoverResponse(NetIncomingMessage msg)
        {
         OnDiscover?.Invoke(this, msg.SenderEndPoint);
        }

        /// <summary>
        /// Sends a connection attempt to a server.
        /// </summary>
        /// <param name="ipAddress"></param>
        public void Connect(IPAddress ipAddress)
        {
            var endPoint = new IPEndPoint(ipAddress, Variables.ServerPort);
            client.Connect(endPoint);
        }

        /// <summary>
        /// Shutsdown the client and listenerThread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public override void Shutdown(object sender, EventArgs args)
        {
            Listener.Listening = false;
            client.Shutdown(Properties.strings.clientManagerShutdown);
        }

        /// <summary>
        /// Called when the connection to the server is lost.
        /// </summary>
        /// <param name="msg"></param>
        protected override void Disconnected(NetIncomingMessage msg)
        {
            Debug.WriteLine(Properties.strings.clientManagerDisconnect);
            OnDisconnect?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when a connection attampt was successful.
        /// </summary>
        /// <param name="msg"></param>
        protected override void Connected(NetIncomingMessage msg)
        {
            Debug.WriteLine(Properties.strings.clientManagerConnect);
            OnConnect?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Sends a packet to the server.
        /// </summary>
        /// <param name="packet"></param>
        private void SendPacket(Packet packet)
        {
            var deliveryMethod = packet.DeliveryMethod;
            var msg = packet.ToMessage(client.CreateMessage());
            client.SendMessage(msg, deliveryMethod);
        }

        /// <summary>
        /// Sends a MouseMovePacket to the server.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        public void SendMouseMove(Point p1, Point p2)
        {
            var packet = new MouseMovePacket(p1, p2);
            SendPacket(packet);
        }

        /// <summary>
        /// Sends a ChatMessagePacket to the server.
        /// </summary>
        /// <param name="chatMessage"></param>
        public void SendChatMessage(string chatMessage)
        {
            var packet = new ChatMessagePacket(chatMessage);
            SendPacket(packet);
        }

        /// <summary>
        /// Handles all incoming packets.
        /// </summary>
        /// <param name="packet"></param>
        public override void RecievePacket(Packet packet)
        {
            switch (packet.PacketType)
            {
                case PacketTypes.PlayerList:
                    HandlePlayerList(packet);
                    break;

                case PacketTypes.MouseMove:
                    HandleMouseMove(packet);
                    break;

                case PacketTypes.GameStart:
                    HandleGameStart();
                    break;

                case PacketTypes.ChatMessage:
                    HandleChatMessage((ChatMessagePacket)packet);
                    break;

                case PacketTypes.NewTurn:
                    HandleNewTurn((NewTurnPacket)packet);
                    break;

                case PacketTypes.GameEnd:
                    HandleGameEnd();
                    break;

                default:
                    Debug.WriteLine(Properties.strings.managerUnhandledPacketType, packet.PacketType);
                    break;
            }
        }

        

        #region PacketHandler

        /// <summary>
        /// Handles a PlayerListPacket.
        /// </summary>
        /// <param name="packet"></param>
        private void HandlePlayerList(Packet packet)
        {
            OnPlayerListChanged?.Invoke(this, ((PlayerListPacket)packet).PlayerList);
        }

        /// <summary>
        /// Handles a MouseMovePacket.
        /// </summary>
        /// <param name="packet"></param>
        private void HandleMouseMove(Packet packet)
        {
            var mouseMovePacket = (MouseMovePacket)packet;
            var args = new MouseMoveEventArgs(mouseMovePacket.P1, mouseMovePacket.P2);
            OnMouseMove?.Invoke(this, args);
        }

        /// <summary>
        /// Handles a GameStartPacket.
        /// </summary>
        private void HandleGameStart()
        {
            OnGameStart?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Handles a ChatMessagePacket.
        /// </summary>
        /// <param name="packet"></param>
        private void HandleChatMessage(ChatMessagePacket packet)
        {
            OnNewChatMessage?.Invoke(this, packet.ChatMessage);
        }

        /// <summary>
        /// Handles a NewTurnPacket.
        /// </summary>
        /// <param name="packet"></param>
        private void HandleNewTurn(NewTurnPacket packet)
        {
            var args = new NewTurnEventArgs(packet.GuesserPlayerIndex, packet.DrawerPlayerIndex);
            OnNewTurn?.Invoke(this, args);
        }

        /// <summary>
        /// Handles a GameEndPacket.
        /// </summary>
        private void HandleGameEnd()
        {
            OnGameEnd?.Invoke(this, EventArgs.Empty);
        }

        #endregion

    }
}
