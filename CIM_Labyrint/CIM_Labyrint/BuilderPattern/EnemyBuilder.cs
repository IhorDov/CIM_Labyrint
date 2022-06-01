using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CIM_Labyrint
{
    class EnemyBuilder: Component,  IBuilder
    {
        private GameObject gameObject;

        private Vector2 gridPosition;

        public Vector2 position;

        
        private Vector2 velocity;
  
        static readonly object _object = new object();

        private Thread internalThread;


        public EnemyBuilder(float x, float y)
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
            Enemy cr = (Enemy)gameObject.AddComponent(new Enemy());

       internalThread = new Thread(Run);

            internalThread.IsBackground = true;
            internalThread.Start();

            gameObject.AddComponent(new Enemy());
            gameObject.AddComponent(new SpriteRenderer());
            gameObject.AddComponent(new StartAlgoritme());

            
     


            Collider c = (Collider)gameObject.AddComponent(new Collider());
            c.CollisionEvent.Attach(cr);
        }

        void Run()
        {

            lock (_object)
            {




                gameObject.Transform.Position = new Vector2(position.X, position.Y);


                velocity = new Vector2(4.101f, 0);


                velocity *= Speed;

                velocity.X = position.X;

                velocity.Y = position.Y;

                if (velocity != Vector2.Zero)
                {
                    velocity.Normalize();
                }
                Thread.Sleep(1);
            }

        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
