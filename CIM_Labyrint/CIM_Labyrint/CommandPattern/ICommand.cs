using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    interface ICommand
    {
        void Execute(Player player);
    }
}