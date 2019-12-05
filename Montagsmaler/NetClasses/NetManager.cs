using Lidgren.Network;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Montagsmaler
{
    public abstract class NetManager
    {
        protected NetPeerConfiguration Config;
        protected NetListener Listener;

        public abstract void RecievePacket(Packet packet);
        public abstract void Shutdown(object sender, EventArgs args);
        protected abstract void Connected(NetIncomingMessage msg);
        protected abstract void Disconnected(NetIncomingMessage msg);

        protected NetManager()
        {
            Config = new NetPeerConfiguration(Variables.NetIdentifier);
            AppDomain.CurrentDomain.ProcessExit += Shutdown;    //Sockets will be closed when the program is being exited in any way
            Application.ApplicationExit += Shutdown;
        }

        /// <summary>
        /// Handles StatusChangedMessages
        /// </summary>
        /// <param name="msg"></param>
        public void StatusChange(NetIncomingMessage msg)
        {
            NetConnectionStatus status = (NetConnectionStatus)msg.ReadByte();
            switch (status)
            {
                case NetConnectionStatus.Disconnected:
                    Disconnected(msg);
                    break;

                case NetConnectionStatus.Connected:
                    Connected(msg);
                    break;

                default:
                    Debug.WriteLine(string.Format(Properties.strings.managerUnhandledStatusChange, status.ToString()));
                    break;
            }
        }



    }
}
