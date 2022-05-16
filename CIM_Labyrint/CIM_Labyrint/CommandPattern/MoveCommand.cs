using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CIM_Labyrint
{
    class MoveCommand : ICommand
    {
        private Vector2 velocity;



        public MoveCommand(Vector2 velocity)
        {
            this.velocity = velocity;
        }

        public void Execute(Player player)
        {

            InternalThread I = new InternalThread();

            I.ThreadAll(velocity);
        }


    }
}
