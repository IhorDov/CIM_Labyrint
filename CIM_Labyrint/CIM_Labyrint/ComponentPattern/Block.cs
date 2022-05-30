using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Block : Component, IGameListner
    {
        Player player = new Player();
        private Collider blockCollider;
        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Bloks/block_05");
            sr.LayerDepth = 0;
            sr.Rotation = 0;

            blockCollider = GameObject.GetComponent<Collider>() as Collider;
        }
        public override void Awake()
        {
            GameObject.Tag = "Block";            
        }

        public void Notify(GameEvent gameEvent)
        {            
            if (gameEvent is CollisionEvent ce)
            {
                Collider otherCollider = ce.Other.GetComponent<Collider>();
            }
        }
    }

}