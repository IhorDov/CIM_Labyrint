using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{

    class SpriteRenderer : Component
    {
        public int number = 0;
        public Rectangle rectangle;

                private GameObject gameObject;



        public Texture2D Sprite { get; set; }
        public Vector2 Origin { get; set; }

        public override void Start()
        {
            gameObject = new GameObject();

            Origin = new Vector2(Sprite.Width / 3, Sprite.Height / 3);
            rectangle = new Rectangle((int)gameObject.Transform.Position.X, (int)gameObject.Transform.Position.Y, Sprite.Width, Sprite.Height);

        }
        public void SetSprite(string spriteName)
        {
            Sprite = GameWorld.Instance.Content.Load<Texture2D>(spriteName);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, GameObject.Transform.Position, rectangle, Color.White, 0, Origin, 0.7f, SpriteEffects.None, 0);
        }
    }
}