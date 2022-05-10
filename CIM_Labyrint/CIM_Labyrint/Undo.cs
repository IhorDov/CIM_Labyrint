using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CIM_Labyrint
{
    class Undo
    {
        //Singelton
        private static Undo instance = null;

        public static Undo Instance
        {
            get
            {
                if (instance == null)
                    instance = new Undo();

                return instance;
            }
        }

        public Stack<MovementList> movementLists { get; private set; } = new Stack<MovementList>();

        private MovementList colletedMoves;



        public void PutOnStack()
        {
            if (colletedMoves != null)
            {
                movementLists.Push(colletedMoves);
                colletedMoves = null;
            }
        }

        public void AddPosition(Vector2 position, GameObjectWithMovement mover)
        {
            if (colletedMoves == null)
                colletedMoves = new MovementList();

            colletedMoves.InputMove(mover, position);
        }

        public void MoveBack()
        {
            MovementList lastStep = null;

            if (movementLists.Count == 1)
                lastStep = movementLists.Peek();
            else if (movementLists.Count > 1)
                lastStep = movementLists.Pop();

            if (lastStep != null)
                UpdatePositions(lastStep);
        }

        void UpdatePositions(MovementList target)
        {
            for (int i = 0; i < target.Objs.Count; i++)
                target.Objs[i].SetPosition(target.Moves[i]);

            colletedMoves = null;
        }
    }
}
