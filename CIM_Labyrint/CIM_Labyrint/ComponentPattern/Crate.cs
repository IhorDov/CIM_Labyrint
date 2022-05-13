using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Crate : Component
    {
        public float XPos { get; set; }
        public float YPos { get; set; }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Crate/crate_10");
        }
        //public bool CrateMovement(Vector2 direction)
        //{
        //    GameObjectWithCollider targetObject = LookAround.LookAt(GridPlacement.Placement(gridPosition + direction));

        //    if (targetObject == null)
        //    {
        //        MoveInDirection(direction);

        //        return true;
        //    }

        //    return false;
        //}
        //public override void Update()
        //{
        //    GameWorld.Instance.Execute(this);
        //}
    }
}
