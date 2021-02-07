using System;
using System.Collections.Generic;

namespace Stella.GameLogic.Command
{
    public interface ICommandListener
    {
        void Listen(ICommand command);
        
    }

    public static class CommandDispatcher
    {
        private static List<ICommandListener> listeners = new List<ICommandListener>();

        public static void AddListener(ICommandListener listener)
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public static void RemoveListener(ICommandListener listener)
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }

        public static void Dispatch(ICommand command)
        {
            foreach (var listener in listeners)
            {
                listener?.Listen(command);
            }
        }
    }
}
