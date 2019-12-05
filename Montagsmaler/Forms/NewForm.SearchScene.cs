using System;
using System.Net;
using System.Windows.Forms;

namespace Montagsmaler
{
    public partial class NewForm
    {
        /// <summary>
        /// Opens the search scene, starts and prepares a client.
        /// </summary>
        private void OpenSearch()
        {
            groupSearch.SynchronizedInvoke(() =>
            {
                groupSearch.Visible = true;
                buttonSearchScanNetwork.Focus();
            });

            clientManager?.Shutdown(this, EventArgs.Empty);
            clientManager = new NetClientManager();
            clientManager.OnDiscover += AddServerToSearchListBox;
            clientManager.OnConnect += ConnectSucceded;
        }

        /// <summary>
        /// Called when a server responds to a discovery message.
        /// Adds a new found server to the listBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newServer"></param>
        private void AddServerToSearchListBox(object sender, IPEndPoint newServer)
        {
            var newIp = newServer.Address;
            if(!listBoxSearch.Items.Contains(newIp))
                listBoxSearch.SynchronizedInvoke(() => listBoxSearch.Items.Add(newIp));
        }

        /// <summary>
        /// Tells the ckientManager to broadcast a discovery message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearchNetwork_Click(object sender, EventArgs e)
        {
            listBoxSearch.Items.Clear();
            clientManager.Discover();
        }

        private void listBoxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBoxSearch.SelectedItems.Count > 0)
                textBoxSearchServerAddress.Text = listBoxSearch.SelectedItem.ToString();
        }

        /// <summary>
        /// Tells the clientManager to connect to a server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonJoinGame_Click(object sender, EventArgs e)
        {
            IPAddress ipAddress;
            if(IPAddress.TryParse(textBoxSearchServerAddress.Text, out ipAddress))
                    clientManager.Connect(ipAddress);
            else
                MessageBox.Show(Properties.strings.searchSceneWrongIp, Properties.strings.error);
        }

        /// <summary>
        /// Called when a connection attempt succeded.
        /// Opens the Lobby scene as client.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectSucceded(object sender, EventArgs e)
        {
            clientManager.OnDiscover -= AddServerToSearchListBox;
            clientManager.OnConnect -= ConnectSucceded;
            ChangeScene(SceneChanges.LobbyClient);
        }

    }
}
