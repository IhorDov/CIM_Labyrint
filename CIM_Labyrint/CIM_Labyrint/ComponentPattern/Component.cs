using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    public abstract class Component
    {


        private float speed;
        public GameObject GameObject { get; set; }
        public float Speed { get => speed; set => speed = value; }

        public virtual void Awake() { }

        public virtual void Start() { }

        public virtual void Update() { }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
