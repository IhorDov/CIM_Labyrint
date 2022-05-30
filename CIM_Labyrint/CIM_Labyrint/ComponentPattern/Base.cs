﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Base : Component, IGameListner
    {    
        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Environment/environment_06");
            sr.LayerDepth = 0;
            sr.Rotation = 0;
        }
        public override void Awake()
        {
            GameObject.Tag = "Base";
            GameObject.Speed = 0;
        }

        public void Notify(GameEvent gameEvent)
        {
            if (gameEvent is CollisionEvent)
            {
                //GameWorld.Instance.Destroy((gameEvent as CollisionEvent).Other);
            }
        }
    }
}
