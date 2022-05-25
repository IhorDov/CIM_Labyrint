using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Enemy : Component, IGameListner
    {
        public float XPos { get; set; }
        public float YPos { get; set; }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Crate/crate_03");
        }

        public void Notify(GameEvent gameEvent)
        {
            //if (gameEvent is CollisionEvent)
            //{
            //    GameWorld.Instance.Destroy((gameEvent as CollisionEvent).Other);
            //}
        }
    }
}
