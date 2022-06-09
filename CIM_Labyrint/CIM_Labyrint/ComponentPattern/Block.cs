using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Block : Component, IGameListner
    {
        Player player = new Player();

        private Collider blockCollider;

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Bloks/block_05");
            sr.LayerDepth = 0.05f;
            sr.Rotation = 0;
        }
        public override void Awake()
        {
            GameObject.Tag = "Block";
        }

        public void Notify(GameEvent gameEvent)
        {
            blockCollider = GameObject.GetComponent<Collider>() as Collider;
            if (gameEvent is CollisionEvent ce)
            {
                Collider otherCollider = ce.Other.GetComponent<Collider>();
                Crate crate = ce.Other.GetComponent<Crate>();

                if (ce.Other.Tag == "Crate")
                {
                    if (blockCollider.CollisionBox.Right < otherCollider.CollisionBox.Right)
                    {
                        //crate.GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X + 1, GameObject.Transform.Position.Y);
                        crate.GameObject.Transform.Translate(new Vector2(1, 0));
                    }

                    if (blockCollider.CollisionBox.Left > otherCollider.CollisionBox.Left)
                    {
                        //crate.GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X - 1, GameObject.Transform.Position.Y);

                        crate.GameObject.Transform.Translate(new Vector2(-1, 0));
                    }

                    if (blockCollider.CollisionBox.Top > otherCollider.CollisionBox.Top)
                    {
                        //crate.GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X, GameObject.Transform.Position.Y + 1);

                        crate.GameObject.Transform.Translate(new Vector2(0, -1));
                    }

                    if (blockCollider.CollisionBox.Bottom < otherCollider.CollisionBox.Bottom)
                    {
                        //crate.GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X, GameObject.Transform.Position.Y - 1);

                        crate.GameObject.Transform.Translate(new Vector2(0, 1));
                    }
                }
            }
        }
    }
}