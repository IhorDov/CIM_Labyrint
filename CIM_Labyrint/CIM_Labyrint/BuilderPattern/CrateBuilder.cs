using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class CrateBuilder : IBuilder
    {
        private GameObject gameObject;

        private Vector2 gridPosition;

        public Vector2 position;

        public CrateBuilder(float x, float y)
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
            Crate cr = (Crate)gameObject.AddComponent(new Crate());

            gameObject.AddComponent(new Crate());
            gameObject.AddComponent(new SpriteRenderer());
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
