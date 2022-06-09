using database;

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