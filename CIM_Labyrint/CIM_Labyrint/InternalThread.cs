using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CIM_Labyrint
{
    public class InternalThread 
    {
        public Thread newThread;



        public void ThreadAll(Vector2 vector)
        {
            Player tws = new Player(vector);

            newThread = new Thread(() => tws.Move());
            newThread.Start();
        }

    }
}
