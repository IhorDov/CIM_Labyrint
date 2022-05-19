using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Player : Component, IGameListner
    {
        //private BlockBuilder block = new BlockBuilder(new float(), new float());
        //private Player playerPosition = new Player();
        //private readonly object Transform;


        private Animator animator;

        private Dictionary<Keys, BUTTONSTATE> movementKeys = new Dictionary<Keys, BUTTONSTATE>();

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
            this.speed = 150;            
        }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Player/PlayerF_2");
            sr.LayerDepth = 0;

            animator = (Animator)GameObject.GetComponent<Animator>();
        }


        public override void Update()
        {
            InputHandler.Instance.Execute(this);

            //if (GameObject.Transform.Position.Y == block.position.Y)
            //{
            //    playerPosition.Transform.Equals(block.position.Y - 1);
            //}
            //else if (GameObject.Transform.Position.X == block.position.X)
            //{
            //    playerPosition.Transform.Equals(block.position.X - 1);
            //}
        }

        public void Notify(GameEvent gameEvent)
        {
            if (gameEvent is CollisionEvent)
            {
                //GameWorld.Instance.Destroy(GameObject);

                if (GameObject is Block)  //When this enemy colliders with shield
                {
                    GameWorld.Instance.Destroy(GameObject);

                }
            }
            else if (gameEvent is ButtonEvent)
            {
                ButtonEvent be = (gameEvent as ButtonEvent);

                movementKeys[be.Key] = be.State;
            }
        }
    }
}