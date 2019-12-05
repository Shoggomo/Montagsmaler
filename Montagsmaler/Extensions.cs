using System;
using System.ComponentModel;

namespace Montagsmaler
{
    public static class Extensions
    {
        /// <summary>
        /// Method to perform methods in forms while being thread safe
        /// Credits to casperOne from StackOverflow
        /// </summary>
        /// <param name="sync">The control to invoke</param>
        /// <param name="action">The action to invoke</param>
        public static void SynchronizedInvoke(this ISynchronizeInvoke sync, Action action)
        {
            if (!sync.InvokeRequired)
            {
                action();
                return;
            }
            sync.Invoke(action, new object[] { });
        }

    }
}
