using System;
using System.Drawing;

namespace Montagsmaler
{

    /// <summary>
    /// EventArgs to send mousepositions.
    /// </summary>
    public class MouseMoveEventArgs : EventArgs
    {
        public Point P1 { get; private set; }
        public Point P2 { get; private set; }

        public MouseMoveEventArgs(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;
        }
    }
}
