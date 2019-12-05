using Lidgren.Network;
using System.Diagnostics;
using System.Threading;

namespace Montagsmaler
{
    //Klasse um Daten sowohl von Server als auch von Client in Empfang zu nehmen
    public class NetListener
    {
        private readonly NetPeer peer;
        private readonly NetManager manager;
        public bool Listening { get; set;}

        public NetListener(NetPeer peer, NetManager manager)
        {
            Listening = true;
            this.peer = peer;
            this.manager = manager;
        }

        /// <summary>
        /// Always checks if there's a incoming message and tells the manager.
        /// </summary>
        public void ProcessMessages()
        {
            while (Listening)
            {
                Thread.Sleep(1);
                NetIncomingMessage msg;
                while ((msg = peer.ReadMessage()) != null)
                {
                    switch (msg.MessageType)
                    {

                        //Server only Messages   
                        case NetIncomingMessageType.DiscoveryRequest:
                            ((NetServerManager)manager).OnDiscoverRequest(msg);
                            break;

                        case NetIncomingMessageType.ConnectionApproval:
                            ((NetServerManager)manager).OnConnectionApproval(msg);
                            break;


                        //Client only Messages
                        case NetIncomingMessageType.DiscoveryResponse:
                            ((NetClientManager)manager).OnDiscoverResponse(msg);
                            break;


                        //Server and Client Messages
                        case NetIncomingMessageType.Data:   //Data messages are converted in packets and then transmitted further
                            var packet = Packet.ToPacket(msg);
                            manager.RecievePacket(packet);
                            break;

                        case NetIncomingMessageType.StatusChanged:
                            manager.StatusChange(msg);
                            break;

                            //Error Messages
                        case NetIncomingMessageType.VerboseDebugMessage:
                        case NetIncomingMessageType.DebugMessage:
                        case NetIncomingMessageType.WarningMessage:
                        case NetIncomingMessageType.ErrorMessage:
                            Debug.WriteLine(msg.ReadString());
                            break;

                        default:
                            Debug.WriteLine(Properties.strings.listenerUnhandledMessage, msg.MessageType);
                            break;
                    }
                    peer.Recycle(msg);
                }
            }
            Debug.WriteLine(Properties.strings.listenerThreadFinished);

        }


    }
}
