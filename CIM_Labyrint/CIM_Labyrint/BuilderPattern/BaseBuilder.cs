using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class BaseBuilder : Component, IBuilder
    {
        private GameObject gameObject;     

        private Vector2 gridPosition;

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

        public override void Update()
        {
            MoveCrateOnBasePlace(gridPosition.X, gridPosition.Y);
        }

        private void MoveCrateOnBasePlace(float x, float y)
        {
            CrateBuilder crateBuilder = new CrateBuilder(x, y);

            this.gridPosition.X = x;
            this.gridPosition.Y = y;

            if (position.Y  <= crateBuilder.position.Y)
            {
                crateBuilder.position.Y = position.Y;
            }
            if (position.Y >= crateBuilder.position.Y)
            {
                crateBuilder.position.Y = position.Y;
            }
            if (position.X  <= crateBuilder.position.X)
            {
                crateBuilder.position.X = position.X;
            }
            if (position.X >= crateBuilder.position.X)
            {
                crateBuilder.position.X = position.X;
            }
        }
        private void BuildComponents()
        {
            Base b = (Base)gameObject.AddComponent(new Base());
            gameObject.AddComponent(new SpriteRenderer());

            Collider c = (Collider)gameObject.AddComponent(new Collider());
            c.CollisionEvent.Attach(b);
            //if (c.CollisionBox.Intersects()
            //{

            //}
            gameObject.Transform.Position = new Vector2(position.X, position.Y);
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
