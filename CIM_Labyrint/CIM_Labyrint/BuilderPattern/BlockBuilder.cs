using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class BlockBuilder : IBuilder
    {
        private GameObject gameObject;

        private Vector2 gridPosition;

        public Vector2 position;

        public BlockBuilder(float x, float y)
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
            Block block = (Block)gameObject.AddComponent(new Block());
            gameObject.AddComponent(new SpriteRenderer());

            Collider c = (Collider)gameObject.AddComponent(new Collider());
            c.CollisionEvent.Attach(block);

            gameObject.Transform.Position = new Vector2(position.X, position.Y);
        }


        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
