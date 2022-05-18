﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace CIM_Labyrint
{
    class Player : Component, IGameListner
    {
        private Animator animator;

        private Dictionary<Keys, BUTTONSTATE> movementKeys = new Dictionary<Keys, BUTTONSTATE>();

        //private float time;
        private Thread internalThread;


        public Player()
        {
            internalThread = new Thread(InputHandlerThread);
            internalThread.IsBackground = true;
            internalThread.Start();
        }

        void InputHandlerThread()
        {
            while (true)
            {
                //time =+ 0.001f;

                //if (time >= GameWorld.DeltaTime)
                //{

                //    time = 0;
                //}
                    InputHandler.Instance.Execute(this);
            }
        }


        public void Move(Vector2 velocity)
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
        }

        public override void Awake()
        {
            this.speed = 0.01f;
        }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Player/PlayerF_2");

            animator = (Animator)GameObject.GetComponent<Animator>();
        }




        public override void Update()
        {


        }

        public void Notify(GameEvent gameEvent)
        {
            if (gameEvent is CollisionEvent)
            {
                GameWorld.Instance.Destroy((gameEvent as CollisionEvent).Other);
            }
            else if (gameEvent is ButtonEvent)
            {
                ButtonEvent be = (gameEvent as ButtonEvent);

                movementKeys[be.Key] = be.State;
            }
        }
    }
}