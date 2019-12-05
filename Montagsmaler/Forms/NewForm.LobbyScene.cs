using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace Montagsmaler
{
    public partial class NewForm
    {

        /// <summary>
        /// Opens the Lobby Scene, starts and prepares the server, starts and prepares a client and connects it to the server.
        /// </summary>
        private void OpenLobbyHost()
        {
            //Initialize Scene
            groupLobby.SynchronizedInvoke(() =>
            {
                groupLobby.Visible = true;
                buttonLobbyStartGame.Enabled = true;
                buttonLobbyStartGame.Focus();
            });

            //Initialize clientManager
            clientManager?.Shutdown(this, EventArgs.Empty);      //Reset Client
            clientManager = new NetClientManager();
            clientManager.OnPlayerListChanged += UpdatePlayerListBoxInLobby;
            clientManager.OnDisconnect += ConnectionToServerLostInLobby;
            clientManager.OnGameStart += ClientStartGame;

            //Initialize serverManager
            serverManager?.Shutdown(this, EventArgs.Empty);      //Reset Server
            serverManager = new NetServerManager();
            serverManager.OnPortError += PortError;
            serverManager.StartServer();

            clientManager.Connect(IPAddress.Parse("127.0.0.1"));

        }

        /// <summary>
        /// Opens the Lobby Scene and prepares a client.
        /// </summary>
        private void OpenLobbyClient()
        {
            groupLobby.SynchronizedInvoke(() =>
            {
                groupLobby.Visible = true;
                buttonLobbyStartGame.Enabled = false;
            });
            clientManager.OnPlayerListChanged += UpdatePlayerListBoxInLobby;
            clientManager.OnDisconnect += ConnectionToServerLostInLobby;
            clientManager.OnGameStart += ClientStartGame;
        }

        /// <summary>
        /// Updates the current playerList in a listBox with a new one recieved from the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newPlayerList"></param>
        private void UpdatePlayerListBoxInLobby(object sender, List<IPEndPoint> newPlayerList)
        {
            listBoxLobby.SynchronizedInvoke(() => listBoxLobby.Items.Clear());

            foreach (var newPlayer in newPlayerList)
            {
                listBoxLobby.SynchronizedInvoke(() => listBoxLobby.Items.Add(newPlayer));

            }
        }

        /// <summary>
        /// Called when the server port is already in use.
        /// Opens the opening Scene.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PortError(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.strings.portError, Properties.strings.error);
            ChangeScene(SceneChanges.Opening);
        }

        /// <summary>
        /// Called when a client losses its connection while being in the lobby.
        /// Opens the opening scene.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionToServerLostInLobby(object sender, EventArgs e)
        {
            clientManager.OnPlayerListChanged -= UpdatePlayerListBoxInLobby;
            clientManager.OnDisconnect -= ConnectionToServerLostInLobby;
            clientManager.OnGameStart -= ClientStartGame;
            clientManager.Shutdown(this, EventArgs.Empty);
            ChangeScene(SceneChanges.Opening);
            MessageBox.Show(Properties.strings.lostConnectionToServer, Properties.strings.error);
        }

        /// <summary>
        /// Called when the server started the game.
        /// Opens the game scene.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientStartGame(object sender, EventArgs e)
        {
            clientManager.OnPlayerListChanged -= UpdatePlayerListBoxInLobby;
            clientManager.OnDisconnect -= ConnectionToServerLostInLobby;
            clientManager.OnGameStart -= ClientStartGame;
            ChangeScene(SceneChanges.Game);
        }

        /// <summary>
        /// Tells the server to start the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            serverManager.StartGame();
        }

    }
}
