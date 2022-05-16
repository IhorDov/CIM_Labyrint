using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace CIM_Labyrint
{
    class LevelManager
    {
        public List<int[,]> levelHolder = new List<int[,]>();

        private GameObject gameObject;

        public LevelManager()
        {
            levelHolder.Add(LevelData.level_0);
            levelHolder.Add(LevelData.level_1);
            levelHolder.Add(LevelData.level_2);
            levelHolder.Add(LevelData.level_3);
            levelHolder.Add(LevelData.level_4);
        }

        private GameObject CreateGameObject(int whatObjects, float xPos, float yPos)
        {
            GameObject go = new GameObject();

            Director director = null;

            if (whatObjects == 0) //Alle objects skal tilføjes i denne metode
            {
                //tilføjer ground til spillet
                director = new Director(new GroundBuilder(xPos, yPos));
                go = director.Construct();

            }
            else if (whatObjects == 1) 
            {
                //tilføjer en block til spillet
                director = new Director(new BlockBuilder(xPos, yPos));
                go = director.Construct();
                
            }
            else if (whatObjects == 2)
            {
                //tilføjer en crate til spillet
                director = new Director(new CrateBuilder(xPos, yPos));
                go = director.Construct();
                
            }
            else if (whatObjects == 3)
            {
                director = new Director(new BaseBuilder(xPos, yPos));
                go = director.Construct();
              
            }
            else if (whatObjects == 4)
            {
                //tilføjer en player til spillet
                director = new Director(new PlayerBuilder(xPos, yPos));
                go = director.Construct();
                
            }

            return go;
        }

        public void LoadLevel(int targetLevel)
        {
            int[,] spawnLevel = new int[0, 0];
            gameObject = new GameObject();


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
                        if (spawnLevel[x, y] >= 0)
                        {                            
                            GameWorld.Instance.Instantiate(CreateGameObject(0, x, y));

                            GameObject newObject = CreateGameObject(spawnLevel[x, y], x, y);


                            //tilføj newObject til GameWorlds ´gameObjects liste
                            if (newObject != null)
                            {
                                GameWorld.Instance.Instantiate(newObject);

                            }
                        }

                        //Spawn object
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

        public void BuildGameObject()
        {
            LoadLevel(0);

        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
