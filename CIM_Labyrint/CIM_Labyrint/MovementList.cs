using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class MovementList
    {
        private List<GameObjectWithMovement> objs = new List<GameObjectWithMovement>();
        private List<Vector2> moves = new List<Vector2>();

        public List<GameObjectWithMovement> Objs { get => objs; set => objs = value; }
        public List<Vector2> Moves { get => moves; set => moves = value; }

        public void InputMove(GameObjectWithMovement objectWithMovement, Vector2 lastPosition)
        {
            objs.Add(objectWithMovement);
            moves.Add(lastPosition);
        }
    }
}
