using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CIM_Labyrint
{
    public class GameObject
    {
        private List<Component> components = new List<Component>();

        public Transform Transform { get; private set; } = new Transform();
        public List<Component> Components { get => components; set => components = value; }
        public bool Trigger { get; set; }

        public string Tag { get; set; }
        public float Speed { get; set; }

        public Vector2 Position { get; set; }

        public Component AddComponent(Component component)
        {
            component.GameObject = this;

            Components.Add(component);

            return component;
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)Components.Find(x => x.GetType() == typeof(T));
        }

        public void Awake()
        {
            for (int i = 0; i < components.Count; i++)
            {
                Components[i].Awake();
            }
        }

        public void Start()
        {
            for (int i = 0; i < components.Count; i++)
            {
                Components[i].Start();
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                Components[i].Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++)
            {
                Components[i].Draw(spriteBatch);
            }
        }
    }
}
