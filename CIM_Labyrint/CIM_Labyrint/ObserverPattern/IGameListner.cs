using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    public interface IGameListner
    {
        void Notify(GameEvent gameEvent);
    }
}