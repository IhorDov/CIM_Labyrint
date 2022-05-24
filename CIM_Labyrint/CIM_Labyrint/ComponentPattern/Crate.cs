using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Crate : Component, IGameListner
    {
        private Vector2 translation;
        private bool blocked;
        private bool stopMove = false;

        private COLLIDERSIDE sideOfCollider = new COLLIDERSIDE();

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Crate/crate_10");
            sr.LayerDepth = 0;
            sr.Rotation = 0;
        }

        public override void Awake()
        {
            this.Speed = 150;

            GameObject.Tag = "Crate";
        }

        //public override void Update()
        //{
        //    base.Update();
        //    if(!blocked)
        //    {
        //        GameObject.Transform.Translate(translation);
        //        translation = Vector2.Zero;
        //    }
        //    blocked = false;
        //}
                
        public void Notify(GameEvent gameEvent)
        {

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
                if (rb.Block)
                {
                    stopMove = true;
                }
                else
                {
                    translation = direction * rb.Speed;
                }
            }

            //if (gameEvent is CollisionEvent ce)
            //{
            //    if (ce.Other.Tag == "Player")
            //    {
            //        GameObject.Transform.Translate(new Vector2(-1, 0));
            //        //Player p = ce.Other.GetComponent<Player>();
            //        //if(p != null)
            //        //{
            //        //    translation = new Vector2(-1, 0) * p.Speed;
            //        //}
            //    }
            //    else if(ce.Other.Tag == "Crate")
            //    {
            //        GameObject.Transform.Translate(new Vector2(0, 0));
            //        blocked = true;
            //    }
                
            //}
        }
    }
}
