using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Block : Component
    {
        public float XPos { get; set; }
        public float YPos { get; set; }

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Bloks/block_05");
        }
    }
}
