using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
   public static class GridPlacement
    {
        const int gridSize = 64;

        public static Vector2 Placement(Vector2 dir)
        {
            Vector2 result =  new Vector2(dir.X * gridSize + gridSize / 2,
                dir.Y * gridSize + gridSize / 2);

            return result;
        }
    }
}
