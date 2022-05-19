using System;
using System.Collections.Generic;
using System.Text;

namespace CIM_Labyrint
{
    class GameObjectsPool
    {
        protected List<GameObject> active = new List<GameObject>();

        protected Stack<GameObject> inactive = new Stack<GameObject>();

        //Singelton
        private static GameObjectsPool instance = null;

        public static GameObjectsPool Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameObjectsPool();

                return instance;
            }
        }

        public GameObject GetObject()
        {
            if (inactive.Count == 0)
            {
                return CreateObject();
            }
            GameObject go = inactive.Pop();
            active.Add(go);
            return go;
        }

        public void ReleaseObject(GameObject gameObject)
        {
            active.Remove(gameObject);
            inactive.Push(gameObject);
            GameWorld.Instance.Destroy(gameObject);
            //CleanUp(gameObject);
        }

        public GameObject CreateObject()
        {
            return new GameObject();
        }

        //public void CleanUp(GameObject gameObject) { }
    }
}

