using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Apple : Component, IGameListner
    {
        private float Lifetime = 3600f;
        private Collider enemyCollider;
        private float cooldown = 0f; //Cooldown field

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("cherry");

            enemyCollider = GameObject.GetComponent<Collider>() as Collider;

        }

        public override void Awake()
        {
            this.Speed = 120;

            GameObject.Tag = "Apple";
        }
        public override void Update()
        {

        }

            public void Notify(GameEvent gameEvent)
        {
            if (gameEvent is CollisionEvent)
            {
                GameWorld.Instance.Destroy(GameObject);
               

                        GameWorld.lives++;

                
                //GameWorld.Instance.Destroy((gameEvent as CollisionEvent).Other);
            }
        }
    }
}
