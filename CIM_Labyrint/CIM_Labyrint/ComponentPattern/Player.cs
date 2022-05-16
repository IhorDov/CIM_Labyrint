using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace CIM_Labyrint
{
    class Player : Component, IGameListner
    {

        static readonly object _object = new object();

        private Thread internalThread;
        private Vector2 velocity;

        private Animator animator;

        private Dictionary<Keys, BUTTONSTATE> movementKeys = new Dictionary<Keys, BUTTONSTATE>();


        public void Move(Vector2 velocity)
        {
            this.velocity = velocity;
            internalThread = new Thread(test);
            internalThread.IsBackground = true;
        }

        public void Startd()
        {
            internalThread.Start();

        }


        void test()
        {
            while (true)
            {
                Run();
            }
        }

        void Run()
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