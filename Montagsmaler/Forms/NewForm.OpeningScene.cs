using System;
using System.Windows.Forms;

namespace Montagsmaler
{
    public partial class NewForm
    {

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.strings.openingSceneInfo, Properties.strings.openingSceneInfoTitle);
        }

        private void buttonLobby_Click(object sender, EventArgs e)
        {
            ChangeScene(SceneChanges.LobbyHost);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            ChangeScene(SceneChanges.Search);
        }

        private void buttonOpeningExplanation_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.strings.openingSceneExplanation, Properties.strings.openingSceneExplanationTitle);
        }

        /// <summary>
        /// Opens the opening scene.
        /// </summary>
        private void OpenOpening()
        {
            groupOpening.SynchronizedInvoke(() =>
            {
                groupOpening.Visible = true;
                buttonOpeningHost.Focus();
            });
        }


    }
}
