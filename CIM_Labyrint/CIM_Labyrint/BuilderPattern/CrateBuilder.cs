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

            gameObject.Transform.Position = new Vector2(position.X, position.Y);
        }
        private void BuildComponents()
        {

            gameObject.AddComponent(new Crate());
            gameObject.AddComponent(new SpriteRenderer());
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
