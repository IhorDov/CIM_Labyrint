using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

using database;


namespace CIM_Labyrint
{
    public class GameWorld : Game
    {
        private static GameWorld instance;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static bool sound = false; //Play soundeffects and music.
        private bool soundTap = true; //Used to prevent music spam.
        private SpriteFont text; //A single spritefront for the text (viewing score)
        public static int lives = 3; //We make static field for life
        public static int score;      //Static field for score
        private int highScore;        //Create field for highScore

        IRepository repository;


        private Character mapScore;


        //game gameObjects
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> newGameObjects = new List<GameObject>();
        private List<GameObject> destroyedGameObjects = new List<GameObject>();

        //Colliders
        public List<Collider> Colliders { get; private set; } = new List<Collider>();

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

            Data();

        }


       private void Data()
        {
            // TODO: Add your initialization logic here
            var mapper = new Mapper();
            var provider = new SQLiteDatabaseProvider("Data Source=Data.db;Version=3;new=true");

            // Til så Tingene bliver slettet bagefter når man er i gang med at teste
            // var provider = new SQLiteDatabaseProvider("Data Source=:memory:;Version=3;New=true");

            //data
            repository = new Reading(provider, mapper);

            // start
            repository.Open();

            // den husker stadigvæk tingene når du tager den væk over det hele kører som normalt
            repository.AddScore(100);

            // Maper
            mapScore = repository.GetAllScore(1);

            // stop  vi stopper databasen for at stoppe memory leak
            repository.Close();

        }


        protected override void Initialize()
        {
            //laver skærmstørrelse
            ScreenSize();

            // Definer hvilket level den skal være
            LevelManager levelManager = new LevelManager();
            levelManager.LoadLevel(1);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Start();
            }

            //MediaPlayer.IsRepeating = true;          //Set MediaPlayer to true
            //Song music = Content.Load<Song>("The-Northern-Path"); //Download music
            //MediaPlayer.Play(music);                       //Create a MediaPlayer to play downloaded music
            //MediaPlayer.Pause();                           //Start, and pause music. Toggable later in the code.
            text = Content.Load<SpriteFont>("File"); //Download sprite fond

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

            base.Update(gameTime);

            Cleanup();

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.V) && soundTap == true) //Toggle music and sounds
            {
                if (sound == false)
                {
                    sound = true;
                }
                else
                {
                    sound = false;
                }
                soundTap = false;

                if (sound)
                {
                    MediaPlayer.Resume(); //if sound on, resume playing
                }
                else
                {
                    MediaPlayer.Pause(); //if off stop playing
                }
            }
            if (keyState.IsKeyUp(Keys.V))
            {
                soundTap = true; //prevent running each time.
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront);


            spriteBatch.DrawString(text, $"Score: {mapScore.Score}\nLives: {lives}\n\nSound (V): On", new Vector2(0, 0), Color.White);

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

        public void Destroy(GameObject go)
        {
            destroyedGameObjects.Add(go);
        }

        private void Cleanup()
        {
            for (int i = 0; i < newGameObjects.Count; i++)
            {
                gameObjects.Add(newGameObjects[i]);
                newGameObjects[i].Awake();
                newGameObjects[i].Start();

                Collider c = (Collider)newGameObjects[i].GetComponent<Collider>();
                if (c != null)
                {
                    Colliders.Add(c);
                }
            }

            for (int i = 0; i < destroyedGameObjects.Count; i++)
            {
                Collider c = (Collider)destroyedGameObjects[i].GetComponent<Collider>();

                gameObjects.Remove(destroyedGameObjects[i]);

                if (c != null)
                {
                    Colliders.Remove(c);
                }
            }
            destroyedGameObjects.Clear();
            newGameObjects.Clear();
        }
    }
}