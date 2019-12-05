using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Montagsmaler
{
    public partial class NewForm : Form
    {
        private NetServerManager serverManager;
        private NetClientManager clientManager;

        /// <summary>
        /// Moves all GroupBoxes in the upper left corner.
        /// </summary>
        public NewForm()
        {
            InitializeComponent();
            foreach (GroupBox control in Controls.OfType<GroupBox>())
                control.Location = new Point(0, 0);
        }

        /// <summary>
        /// Changes the current visible GroupBox.
        /// </summary>
        /// <param name="scnChng">Scene to change to</param>
        private void ChangeScene(SceneChanges scnChng)
        {
            foreach (GroupBox control in Controls.OfType<GroupBox>())
                control.SynchronizedInvoke(() => control.Visible = false );

            switch (scnChng)
            {
                case SceneChanges.Opening:
                    OpenOpening();
                    break;
                case SceneChanges.LobbyHost:
                    OpenLobbyHost();
                    break;
                case SceneChanges.LobbyClient:
                    OpenLobbyClient();
                    break;
                case SceneChanges.Search:
                    OpenSearch();
                    break;
                case SceneChanges.Game:
                    OpenGame();
                    break;
            }
        }

        enum SceneChanges
        {
            Opening,
            LobbyHost,
            LobbyClient,
            Search,
            Game
        }
        
    }
}
