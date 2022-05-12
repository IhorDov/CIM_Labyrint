using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Crate : Component
    {
 

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Crate/crate_10");
        }

        //public override void Update()
        //{
        //    GameWorld.Instance.Execute(this);
        //}
    }
}
