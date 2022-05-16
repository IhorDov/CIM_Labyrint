using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class BaseBuilder : IBuilder
    {
        private GameObject gameObject;

     

        protected Vector2 gridPosition;

        public Vector2 position;


        public BaseBuilder(float x, float y)
        {
            this.gridPosition.X = x;
            this.gridPosition.Y = y;


            this.position = GridPlacement.Placement(gridPosition);

        }
        public void BuildGameObject()
        {
            gameObject = new GameObject();

            BuildComponents();



        }
        private void BuildComponents()
        {
            //Player p = (Player)gameObject.AddComponent(new Player());

            gameObject.AddComponent(new Base());
            gameObject.AddComponent(new SpriteRenderer());
            gameObject.Transform.Position = new Vector2(position.X, position.Y);

            //Collider c = (Collider)gameObject.AddComponent(new Collider());
            //c.CollisionEvent.Attach(p);
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
