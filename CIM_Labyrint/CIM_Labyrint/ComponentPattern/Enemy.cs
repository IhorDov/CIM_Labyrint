using database;
using Microsoft.Xna.Framework;

namespace CIM_Labyrint
{
    class Enemy : Component, IGameListner
    {
        private float Lifetime = 3600f;
        private Collider enemyCollider;
        private float cooldown = 0f; //Cooldown field

        IRepository repository;

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            sr.SetSprite("Enemy/monster10");

            enemyCollider = GameObject.GetComponent<Collider>() as Collider;
        }

        public override void Awake()
        {
            this.Speed = 120;

            GameObject.Tag = "Enemy";
        }
        public override void Update()
        {
            Lifetime--;
            if (Lifetime <= 0)
            {
                GameWorld.Instance.Destroy(GameObject);
            }
        }

        public void EnemyMoveToNode(Node<Vector2> pathNode)
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            //// The Direction of the Enemy.
            //Vector2 direction = pathNode.Data - GameObject.Transform.Position;
            //direction.Normalize(); // Normalize in Unity terms: Makes the Objects position Local instead of World.

            //// The Woker Forward Movement into the direction they look at (which is the Player here)...
            //GameObject.Transform.Position += direction * (float)(Speed * GameWorld.DeltaTime);

            //// Tells the Enemy to face the Player, but only AFTER getting their position.
            //sr.Rotation = (float)Math.Atan2(direction.Y, direction.X) - MathHelper.ToRadians(90);
        }


        private bool CheckDistance(Vector2 a, Vector2 b, int r)
        {
            //Get distance
            float l = (a - b).Length();

            //Check distance
            if (l < r) return true;
            else return false;
        }

        public void Notify(GameEvent gameEvent)
        {
            if (gameEvent is CollisionEvent)
            {
                GameWorld.Instance.Destroy(GameObject);

                if (cooldown <= 0)
                {
                    cooldown = 60;

                    GameWorld.lives--;

                    var mapper = new Mapper();
                    var provider = new SQLiteDatabaseProvider("Data Source=Data.db;Version=3;");

                    //data
                    repository = new Reading(provider, mapper);

                    repository.Open();
                    repository.UPDATELife(GameWorld.lives);
                    if (GameWorld.lives <= 0)
                    {
                        GameWorld.lives = 3;
                        repository.UPDATELife(GameWorld.lives);
                        GameWorld.Instance.Quit();
                    }
                    repository.Close();
                }
                cooldown--;
                //GameWorld.Instance.Destroy((gameEvent as CollisionEvent).Other);
            }
        }
    }
}