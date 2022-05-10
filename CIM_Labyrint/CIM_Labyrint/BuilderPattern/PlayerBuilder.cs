using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class PlayerBuilder : IBuilder
    {
        private GameObject gameObject;

        private PlayerBuilder levels = new PlayerBuilder();
        private float adaw;
        private float dwawa;

        public List<int[,]> levelHolder = new List<int[,]>();





        private GameObjectManeger objectManeger = GameObjectManeger.Instance;


        public void BuildGameObject()
        {
            gameObject = new GameObject();
levels.LoadLevel(0);
            BuildComponents();



            Animator animator = (Animator)gameObject.GetComponent<Animator>();

            animator.AddAnimation(BuildAnimation("Forward", new string[]
            { "Player/PlayerF_1", "Player/PlayerF_2", "Player/PlayerF_3", "Player/PlayerF_4" }));
            animator.AddAnimation(BuildAnimation("Back", new string[]
            { "Player/PlayerB_1", "Player/PlayerB_2", "Player/PlayerB_3", "Player/PlayerB_4" }));
            animator.AddAnimation(BuildAnimation("Right", new string[]
            { "Player/PlayerR_1", "Player/PlayerR_2", "Player/PlayerR_3", "Player/PlayerR_4" }));
            animator.AddAnimation(BuildAnimation("Left", new string[]
            { "Player/PlayerL_1", "Player/PlayerL_2", "Player/PlayerL_3", "Player/PlayerL_4" }));
            animator.AddAnimation(BuildAnimation("Stay", new string[]
            { "Player/PlayerF_2", "Player/PlayerF_2", "Player/PlayerF_2", "Player/PlayerF_2" }));
        }

        private void BuildComponents()
        {
            //Player p = (Player)gameObject.AddComponent(new Player());

            //levels.LoadLevel(0);

            levelHolder.Add(LevelData.level_0);
            levelHolder.Add(LevelData.level_1);
            levelHolder.Add(LevelData.level_2);
            levelHolder.Add(LevelData.level_3);
            levelHolder.Add(LevelData.level_4);

            


            gameObject.AddComponent(new SpriteRenderer());
            gameObject.AddComponent(new Animator());

            //Collider c = (Collider)gameObject.AddComponent(new Collider());
            //c.CollisionEvent.Attach(p);
        }

        private Animation BuildAnimation(string animationName, string[] spriteNames)
        {
            Texture2D[] sprites = new Texture2D[spriteNames.Length];

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = GameWorld.Instance.Content.Load<Texture2D>(spriteNames[i]);
            }

            Animation animation = new Animation(animationName, sprites, 5);

            return animation;
        }









        private Component Object(int whatObjects, float xPos, float yPos)
        {
            switch (whatObjects)
            {
                //case 0:
                //    return new Floor(xPos, yPos);
                //case 1:
                //    return new Wall(xPos, yPos);
                //case 2:
                //    return new Box(xPos, yPos);
                //case 3:
                //    return new Goal(xPos, yPos);
                case 0:
                    return gameObject.AddComponent(new Player(xPos, yPos));
            }

            return null;
        }

        public void LoadLevel(int targetLevel)
        {
            int[,] spawnLevel = new int[0, 0];

            try
            {
                spawnLevel = levelHolder[targetLevel];

                //Remove old level
                //if (objectManeger.GameObjects.Count > 0)
                //{
                //    foreach (var item in objectManeger.GameObjects)
                //    {
                //        objectManeger.Destory(item);
                //    }
                //}

                //Inscert level
                for (int y = 0; y < spawnLevel.GetLength(1); y++)
                {
                    for (int x = 0; x < spawnLevel.GetLength(0); x++)
                    {
                        //Add floor if needed
                        if (spawnLevel[x, y] > 1)
                 Object(0, x, y);

                        //Spawn object
                        Object(spawnLevel[x, y], x, y);
                    }
                }

               
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"No level of {targetLevel} number found. There are {levelHolder.Count} levels");
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("heJ" + e.Message);
            }
        }





        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
