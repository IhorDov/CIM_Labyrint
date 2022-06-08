using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CIM_Labyrint
{
    class AppleBuilder : Component,  IBuilder
    {
        private GameObject gameObject;

        private Vector2 gridPosition;

        public Vector2 position;

        
        private Vector2 velocity;
  
        static readonly object _object = new object();

        private Thread internalThread;


        public AppleBuilder(float x, float y)
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
            //Enemy cr = (Apple)gameObject.AddComponent(new Apple());



            gameObject.AddComponent(new Apple());
            gameObject.AddComponent(new SpriteRenderer());

            
     


            //Collider c = (Collider)gameObject.AddComponent(new Collider());
            //c.CollisionEvent.Attach(cr);
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
