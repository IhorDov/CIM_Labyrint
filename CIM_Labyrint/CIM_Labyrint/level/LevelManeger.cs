using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class LevelManeger : Component , IBuilder
    {

        public List<int[,]> levelHolder = new List<int[,]>();

        private GameObject gameObject;



        private Player player = new Player(new float() , new float());




        public LevelManeger()
        {
            levelHolder.Add(LevelData.level_0);
            levelHolder.Add(LevelData.level_1);
            levelHolder.Add(LevelData.level_2);
            levelHolder.Add(LevelData.level_3);
            levelHolder.Add(LevelData.level_4);
        }

        public Player PlayerObject(int whatObjects, float xPos, float yPos)
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
                case 4:
                    return new Player(xPos, yPos);
            }

            return null;
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
                        if (spawnLevel[x, y] > 1)
                            PlayerObject(0, x, y);

                        //Spawn object
                        gameObject.AddComponent(PlayerObject(spawnLevel[x, y], x, y));
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
