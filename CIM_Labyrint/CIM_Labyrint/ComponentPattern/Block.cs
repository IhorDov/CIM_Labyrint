using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Block : Component, IGameListner
    {
        Player player = new Player();
        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Bloks/block_05");
            sr.LayerDepth = 0;
            sr.Rotation = 0;
        }
        public override void Awake()
        {
            GameObject.Tag = "Block";            
        }

        public void Notify(GameEvent gameEvent)
        {
            if (gameEvent is CollisionEvent ce)
            {
                if (ce.Other.Tag == "Player")
                {
                    GameObject.Transform.Translate(new Vector2(0, 0));                    
                }
            }
                if (gameEvent is CollisionEvent)
            {
                //GameWorld.Instance.Destroy((gameEvent as CollisionEvent).Other);
            }
        }
    }

}