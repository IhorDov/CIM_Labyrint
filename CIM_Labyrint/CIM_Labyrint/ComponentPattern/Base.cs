using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Base : Component, IGameListner
    {
        private Collider crateCollider;
        //private CrateBuilder crate = new CrateBuilder();

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Environment/environment_06");
            sr.LayerDepth = 0;
            sr.Rotation = 0;

            crateCollider = GameObject.GetComponent<Collider>() as Collider;
        }

        public override void Awake()
        {
            GameObject.Tag = "Base";
        }

        public void Notify(GameEvent gameEvent)
        {
            if (gameEvent is CollisionEvent ce)
            {
                Collider otherCollider = ce.Other.GetComponent<Collider>();

                if (ce.Other.Tag == "Crate")
                {
                    if (crateCollider.CollisionBox.Intersects(otherCollider.CollisionBox))
                    {
                        //Base position is crate position
                        //crate.GameObject.Transform.Position = GameObject.Transform.Position;
                    }
                }
            }
        }
    }
}
