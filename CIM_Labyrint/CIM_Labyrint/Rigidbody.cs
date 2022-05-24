using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Rigidbody : Component, IGameListner
    {
        private bool block;
        private bool stopMove;
        private Vector2 translation;
        private COLLIDERSIDE sideOfCollider;
        public bool Block { get => block; set => block = value; }

        public override void Awake()
        {
            Speed = 100;
        }

        public override void Update()
        {
            if(!stopMove)
            {
                GameObject.Transform.Translate(translation);
                translation = Vector2.Zero;
            }
            stopMove = false;
        }

        public void Notify(GameEvent gameEvent)
        {
            return;
            if (gameEvent is CollisionEvent ce)
            {
                Rigidbody rb = ce.Other.GetComponent<Rigidbody>();
                if (rb == null)
                    return;
                Vector2 direction = (GameObject.Transform.Position - rb.GameObject.Transform.Position);
                direction.Normalize();

                if (direction.X > 0)
                {
                    direction.Y = 0;
                    sideOfCollider = sideOfCollider | COLLIDERSIDE.RIGHT;
                }
                else if (direction.X < 0)
                {
                    direction.Y = 0;
                    sideOfCollider = sideOfCollider | COLLIDERSIDE.LEFT;
                }
                else if (direction.Y > 0)
                {
                    direction.X = 0;
                    sideOfCollider = sideOfCollider | COLLIDERSIDE.DOWN;
                }
                else if (direction.Y < 0)
                {
                    direction.X = 0;
                    sideOfCollider = sideOfCollider | COLLIDERSIDE.UP;
                }
                if(rb.Block)
                {
                    stopMove = true;
                }
                else
                {
                    translation = direction * rb.Speed;
                }
            }
        }
    }
}
