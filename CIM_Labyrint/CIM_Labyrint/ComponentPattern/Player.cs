using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace CIM_Labyrint
{
    class Player : Component, IGameListner
    {
        private Animator animator;

        private Dictionary<Keys, BUTTONSTATE> movementKeys = new Dictionary<Keys, BUTTONSTATE>();



        public void Move(Vector2 velocity)
        {

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= Speed;

            GameObject.Transform.Position += velocity * GameWorld.DeltaTime;

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
            this.Speed = 150;

            GameObject.Tag = "Player";
        }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Player/PlayerF_2");
            sr.LayerDepth = 0;
            sr.Rotation = 0;

            animator = (Animator)GameObject.GetComponent<Animator>();
        }

        public override void Update()
        {
            InputHandler.Instance.Execute(this);
        }

        public void Notify(GameEvent gameEvent)
        {

            ButtonEvent be = (gameEvent as ButtonEvent);

            if (gameEvent is CollisionEvent ce)
            {
                if (ce.Other.Tag == "Block")
                {
                    GameObject.Transform.Translate(new Vector2(0, 0));
                }

                //GameWorld.Instance.Destroy(GameObject);
                //GameWorld.Instance.Destroy((gameEvent as CollisionEvent).Other);
            }
            if (gameEvent is ButtonEvent)
            {
                movementKeys[be.Key] = be.State;
            }
            //else if (gameEvent is CrateEvent)
            //{
            //    if (be != null) movementKeys[be.Key] = be.State;
            //}

        }
    }
}