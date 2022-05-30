using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Crate : Component, IGameListner
    {
        private Vector2 cratePosition;
        private Collider crateCollider;
        private Player player;

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Crate/crate_10");
            sr.LayerDepth = 0;
            sr.Rotation = 0;
            crateCollider = GameObject.GetComponent<Collider>() as Collider;
            player = GameObject.GetComponent<Player>() as Player;
        }

        public override void Awake()
        {
            GameObject.Tag = "Crate";
            GameObject.Speed = 150;
            cratePosition = GridPlacement.Placement(cratePosition);
        }
                
        public void Notify(GameEvent gameEvent)
        {
            if (gameEvent is CollisionEvent ce)
            {
                Collider otherCollider = ce.Other.GetComponent<Collider>();

                if (ce.Other.Tag == "Player")
                {

                     if (crateCollider.CollisionBox.Right >= otherCollider.CollisionBox.Right)
                     {
                        GameObject.Transform.Translate(new Vector2(1, 0));
                        GameObject.Speed = 150;
                     }

                     if (crateCollider.CollisionBox.Left <= otherCollider.CollisionBox.Left)
                    {
                        GameObject.Transform.Translate(new Vector2(-1, 0));
                        GameObject.Speed = 150;
                    }

                    if (crateCollider.CollisionBox.Top >= otherCollider.CollisionBox.Top)
                    {
                        GameObject.Transform.Translate(new Vector2(0, 1));
                        GameObject.Speed = 150;
                    }

                    if (crateCollider.CollisionBox.Bottom <= otherCollider.CollisionBox.Bottom)
                    {
                        GameObject.Transform.Translate(new Vector2(0, -1));
                        GameObject.Speed = 150;
                    }
                }
                if (ce.Other.Tag == "Crate")
                {
                    if (crateCollider.CollisionBox.Right >= otherCollider.CollisionBox.Right)
                    {
                        GameObject.Transform.Translate(new Vector2(1, 0));
                    }

                    if (crateCollider.CollisionBox.Left <= otherCollider.CollisionBox.Left)
                    {
                        GameObject.Transform.Translate(new Vector2(-1, 0));
                    }

                    if (crateCollider.CollisionBox.Top >= otherCollider.CollisionBox.Top)
                    {
                        GameObject.Transform.Translate(new Vector2(0, 1));
                    }

                    if (crateCollider.CollisionBox.Bottom <= otherCollider.CollisionBox.Bottom)
                    {
                        GameObject.Transform.Translate(new Vector2(0, -1));
                    }
                }
                if (ce.Other.Tag == "Block")
                {
                    if (crateCollider.CollisionBox.Right >= otherCollider.CollisionBox.Right)
                    {
                        GameObject.Transform.Translate(new Vector2(1, 0));
                    }

                    if (crateCollider.CollisionBox.Left <= otherCollider.CollisionBox.Left)
                    {
                        GameObject.Transform.Translate(new Vector2(-1, 0));
                    }

                    if (crateCollider.CollisionBox.Top >= otherCollider.CollisionBox.Top)
                    {
                        GameObject.Transform.Translate(new Vector2(0, 1));
                    }

                    if (crateCollider.CollisionBox.Bottom <= otherCollider.CollisionBox.Bottom)
                    {
                        GameObject.Transform.Translate(new Vector2(0, -1));
                    }
                }
            }
        }
    }
}
