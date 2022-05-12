using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class Block : Component
    {
        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("block/block_05");
        }
    }

}