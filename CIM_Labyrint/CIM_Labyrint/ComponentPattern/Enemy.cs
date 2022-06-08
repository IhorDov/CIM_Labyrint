using database;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Enemy : Component, IGameListner
    {
        private float Lifetime = 3600f;
        private Collider enemyCollider;
        private float cooldown = 0f; //Cooldown field

        IRepository repository;


        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Enemy/monster10");

            enemyCollider = GameObject.GetComponent<Collider>() as Collider;

        }

        public override void Awake()
        {
            this.Speed = 120;

            GameObject.Tag = "Enemy";
        }
        public override void Update()
        {
            Lifetime--;

            if (Lifetime <= 0)
            {
                GameWorld.Instance.Destroy(GameObject);
            }
        }

            public void Notify(GameEvent gameEvent)
        {
            if (gameEvent is CollisionEvent)
            {
                GameWorld.Instance.Destroy(GameObject);
               
                    if (cooldown <= 0)
                    {
                        cooldown = 60;
                        GameWorld.lives--;
                    repository.AddLife(GameWorld.lives);
                }
                    cooldown--;
                
                //GameWorld.Instance.Destroy((gameEvent as CollisionEvent).Other);
            }
        }
    }
}
