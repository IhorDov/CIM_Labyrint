using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace CIM_Labyrint
{
    public class GameWorld : Game
    {
        private static GameWorld instance;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //game gameObjects
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> newGameObjects = new List<GameObject>();
        private List<GameObject> destroyedGameObjects = new List<GameObject>();


        private Thread internalThread;

        //Colliders
        //public List<Collider> Colliders { get; private set; } = new List<Collider>();



        public static float DeltaTime { get; private set; }
        public GraphicsDeviceManager Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }



        protected override void Initialize()
        {
            LevelManager levelManager = new LevelManager();

            ScreenSize();
levelManager.startd(0);
            
            internalThread = new Thread(levelManager.StartLoadLevel);

         


            

            gameObjects.AddRange(newGameObjects);
            
            newGameObjects.Clear();

   internalThread.Start();

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Awake();
            }

 

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);



            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Start();
            }
        }





        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Update(gameTime);
            }

            gameObjects.AddRange(newGameObjects);
            newGameObjects.Clear();

            base.Update(gameTime);


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
        private void ScreenSize()
        {
            Graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height - 150; //sets the height of the window
            Graphics.PreferredBackBufferWidth = Graphics.PreferredBackBufferHeight / 300 * 600; //sets the width of the window
            Graphics.ApplyChanges(); //applies the changes
        }

        /// <summary>
        /// Instantiate during runtime
        /// </summary>
        /// <param name="go"></param>
        public void Instantiate(GameObject go)
        {
            newGameObjects.Add(go);
        }
    }
}