using System;

namespace Montagsmaler
{
    /// <summary>
    /// EventArgs to send two player indices.
    /// </summary>
    public class NewTurnEventArgs : EventArgs
    {
        public int GuesserPlayerIndex { get; private set; }
        public int DrawerPlayerIndex { get; private set; }

        public NewTurnEventArgs(int guesserIndex, int drawerIndex)
        {
            GuesserPlayerIndex = guesserIndex;
            DrawerPlayerIndex = drawerIndex;
        }
    }
}
