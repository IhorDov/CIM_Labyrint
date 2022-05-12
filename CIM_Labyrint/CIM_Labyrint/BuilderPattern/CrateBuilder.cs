using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class CrateBuilder : IBuilder
    {
        private GameObject gameObject;

        private float x, y;

        public CrateBuilder(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public void BuildGameObject()
        {
            gameObject = new GameObject();

            BuildComponents();

            gameObject.Transform.Position = new Microsoft.Xna.Framework.Vector2(x, y);
        }
        private void BuildComponents()
        {
            //Player p = (Player)gameObject.AddComponent(new Player());

            gameObject.AddComponent(new Crate());
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
