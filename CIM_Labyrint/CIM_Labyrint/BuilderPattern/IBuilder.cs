using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    interface IBuilder
    {
        void BuildGameObject();

        GameObject GetResult();
    }
}
