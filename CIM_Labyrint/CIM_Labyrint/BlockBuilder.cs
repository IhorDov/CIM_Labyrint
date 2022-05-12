using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class BlockBuilder : IBuilder
    {
        private GameObject gameObject;

        private float x, y;

        public BlockBuilder(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public void BuildGameObject()
        {
            gameObject = new GameObject();

            BuildComponents();

            gameObject.Transform.Position = new Vector2(x, y);
        }
        private void BuildComponents()
        {
            //Player p = (Player)gameObject.AddComponent(new Player());

            gameObject.AddComponent(new Block());
            gameObject.AddComponent(new SpriteRenderer());

            //Collider c = (Collider)gameObject.AddComponent(new Collider());
            //c.CollisionEvent.Attach(p);
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
