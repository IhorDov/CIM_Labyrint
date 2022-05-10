﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Player : Component, IGameListner
    {
        private float Y;
        private float X;

        //private bool canShoot = true;

        //private float shootTime = 0;

        private Animator animator;

        private Dictionary<Keys, BUTTONSTATE> movementKeys = new Dictionary<Keys, BUTTONSTATE>();

        public Player(float xPosition, float yPosition)
        {
            this.X = xPosition;

            this.Y = yPosition;
        }


        public void Move(Vector2 velocity)
        {
            velocity.X = X;
            velocity.Y = Y;


            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

            velocity *= speed;

            //Movement(velocity);

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

        //private void Movement(Vector2 direction)
        //{
        //    //Look
        //    GameObjectWithCollider targetObject = LookAround.LookAt(GridPlacement.Placement(GameObject.gridPosition + direction));

        //    if (targetObject == null)
        //       MoveInDirection(direction);
        //    else if (targetObject is Box)
        //    {
        //        Box targetBox = (Box)targetObject;
        //        bool result = targetBox.BoxMovement(direction);

        //        if (result)
        //            MoveInDirection(direction);
        //    }
        //}


        public override void Awake()
        {
            this.speed = 150;
            //this.myScale = 0.5f;
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
