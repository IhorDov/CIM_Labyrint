using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace CIM_Labyrint
{
   public class Player : Component, IGameListner
    {

        static readonly object _object = new object();
        private Animator animator;

        private Dictionary<Keys, BUTTONSTATE> movementKeys = new Dictionary<Keys, BUTTONSTATE>();
        private Vector2 velocity;

        public void D(Vector2 velocityu)
        {
            this.velocity = velocityu;
        }

        public void Move()
        {

            while (true)
            {
                Run(velocity);
            }
        }




        void Run(Vector2 velocity)
        {

            lock (_object)
            {

                if (velocity != Vector2.Zero)
                {
                    velocity.Normalize();
                }

                velocity *= speed;

                GameObject.Transform.Position += (velocity * GameWorld.DeltaTime);

                if (velocity.X > 0)
                {
                    animator.PlayAnimation("Right");

                }
                else if (velocity.X < 0)
                {
                    animator.PlayAnimation("Left");
                }
                else if (velocity.Y < 0)
                {
                    animator.PlayAnimation("Back");
                }
                else if (velocity.Y > 0)
                {
                    animator.PlayAnimation("Forward");
                }
                else if (velocity.X == 0 && velocity.Y == 0)
                {
                    animator.PlayAnimation("Stay");
                }

                Thread.Sleep(1);
            }

        }


        public override void Awake()
        {
            this.speed = 150;
            
        }

        public override void Start()
        {




            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Player/PlayerF_2");


            animator = (Animator)GameObject.GetComponent<Animator>();
        }


        public override void Update()
        {
            InputHandler.Instance.Execute(this);
        }

        public void Notify(GameEvent gameEvent)
        {
            //if (gameEvent is CollisionEvent)
            //{
            //    GameWorld.Instance.Destroy((gameEvent as CollisionEvent).Other);
            //}
            //else if (gameEvent is ButtonEvent)
            //{
            ButtonEvent be = (gameEvent as ButtonEvent);

            movementKeys[be.Key] = be.State;
            //}
        }
    }
}