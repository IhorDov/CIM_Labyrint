using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class GameObjectManeger
    {
        //Singelton
        private static GameObjectManeger instance = null;
        private ContentManager content;

        private GameObjectManeger()
        {
        }

        public static GameObjectManeger Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameObjectManeger();

                return instance;
            }
        }

        //Game object adding & removeing
        List<GameObject> addedGameObjects = new List<GameObject>(); //For gameobjectets der skal komme i gameObjevt listen
        List<GameObject> gameObjects = new List<GameObject>(); //For main loop
        List<GameObject> removingGameObjects = new List<GameObject>(); //For ting der skal fjernes
        //Collision

        #region Properties
        //GameObject
        public List<GameObject> GameObjects { get => gameObjects; set => gameObjects = value; }
        //Colision
        #endregion

        public void SetContentManeger(ContentManager content)
        {
            this.content = content;
        }


        public void UpdateLoop()
        {
            //Add if new gameobjects is added
            //AddGameObjectToWorld();

            //Remove gameobjebts from the list
         
        }

        public void Instantiate(GameObject target)
        {
            addedGameObjects.Add(target);
        }

        public void Destory(GameObject target)
        {
            removingGameObjects.Add(target);
        }

        public void ClearSceen()
        {
            foreach (var item in gameObjects)
            {
                Destory(item);
            }
        }

        //private void AddGameObjectToWorld()
        //{
        //    if (addedGameObjects.Count > 0)
        //    {
        //        for (int i = 0; i < addedGameObjects.Count; i++)
        //        {
        //            addedGameObjects[i].LoadContent(content);
        //            gameObjects.Add(addedGameObjects[i]);

        //            //Add to collision if needed
        //            if (addedGameObjects[i] is GameObjectWithCollider)
        //                collisionList.Add((GameObjectWithCollider)addedGameObjects[i]);
        //        }
        //        addedGameObjects.Clear();
        //    }
        //}

        //private void RemoveGameObjectFromWorld()
        //{
        //    if (removingGameObjects.Count > 0)
        //    {
        //        for (int i = 0; i < removingGameObjects.Count; i++)
        //        {
        //            gameObjects.Remove(removingGameObjects[i]);

        //            //Remove from collision
        //            if (removingGameObjects[i] is GameObjectWithCollider)
        //                collisionList.Remove((GameObjectWithCollider)removingGameObjects[i]);
        //        }
        //        removingGameObjects.Clear();
        //    }
        //}



    }
}
