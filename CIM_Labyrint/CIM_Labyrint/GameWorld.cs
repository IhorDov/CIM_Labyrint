using database;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;


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
        public static int lives; //We make static field for life
        public static int score;      //Static field for score
        private int highScore;        //Create field for highScore

        IRepository repository;


        private Character mapScore;

        private Life Life;
        private Musik musik;

        private static bool start = false; //startmenu stuff


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

            repository.AddLife(3);


            //Maper life
            Life = repository.GetAllLife(1);


            // Maper
            mapScore = repository.GetAllScore(1);

            musik = repository.GetAlltru(1);



            // stop  vi stopper databasen for at stoppe memory leak
            repository.Close();

        }


        protected override void Initialize()
        {
            //laver skærmstørrelse
            ScreenSize();
      
            // Definer hvilket level den skal være
            LevelManager levelManager = new LevelManager();
            levelManager.LoadLevel(0);
            lives = int.Parse(Life.life);

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

            //Make here option to start game when we push Key B
            if (!start)
            {
                KeyboardState keyState = Keyboard.GetState();
                if (keyState.IsKeyDown(Keys.B))
                {
                    start = true;
                }
            }
            else
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();


                DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                for (int i = 0; i < gameObjects.Count; i++)
                {
                    gameObjects[i].Update(gameTime);
                }

                Cleanup();

                KeyboardState keyState = Keyboard.GetState();

                if (keyState.IsKeyDown(Keys.V) && soundTap == true) //Toggle music and sounds
                {
                    repository.Open();
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
                        repository.Addmusik(sound);
                        MediaPlayer.Resume(); //if sound on, resume playing
                    }
                    else
                    {
                        repository.Addmusik(sound);
                        MediaPlayer.Pause(); //if off stop playing
                    }
                    repository.Close();
                }
                if (keyState.IsKeyUp(Keys.V))
                {
                    soundTap = true; //prevent running each time.
                }

                if (lives < 0) //If dead, pause all logic in the game.
                {
                    if (score > highScore) //Set new highscore
                    {
                        highScore = score;
                    }

                    MediaPlayer.Pause();

                    if (keyState.IsKeyDown(Keys.R)) //Restart game
                    {
                        RestartGame();

                        if (sound)
                        {
                            MediaPlayer.Resume();
                        }
                    }
                }


                base.Update(gameTime);
            }
        }

        public void RestartGame()
        {
            Initialize();
            LoadContent();
            repository.Open();
            lives = 3;
            score = 0;
            repository.UPDATELife(lives);
            repository.Close();

        }
        private void EndScreen()
        {
            //We have info efter end of game
            string stringTemp = $"      GAME OVER\n You Achived a score \n               {score}" +
                $"\n with a Maximum score \n               {highScore}";
            Vector2 stringSize = text.MeasureString(stringTemp);
            spriteBatch.DrawString(text, stringTemp, new Vector2(GraphicsDevice.Viewport.Width / 2 - stringSize.X,
               GraphicsDevice.Viewport.Height / 2 - (int)(stringSize.Y * 1.5)), Color.White, 0, new Vector2(0, 0), 2f, 0, 0);
            stringTemp = "Press R to retry";
            stringSize = text.MeasureString(stringTemp);
            spriteBatch.DrawString(text, stringTemp, new Vector2(GraphicsDevice.Viewport.Width / 2 - stringSize.X,
                GraphicsDevice.Viewport.Height / 2 + (int)(stringSize.Y * 2.5)), Color.Yellow, 0, new Vector2(0, 0), 2f, 0, 0);
        }

        /// <summary>
        /// Method that is active when we start the game
        /// </summary>
        private void StartScreen()
        {
            if (start)
            {
                return;
            }

            //We write what we need to do to start game
            string stringTemp = "Press B to send your player in Labyrint.";
            Vector2 stringSize = text.MeasureString(stringTemp);

            spriteBatch.DrawString(text, stringTemp, new Vector2(GraphicsDevice.Viewport.Width / 2 - stringSize.X,
                GraphicsDevice.Viewport.Height / 2 - stringSize.Y) + new Vector2(1 * 2f, 1 * 2), Color.Black, 0, Vector2.Zero, 2f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(text, stringTemp, new Vector2(GraphicsDevice.Viewport.Width / 2 - stringSize.X,
                GraphicsDevice.Viewport.Height / 2 - stringSize.Y) + new Vector2(-1 * 2f, 1 * 2), Color.Black, 0, Vector2.Zero, 2f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(text, stringTemp, new Vector2(GraphicsDevice.Viewport.Width / 2 - stringSize.X,
                GraphicsDevice.Viewport.Height / 2 - stringSize.Y) + new Vector2(-1 * 2f, -1 * 2), Color.Black, 0, Vector2.Zero, 2f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(text, stringTemp, new Vector2(GraphicsDevice.Viewport.Width / 2 - stringSize.X,
                GraphicsDevice.Viewport.Height / 2 - stringSize.Y) + new Vector2(1 * 2f, -1 * 2), Color.Black, 0, Vector2.Zero, 2f, SpriteEffects.None, 1f);

            spriteBatch.DrawString(text, stringTemp, new Vector2(GraphicsDevice.Viewport.Width / 2 - stringSize.X,
                GraphicsDevice.Viewport.Height / 2 - stringSize.Y), Color.Gold, 0, Vector2.Zero, 2f, 0, 0);

        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront);

            spriteBatch.DrawString(text, $"Score: {mapScore.Score}\nLives: {lives}\n\nSound (V): {sound}", new Vector2(0, 0), Color.White);

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw(spriteBatch);
            }

            //if (lives <= 1) //If dead, call EndScreen draw method.
            //{
            //    EndScreen();  //Call method EndScreen
            //}
            StartScreen();



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
        /// <summary>
        /// destroyer game objects
        /// </summary>
        /// <param name="god"></param>
        public void Destroy(GameObject god)
        {
            destroyedGameObjects.Add(god);
        }


        public void Quit()
        {
            this.Exit();
        }

        /// <summary>
        /// så får du rydder op på den rigtige måde så at vi er sikker på at alting er slettet i Game World
        /// </summary>
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