using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    public class GameEvent
    {
        private List<IGameListner> listners = new List<IGameListner>();

        public void Attach(IGameListner listner)
        {
            listners.Add(listner);
        }

        public void Detach(IGameListner listner)
        {
            listners.Remove(listner);
        }
        /// <summary>
        /// // Trigger an update in each subscriber.
        /// </summary>
        public void Notify()
        {
            foreach (IGameListner listner in listners)
            {
                listner.Notify(this);
            }
        }
    }
}