using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class EnemyBuilder : IBuilder
    {
        private GameObject gameObject;

        private Vector2 gridPosition;

        public Vector2 position;

        private float speed;

        public EnemyBuilder(float x, float y)
        {
            this.gridPosition.X = x;
            this.gridPosition.Y = y;
            this.speed = 0.01f;

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

            StartAlgoritme Sa = (StartAlgoritme)gameObject.AddComponent(new StartAlgoritme());


            gameObject.AddComponent(new Enemy());
            gameObject.AddComponent(new SpriteRenderer());
            gameObject.AddComponent(new StartAlgoritme());



            gameObject.Transform.Position = new Vector2(position.X, position.Y);

            Collider c = (Collider)gameObject.AddComponent(new Collider());
            c.CollisionEvent.Attach(cr);
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
