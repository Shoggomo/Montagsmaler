using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Montagsmaler
{
    public partial class NewForm
    {
        private Graphics graphics;
        private bool mouseDown;
        private Point mouse, previousMouse;
        private Pen pen;
        private double timeLeft;

        private void OpenGame()
        {
            PrepareClientForGame();
            InitGameForm();
        }

        /// <summary>
        /// Prepares the clientManager for events in Game.
        /// </summary>
        private void PrepareClientForGame()
        {
            clientManager.OnMouseMove += DrawLine;
            clientManager.OnDisconnect += ConnectionToServerLostInGame;
            clientManager.OnPlayerListChanged += UpdatePlayerListBoxInGame;
            clientManager.OnNewChatMessage += UpdateChat;
            clientManager.OnNewTurn += NewTurnStarted;
            clientManager.OnGameEnd += GameEnded;
        }

        /// <summary>
        /// Initializes the Game window
        /// </summary>
        private void InitGameForm()
        {
            textBoxGameChat.BackColor = Color.White;
            textBoxGameChat.Clear();
            groupGame.SynchronizedInvoke(() =>
            {
                groupGame.Visible = true;
                buttonGameSend.Focus();
                listBoxGame.Items.Clear();
                listBoxGameScores.Items.Clear();
                listBoxGame.Items.AddRange(listBoxLobby.Items);
                for (int i = 0; i < listBoxLobby.Items.Count; i++)
                    listBoxGameScores.Items.Add("0");
            });
            mouse = new Point();
            previousMouse = new Point();
            pen = new Pen(Color.Black);
            graphics = graphics ?? panelGame.CreateGraphics();
            graphics.Clear(Color.White);
            timerGameSendData.Start();
            timerGameCountdown.Start();
        }

        /// <summary>
        /// Shutsdown clientManager and changes to opening window when disconnected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectionToServerLostInGame(object sender, EventArgs e)
        {
            clientManager.OnMouseMove -= DrawLine;
            clientManager.OnDisconnect -= ConnectionToServerLostInGame;
            clientManager.OnPlayerListChanged -= UpdatePlayerListBoxInGame;
            clientManager.OnNewChatMessage -= UpdateChat;
            clientManager.OnNewTurn -= NewTurnStarted;
            clientManager.OnGameEnd -= GameEnded;

            clientManager.Shutdown(this, EventArgs.Empty);
            ChangeScene(SceneChanges.Opening);
            MessageBox.Show(Properties.strings.lostConnectionToServer, Properties.strings.error);
        }

        private void panelGame_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void panelGame_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void panelGame_MouseMove(object sender, MouseEventArgs e)
        {
            mouse.X = e.X;
            mouse.Y = e.Y;
        }

        /// <summary>
        /// Tells the clientManager to send the last mouse movement when the user is drawing in the panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerGame_Tick(object sender, EventArgs e)
        {
            if (mouseDown)
            {
                clientManager.SendMouseMove(mouse, previousMouse);
            }
            previousMouse.X = mouse.X;
            previousMouse.Y = mouse.Y;
        }

        /// <summary>
        /// Tells the clientManager to send the input message to the server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGameSend_Click(object sender, EventArgs e)
        {
			clientManager.SendChatMessage(textBoxGameChatInput.Text);
            textBoxGameChat.SynchronizedInvoke(() => textBoxGameChatInput.Clear());
        }

        private void textBoxGameChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                buttonGameSend_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// Draws a line form the previous mouse position to the current.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DrawLine(object sender, MouseMoveEventArgs args)
        {
            graphics.DrawLine(pen, args.P1, args.P2);
        }


        /// <summary>
        /// Replaces the current list if IP Addresses with a new, while correcting the score list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newPlayerList">The new list to insert</param>
        private void UpdatePlayerListBoxInGame(object sender, List<IPEndPoint> newPlayerList)
        {
            //save old lists
            List<string> oldPlayers = listBoxGame.Items.Cast<string>().ToList();
            List<int> oldScores = listBoxGameScores.Items.Cast<int>().ToList();

            listBoxGame.SynchronizedInvoke(() => listBoxGame.Items.Clear());
            listBoxGameScores.SynchronizedInvoke(() => listBoxGameScores.Items.Clear());
            foreach (var newPlayer in newPlayerList)
            {
                string newIp = newPlayer.ToString();
                listBoxGame.SynchronizedInvoke(() => listBoxGame.Items.Add(newIp));

                //check if ip already existed and handle score list
                if (oldPlayers.Contains(newIp))
                {
                    var indexOfScore = oldPlayers.IndexOf(newIp);
                    listBoxGameScores.SynchronizedInvoke(() => listBoxGameScores.Items.Add(oldScores[indexOfScore]));
                }
                else
                {
                    listBoxGameScores.SynchronizedInvoke(() => listBoxGameScores.Items.Add(0));
                }
            }
        }


        /// <summary>
        /// Adds Text to the chatBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="chatMessage">Message to add</param>
        private void UpdateChat(object sender, string chatMessage)
        {
            textBoxGameChat.SynchronizedInvoke(() =>
            {
                textBoxGameChat.AppendText(chatMessage + "\r\n");
            });

        }

        /// <summary>
        /// Prepares a new Turn in game:
        /// Adds points if a player won last turn and clears the panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Used to tell which player won last round.</param>
        private void NewTurnStarted(object sender, NewTurnEventArgs e)
        {

            if (e.GuesserPlayerIndex >= 0)
            {
                var newScoreGuesser = int.Parse(listBoxGameScores.Items[e.GuesserPlayerIndex].ToString()) + Variables.PointsPlayerGuess;
                listBoxGameScores.SynchronizedInvoke(() => listBoxGameScores.Items[e.GuesserPlayerIndex] = newScoreGuesser); 
                var newScoreDrawer = int.Parse(listBoxGameScores.Items[e.DrawerPlayerIndex].ToString()) + Variables.PointsPlayerDraw;
                listBoxGameScores.SynchronizedInvoke(() => listBoxGameScores.Items[e.DrawerPlayerIndex] = newScoreDrawer);
            }

            timeLeft = Variables.RoundTime;
            panelGame.SynchronizedInvoke(() => graphics.Clear(Color.White));
        }

        /// <summary>
        /// Counts down the timerLabel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerGameCountdown_Tick(object sender, EventArgs e)
        {
            timeLeft -= timerGameCountdown.Interval/1000.0;
            labelGameTimer.Text = timeLeft.ToString("0.00");
        }

        /// <summary>
        /// Called when the game has finished and stops the countdown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameEnded(object sender, EventArgs e)
        {
            timerGameCountdown.Stop();
        }

    }
}
