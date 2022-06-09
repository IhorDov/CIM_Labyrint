namespace CIM_Labyrint
{
    class Base : Component, IGameListner
    {
        private bool crateIsThere = true;

        private Collider baseCollider;

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Environment/environment_06");
            sr.LayerDepth = 0;
            sr.Rotation = 0;
            baseCollider = GameObject.GetComponent<Collider>() as Collider;
        }

        public override void Awake()
        {
            GameObject.Tag = "Base";
        }

        public void Notify(GameEvent gameEvent)
        {

            if (gameEvent is CollisionEvent ce)
            {
                Collider otherCollider = ce.Other.GetComponent<Collider>();

                if (ce.Other.Tag == "Crate")
                {
                    //Base position is crate position
                    if (baseCollider.CollisionBox.Intersects(otherCollider.CollisionBox) && crateIsThere)
                    {
                        ce.Other.Transform.Position = baseCollider.GameObject.Transform.Position;
                    }
                    crateIsThere = false;
                }
            }
        }
    }
}
