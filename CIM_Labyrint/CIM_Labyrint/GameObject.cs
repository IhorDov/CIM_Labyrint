using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    public class GameObject
    {
        private List<Component> components = new List<Component>();



        public Vector2 position;
        public float rotaton;
        public Vector2 scale = Vector2.One;
        public Vector2 gridPosition;

        //Rendering
        public float layerDepth;
        protected SpriteEffects effect;
        public Rectangle rectangle;

        //Animation
        protected Texture2D sprite;
        protected Texture2D[] animations;
        protected float animationSpeed;

        public Transform Transform { get; private set; } = new Transform();



        public GameObject()
        {
    
            this.position = GridPlacement.Placement(gridPosition);
        }

        //public Vector2 Origen
        //{
        //    get
        //    {
        //        if (sprite != null)
        //        {
        //            return new Vector2(sprite.Width / 2, sprite.Height / 2);
        //        }
        //        return Vector2.Zero;
        //    }
        //}


        public Component AddComponent(Component component)
        {
            component.GameObject = this;

            components.Add(component);

            return component;
        }

        public Component GetComponent<T>() where T : Component
        {
            return components.Find(x => x.GetType() == typeof(T));
        }

        public void Awake()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Awake();
            }
        }

        public void Start()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Start();
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Draw(spriteBatch);
            }
        }

    }
}
