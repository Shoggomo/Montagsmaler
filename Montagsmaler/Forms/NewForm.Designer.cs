namespace Montagsmaler
{
    partial class NewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupOpening = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOpeningExplanation = new System.Windows.Forms.Button();
            this.buttonOpeningSearch = new System.Windows.Forms.Button();
            this.buttonOpeningInfo = new System.Windows.Forms.Button();
            this.buttonOpeningHost = new System.Windows.Forms.Button();
            this.groupLobby = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonLobbyStartGame = new System.Windows.Forms.Button();
            this.listBoxLobby = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupSearch = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSearchScanNetwork = new System.Windows.Forms.Button();
            this.textBoxSearchServerAddress = new System.Windows.Forms.TextBox();
            this.buttonSearchJoinGame = new System.Windows.Forms.Button();
            this.listBoxSearch = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupGame = new System.Windows.Forms.GroupBox();
            this.listBoxGameScores = new System.Windows.Forms.ListBox();
            this.labelGameTimer = new System.Windows.Forms.Label();
            this.textBoxGameChat = new System.Windows.Forms.TextBox();
            this.buttonGameSend = new System.Windows.Forms.Button();
            this.textBoxGameChatInput = new System.Windows.Forms.TextBox();
            this.listBoxGame = new System.Windows.Forms.ListBox();
            this.panelGame = new System.Windows.Forms.Panel();
            this.timerGameSendData = new System.Windows.Forms.Timer(this.components);
            this.timerGameCountdown = new System.Windows.Forms.Timer(this.components);
            this.groupOpening.SuspendLayout();
            this.groupLobby.SuspendLayout();
            this.groupSearch.SuspendLayout();
            this.groupGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupOpening
            // 
            this.groupOpening.BackColor = System.Drawing.SystemColors.Control;
            this.groupOpening.Controls.Add(this.label3);
            this.groupOpening.Controls.Add(this.buttonOpeningExplanation);
            this.groupOpening.Controls.Add(this.buttonOpeningSearch);
            this.groupOpening.Controls.Add(this.buttonOpeningInfo);
            this.groupOpening.Controls.Add(this.buttonOpeningHost);
            this.groupOpening.Location = new System.Drawing.Point(12, 12);
            this.groupOpening.Name = "groupOpening";
            this.groupOpening.Size = new System.Drawing.Size(800, 600);
            this.groupOpening.TabIndex = 0;
            this.groupOpening.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 271);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(353, 59);
            this.label3.TabIndex = 6;
            this.label3.Text = "Montagsmaler";
            // 
            // buttonOpeningExplanation
            // 
            this.buttonOpeningExplanation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpeningExplanation.Location = new System.Drawing.Point(412, 308);
            this.buttonOpeningExplanation.Name = "buttonOpeningExplanation";
            this.buttonOpeningExplanation.Size = new System.Drawing.Size(342, 119);
            this.buttonOpeningExplanation.TabIndex = 5;
            this.buttonOpeningExplanation.Text = "Spielerklärung";
            this.buttonOpeningExplanation.UseVisualStyleBackColor = true;
            this.buttonOpeningExplanation.Click += new System.EventHandler(this.buttonOpeningExplanation_Click);
            // 
            // buttonOpeningSearch
            // 
            this.buttonOpeningSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpeningSearch.Location = new System.Drawing.Point(411, 183);
            this.buttonOpeningSearch.Name = "buttonOpeningSearch";
            this.buttonOpeningSearch.Size = new System.Drawing.Size(342, 119);
            this.buttonOpeningSearch.TabIndex = 4;
            this.buttonOpeningSearch.Text = "Spiel suchen";
            this.buttonOpeningSearch.UseVisualStyleBackColor = true;
            this.buttonOpeningSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonOpeningInfo
            // 
            this.buttonOpeningInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpeningInfo.Location = new System.Drawing.Point(411, 433);
            this.buttonOpeningInfo.Name = "buttonOpeningInfo";
            this.buttonOpeningInfo.Size = new System.Drawing.Size(342, 119);
            this.buttonOpeningInfo.TabIndex = 3;
            this.buttonOpeningInfo.Text = "Info";
            this.buttonOpeningInfo.UseVisualStyleBackColor = true;
            this.buttonOpeningInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // buttonOpeningHost
            // 
            this.buttonOpeningHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpeningHost.Location = new System.Drawing.Point(411, 58);
            this.buttonOpeningHost.Name = "buttonOpeningHost";
            this.buttonOpeningHost.Size = new System.Drawing.Size(342, 119);
            this.buttonOpeningHost.TabIndex = 2;
            this.buttonOpeningHost.Text = "Spiel hosten";
            this.buttonOpeningHost.UseVisualStyleBackColor = true;
            this.buttonOpeningHost.Click += new System.EventHandler(this.buttonLobby_Click);
            // 
            // groupLobby
            // 
            this.groupLobby.Controls.Add(this.label5);
            this.groupLobby.Controls.Add(this.buttonLobbyStartGame);
            this.groupLobby.Controls.Add(this.listBoxLobby);
            this.groupLobby.Controls.Add(this.label1);
            this.groupLobby.Location = new System.Drawing.Point(12, 618);
            this.groupLobby.Name = "groupLobby";
            this.groupLobby.Size = new System.Drawing.Size(800, 600);
            this.groupLobby.TabIndex = 1;
            this.groupLobby.TabStop = false;
            this.groupLobby.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(26, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(353, 59);
            this.label5.TabIndex = 9;
            this.label5.Text = "Montagsmaler";
            // 
            // buttonLobbyStartGame
            // 
            this.buttonLobbyStartGame.Location = new System.Drawing.Point(407, 453);
            this.buttonLobbyStartGame.Name = "buttonLobbyStartGame";
            this.buttonLobbyStartGame.Size = new System.Drawing.Size(346, 88);
            this.buttonLobbyStartGame.TabIndex = 8;
            this.buttonLobbyStartGame.Text = "Spiel Starten";
            this.buttonLobbyStartGame.UseVisualStyleBackColor = true;
            this.buttonLobbyStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // listBoxLobby
            // 
            this.listBoxLobby.FormattingEnabled = true;
            this.listBoxLobby.ItemHeight = 24;
            this.listBoxLobby.Location = new System.Drawing.Point(406, 105);
            this.listBoxLobby.Name = "listBoxLobby";
            this.listBoxLobby.Size = new System.Drawing.Size(347, 340);
            this.listBoxLobby.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(402, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Warte auf Spieler...";
            // 
            // groupSearch
            // 
            this.groupSearch.Controls.Add(this.label4);
            this.groupSearch.Controls.Add(this.buttonSearchScanNetwork);
            this.groupSearch.Controls.Add(this.textBoxSearchServerAddress);
            this.groupSearch.Controls.Add(this.buttonSearchJoinGame);
            this.groupSearch.Controls.Add(this.listBoxSearch);
            this.groupSearch.Controls.Add(this.label2);
            this.groupSearch.Location = new System.Drawing.Point(873, 12);
            this.groupSearch.Name = "groupSearch";
            this.groupSearch.Size = new System.Drawing.Size(800, 600);
            this.groupSearch.TabIndex = 1;
            this.groupSearch.TabStop = false;
            this.groupSearch.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 271);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(353, 59);
            this.label4.TabIndex = 13;
            this.label4.Text = "Montagsmaler";
            // 
            // buttonSearchScanNetwork
            // 
            this.buttonSearchScanNetwork.Location = new System.Drawing.Point(408, 451);
            this.buttonSearchScanNetwork.Name = "buttonSearchScanNetwork";
            this.buttonSearchScanNetwork.Size = new System.Drawing.Size(160, 88);
            this.buttonSearchScanNetwork.TabIndex = 12;
            this.buttonSearchScanNetwork.Text = "Scanne Netzwerk";
            this.buttonSearchScanNetwork.UseVisualStyleBackColor = true;
            this.buttonSearchScanNetwork.Click += new System.EventHandler(this.buttonSearchNetwork_Click);
            // 
            // textBoxSearchServerAddress
            // 
            this.textBoxSearchServerAddress.Location = new System.Drawing.Point(408, 411);
            this.textBoxSearchServerAddress.Name = "textBoxSearchServerAddress";
            this.textBoxSearchServerAddress.Size = new System.Drawing.Size(346, 29);
            this.textBoxSearchServerAddress.TabIndex = 2;
            // 
            // buttonSearchJoinGame
            // 
            this.buttonSearchJoinGame.Location = new System.Drawing.Point(594, 451);
            this.buttonSearchJoinGame.Name = "buttonSearchJoinGame";
            this.buttonSearchJoinGame.Size = new System.Drawing.Size(160, 88);
            this.buttonSearchJoinGame.TabIndex = 11;
            this.buttonSearchJoinGame.Text = "Lobby beitreten";
            this.buttonSearchJoinGame.UseVisualStyleBackColor = true;
            this.buttonSearchJoinGame.Click += new System.EventHandler(this.buttonJoinGame_Click);
            // 
            // listBoxSearch
            // 
            this.listBoxSearch.FormattingEnabled = true;
            this.listBoxSearch.ItemHeight = 24;
            this.listBoxSearch.Location = new System.Drawing.Point(408, 113);
            this.listBoxSearch.Name = "listBoxSearch";
            this.listBoxSearch.Size = new System.Drawing.Size(346, 292);
            this.listBoxSearch.TabIndex = 10;
            this.listBoxSearch.SelectedIndexChanged += new System.EventHandler(this.listBoxSearch_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(403, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Serverliste:";
            // 
            // groupGame
            // 
            this.groupGame.Controls.Add(this.listBoxGameScores);
            this.groupGame.Controls.Add(this.labelGameTimer);
            this.groupGame.Controls.Add(this.textBoxGameChat);
            this.groupGame.Controls.Add(this.buttonGameSend);
            this.groupGame.Controls.Add(this.textBoxGameChatInput);
            this.groupGame.Controls.Add(this.listBoxGame);
            this.groupGame.Controls.Add(this.panelGame);
            this.groupGame.Location = new System.Drawing.Point(873, 618);
            this.groupGame.Name = "groupGame";
            this.groupGame.Size = new System.Drawing.Size(800, 600);
            this.groupGame.TabIndex = 2;
            this.groupGame.TabStop = false;
            this.groupGame.Visible = false;
            // 
            // listBoxGameScores
            // 
            this.listBoxGameScores.FormattingEnabled = true;
            this.listBoxGameScores.ItemHeight = 24;
            this.listBoxGameScores.Location = new System.Drawing.Point(737, 446);
            this.listBoxGameScores.Name = "listBoxGameScores";
            this.listBoxGameScores.Size = new System.Drawing.Size(57, 148);
            this.listBoxGameScores.TabIndex = 7;
            // 
            // labelGameTimer
            // 
            this.labelGameTimer.AutoSize = true;
            this.labelGameTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGameTimer.Location = new System.Drawing.Point(698, 396);
            this.labelGameTimer.Name = "labelGameTimer";
            this.labelGameTimer.Size = new System.Drawing.Size(98, 38);
            this.labelGameTimer.TabIndex = 6;
            this.labelGameTimer.Text = "00,00";
            // 
            // textBoxGameChat
            // 
            this.textBoxGameChat.Location = new System.Drawing.Point(451, 37);
            this.textBoxGameChat.Multiline = true;
            this.textBoxGameChat.Name = "textBoxGameChat";
            this.textBoxGameChat.ReadOnly = true;
            this.textBoxGameChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxGameChat.Size = new System.Drawing.Size(343, 315);
            this.textBoxGameChat.TabIndex = 5;
            // 
            // buttonGameSend
            // 
            this.buttonGameSend.Location = new System.Drawing.Point(451, 396);
            this.buttonGameSend.Name = "buttonGameSend";
            this.buttonGameSend.Size = new System.Drawing.Size(125, 47);
            this.buttonGameSend.TabIndex = 3;
            this.buttonGameSend.Text = "Senden";
            this.buttonGameSend.UseVisualStyleBackColor = true;
            this.buttonGameSend.Click += new System.EventHandler(this.buttonGameSend_Click);
            // 
            // textBoxGameChatInput
            // 
            this.textBoxGameChatInput.Location = new System.Drawing.Point(451, 358);
            this.textBoxGameChatInput.Name = "textBoxGameChatInput";
            this.textBoxGameChatInput.Size = new System.Drawing.Size(343, 29);
            this.textBoxGameChatInput.TabIndex = 2;
            this.textBoxGameChatInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxGameChatInput_KeyDown);
            // 
            // listBoxGame
            // 
            this.listBoxGame.FormattingEnabled = true;
            this.listBoxGame.ItemHeight = 24;
            this.listBoxGame.Location = new System.Drawing.Point(451, 446);
            this.listBoxGame.Name = "listBoxGame";
            this.listBoxGame.Size = new System.Drawing.Size(280, 148);
            this.listBoxGame.TabIndex = 1;
            // 
            // panelGame
            // 
            this.panelGame.Location = new System.Drawing.Point(6, 37);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(439, 557);
            this.panelGame.TabIndex = 0;
            this.panelGame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelGame_MouseDown);
            this.panelGame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelGame_MouseMove);
            this.panelGame.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelGame_MouseUp);
            // 
            // timerGameSendData
            // 
            this.timerGameSendData.Enabled = true;
            this.timerGameSendData.Interval = 10;
            this.timerGameSendData.Tick += new System.EventHandler(this.timerGame_Tick);
            // 
            // timerGameCountdown
            // 
            this.timerGameCountdown.Enabled = true;
            this.timerGameCountdown.Interval = 10;
            this.timerGameCountdown.Tick += new System.EventHandler(this.timerGameCountdown_Tick);
            // 
            // NewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1694, 1246);
            this.Controls.Add(this.groupGame);
            this.Controls.Add(this.groupSearch);
            this.Controls.Add(this.groupLobby);
            this.Controls.Add(this.groupOpening);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NewForm";
            this.Text = "Montagsmaler";
            this.groupOpening.ResumeLayout(false);
            this.groupOpening.PerformLayout();
            this.groupLobby.ResumeLayout(false);
            this.groupLobby.PerformLayout();
            this.groupSearch.ResumeLayout(false);
            this.groupSearch.PerformLayout();
            this.groupGame.ResumeLayout(false);
            this.groupGame.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupOpening;
        private System.Windows.Forms.Button buttonOpeningSearch;
        private System.Windows.Forms.Button buttonOpeningInfo;
        private System.Windows.Forms.Button buttonOpeningHost;
        private System.Windows.Forms.GroupBox groupLobby;
        private System.Windows.Forms.GroupBox groupSearch;
        private System.Windows.Forms.Button buttonLobbyStartGame;
        private System.Windows.Forms.ListBox listBoxLobby;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSearchJoinGame;
        private System.Windows.Forms.ListBox listBoxSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSearchServerAddress;
        private System.Windows.Forms.Button buttonSearchScanNetwork;
        private System.Windows.Forms.GroupBox groupGame;
        private System.Windows.Forms.TextBox textBoxGameChatInput;
        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.Timer timerGameSendData;
        private System.Windows.Forms.Button buttonGameSend;
        private System.Windows.Forms.ListBox listBoxGame;
        private System.Windows.Forms.TextBox textBoxGameChat;
        private System.Windows.Forms.Label labelGameTimer;
        private System.Windows.Forms.Timer timerGameCountdown;
        private System.Windows.Forms.ListBox listBoxGameScores;
        private System.Windows.Forms.Button buttonOpeningExplanation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}