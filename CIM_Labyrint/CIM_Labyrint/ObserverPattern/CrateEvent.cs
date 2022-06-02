using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class CrateEvent : GameEvent
    {
        //public Player Player { get; private set; }

        public COLLIDERSIDE MoveCrateWithPlayer { get; private set; }

        public void Notify(COLLIDERSIDE createMoveForCrate)
        {
            this.MoveCrateWithPlayer = createMoveForCrate;

            base.Notify();
        }
    }
}
