using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Lidgren.Network;

namespace Montagsmaler
{
    public class NetServerManager : NetManager
    {
        private readonly NetServer server;
        private bool gameRunning;
        private int currentPlayerIndex = -1;
        private int lastCorrectPlayerIndex = -1;
        private NetConnection currentPlayer;
        private int roundCount = 1;
        private System.Timers.Timer turnTimer;
        private string guessWord;
        private readonly Dictionary<EndPoint, int> playerPoints;

        public event EventHandler OnPortError;

        /// <summary>
        /// Configures the server.
        /// </summary>
        public NetServerManager()
        {
            Config.Port = Variables.ServerPort;
            Config.EnableMessageType(NetIncomingMessageType.DiscoveryRequest);
            Config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            Config.EnableMessageType(NetIncomingMessageType.StatusChanged);
            Config.ConnectionTimeout = Variables.ServerManagerTimeout;

            server = new NetServer(Config);
            
            //players score is saved in a dictionary
            playerPoints = new Dictionary<EndPoint, int>();
        }

        /// <summary>
        /// Starts the server.
        /// </summary>
        public void StartServer()
        {
            try
            {
                server.Start();
                Debug.WriteLine(Properties.strings.serverManagerStarted, server.Port);
                Listener = new NetListener(server, this);
                new Thread(Listener.ProcessMessages).Start();
            }
            catch (SocketException e)   
            {
                //Port already in use
                Debug.WriteLine(Properties.strings.portError);
                OnPortError?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Shuts down the server and its listener.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public override void Shutdown(object sender, EventArgs args)
        {
            if(Listener != null)
                Listener.Listening = false;
            server.Shutdown(Properties.strings.serverManagerShutdown);
        }

        /// <summary>
        /// Sends a packet to all clients.
        /// </summary>
        /// <param name="packet">The packet to send</param>
        private void BroadcastPacket(Packet packet)
        {
            var deliveryMethod = packet.DeliveryMethod;
            var msg = packet.ToMessage(server.CreateMessage());
            server.SendToAll(msg, deliveryMethod);
        }

        /// <summary>
        /// Sends a packet to one client.
        /// </summary>
        /// <param name="packet">The packet to send</param>
        /// <param name="connection">The connection to send to</param>
        private void SendPacket(Packet packet, NetConnection connection)
        {
            var deliveryMethod = packet.DeliveryMethod;
            var msg = packet.ToMessage(server.CreateMessage());
            connection.SendMessage(msg, deliveryMethod, 0);
        }

        /// <summary>
        /// Called when a discover message is recieved.
        /// Sends back a response if game's not running.
        /// </summary>
        /// <param name="msg">DiscoverMessage from client</param>
        public void OnDiscoverRequest(NetIncomingMessage msg)
        {
            if(!gameRunning)
                server.SendDiscoveryResponse(null, msg.SenderEndPoint);
        }

        /// <summary>
        /// Called when a connection approval message is recieved from a client.
        /// Sends back approval for a client to connect if the game's not running.
        /// </summary>
        /// <param name="msg">Connectioon approval message from client</param>
        public void OnConnectionApproval(NetIncomingMessage msg)
        {
            var newClient = msg.SenderConnection;
            if (!gameRunning) newClient.Approve();
            else newClient.Deny();
        }

        /// <summary>
        /// Called when a client lost the connection.
        /// Sends a updated playerlist to all clients.
        /// </summary>
        /// <param name="msg">Disconnected message from client.</param>
        protected override void Disconnected(NetIncomingMessage msg)
        {
            BroadcastPlayerListUpdate();
        }

        /// <summary>
        /// Called when a client connected.
        /// Sends a updated playerlist to all clients.
        /// </summary>
        /// <param name="msg">Connected message from client.</param>
        protected override void Connected(NetIncomingMessage msg)
        {
            playerPoints.Add(msg.SenderEndPoint, 0);
            BroadcastPlayerListUpdate();
        }

        /// <summary>
        /// Sends a new playerlist to all clients.
        /// </summary>
        private void BroadcastPlayerListUpdate()
        {
            var playerList = new List<IPEndPoint>();
            playerList.AddRange(server.Connections.Select(connection => connection.RemoteEndPoint));
            var playerListPacket = new PlayerListPacket(playerList);
            BroadcastPacket(playerListPacket);
        }

        /// <summary>
        /// Called when a packet id recieved.
        /// Handles all packets.
        /// </summary>
        /// <param name="packet">The packet to handle.</param>
        public override void RecievePacket(Packet packet)
        {
            switch (packet.PacketType)
            {
                case PacketTypes.MouseMove:
                    HandleMouseMove(packet);
                    break;

                case PacketTypes.ChatMessage:
                    HandleChatMessage((ChatMessagePacket) packet);
                    break;

                default:
                    Debug.WriteLine(Properties.strings.managerUnhandledPacketType, packet.PacketType);
                    break;
            }
        }

        /// <summary>
        /// Starts the game and tells every client.
        /// </summary>
        public void StartGame()
        {
            gameRunning = true;
            BroadcastPacket(new GameStartPacket());
            StartNewTurn();
        }

        /// <summary>
        /// Starts a new turn.
        /// </summary>
        private void StartNewTurn()
        {
            //Gets a new random word for the players to guess
            guessWord = WordList.GetRandomWord();

            if(lastCorrectPlayerIndex >= 0)
                BroadcastPacket(new NewTurnPacket(lastCorrectPlayerIndex, currentPlayerIndex));     //Somebody guessed the word last round
            else
                BroadcastPacket(new NewTurnPacket(-1, -1));                                         //Nobody guessed the word last round

            //set current player and reset last correct player
            lastCorrectPlayerIndex = -1;
            currentPlayerIndex++;
            //If every player has played the next round begins
            if (currentPlayerIndex < 0 || currentPlayerIndex >= server.ConnectionsCount)
            {
                currentPlayerIndex = 0;
                roundCount++;
            }
            if (server.ConnectionsCount > 0)
                currentPlayer = server.Connections[currentPlayerIndex];


            //Check if all rounds have been played
            if (roundCount > Variables.RoundsMax)
                EndGame();
            else
            {
                //Start the countdown for this turn
                turnTimer = new System.Timers.Timer {Interval = Variables.RoundTime*1000};
                turnTimer.Elapsed += TurnTimeElapsed;
                turnTimer.Start();

                //Tell every player the next turn begins
                var nextPlayerMessagePacket = new ChatMessagePacket(string.Format(Properties.strings.serverManagerNextPlayerMessage, currentPlayer.RemoteEndPoint));
                BroadcastPacket(nextPlayerMessagePacket);
                //Tell the drawing player what the searched word is
                var guessWordMessagePacket = new ChatMessagePacket(string.Format(Properties.strings.serverManagerGuessWordMessage, guessWord));
                SendPacket(guessWordMessagePacket, currentPlayer);
            }

        }

        /// <summary>
        /// Called when the time for this turn elapsed.
        /// Tells all clients and starts the next turn.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TurnTimeElapsed(object sender, EventArgs e)
        {
            var chatMessagePacket = new ChatMessagePacket(Properties.strings.serverManagerTimeOverMessage);
            BroadcastPacket(chatMessagePacket);
            turnTimer.Close();
            StartNewTurn();
        }

        /// <summary>
        /// Called when all rounds have been played.
        /// Tells all client  ths the game has ended and announces the winner(s).
        /// </summary>
        private void EndGame()
        {
            BroadcastPacket(new GameEndPacket());
            gameRunning = false;
            turnTimer.Close();
            var winningPlayers = playerPoints.Where(x => x.Value == playerPoints.Values.Max());
            string chatMessage = Properties.strings.serverManagerWinMessage;
            chatMessage = winningPlayers.Aggregate(chatMessage, (current, player) => current + (player.Key + "\n"));
            var chatMessagePacket = new ChatMessagePacket(chatMessage);
            BroadcastPacket(chatMessagePacket);
        }

        #region PacketHandler

        /// <summary>
        /// Handles a MouseMovePacket.
        /// Sends it to all clients if it's from the current player.
        /// </summary>
        /// <param name="packet"></param>
        private void HandleMouseMove(Packet packet)
        {
            if (packet.Sender.Equals(currentPlayer.RemoteEndPoint) && gameRunning)
                BroadcastPacket(packet);
        }

        /// <summary>
        /// Handles a ChatNessagePacket.
        /// Checks if it's the searched word. If it is the players get points and the next turn starts
        /// </summary>
        /// <param name="packet"></param>
        private void HandleChatMessage(ChatMessagePacket packet)
        {
            BroadcastPacket(packet);

            //Check if it's the searched word
            if (packet.ChatMessage.ToLower().Equals(guessWord.ToLower()) && !packet.Sender.Equals(currentPlayer.RemoteEndPoint) && gameRunning)
            {
                //Saves the winning player to announce in the next turn.
                var lastCorrectPlayer = server.Connections.SingleOrDefault(i => i.RemoteEndPoint.Equals(packet.Sender));  //finds connection by ip
                playerPoints[lastCorrectPlayer.RemoteEndPoint] += Variables.PointsPlayerGuess;
                playerPoints[currentPlayer.RemoteEndPoint] += Variables.PointsPlayerDraw;
                var chatMessage = new ChatMessagePacket(string.Format(Properties.strings.serverManagerPlayerGuessedWordMessage, lastCorrectPlayer.RemoteEndPoint));
                lastCorrectPlayerIndex = server.Connections.IndexOf(lastCorrectPlayer);
                turnTimer.Close();
                BroadcastPacket(chatMessage);
                StartNewTurn();
            }
        }

        #endregion

    }
}
