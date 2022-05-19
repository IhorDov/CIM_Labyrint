using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Ground : Component
    {
        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Ground/ground_06");
            sr.LayerDepth = 0.1f;
        }
    }
}
