using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Adventure_man
{
    public class GameWorld : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public GameTime gameTime;
        public ContentManager content;

        // Fonts
        public SpriteFont font;

        public SpriteFont altFont;
        public SpriteFont menuFont;

        // Scenes
        private Scene menu;

        private Scene ui;
        private Scene currentScene;
        public bool isGameStarted = false;

        public World CurrentWorld;
        public int worldNumber = 0;

        public List<Vector2> playerLocations;
        public List<Vector2> worldSizes;
        public List<List<GameObject>> worldLayouts;
        public List<World.CompleationParamitor> worldCompleationParamitors;
        public List<List<ParallaxLayer>> parallax;

        public (int x, int y) SceenSize;
        private KeyboardState laststate;
        private Song backgroundMusic;

        /// <summary>
        /// Sets direction to eihter +1 or -1, used to decide which direction and velocity objects have
        /// </summary>
        public enum Direction : int
        {
            Right = 1,
            Left = -1
        }

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            content = Content;
            gameTime = new GameTime();
        }

        /// <summary>
        /// Sets screensize, current world and scene
        /// </summary>
        protected override void Initialize()
        {
            GenerateWorlds();
            CurrentWorld = new World(parallax[worldNumber], playerLocations[worldNumber], worldSizes[worldNumber], worldLayouts[worldNumber], worldCompleationParamitors[worldNumber]);

            _graphics.PreferredBackBufferWidth = (int)CurrentWorld.worldSize.X;
            _graphics.PreferredBackBufferHeight = (int)CurrentWorld.worldSize.Y;
            SceenSize = (GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height);

            menu = new Menu();
            ui = new UI();

            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Sets spritefont
            font = Content.Load<SpriteFont>("Font");
            altFont = Content.Load<SpriteFont>("AltFont");
            menuFont = Content.Load<SpriteFont>("MenuFont");

            foreach (GameObject o in CurrentWorld.Objects)
            {
                o.LoadContent(content);
            }

            ///MUSIC
            backgroundMusic = Content.Load<Song>("Background Music");
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// Ras - Updates currentworld if bool is true and sets currentscene
        /// </summary>
        /// <returns></returns>
        private void GameState()
        {
            if (isGameStarted)
            {
                currentScene = ui;
                CurrentWorld.Update();
            }
            else
            {
                currentScene = menu;
            }
        }

        /// <summary>
        /// Ras - gets player input, if input = escape, flips bool isgamestarted
        /// Debug : swaps escape with enter
        /// </summary>
        private void PlayerInput()
        {
            var getstate = Keyboard.GetState();
#if DEBUG
            if (getstate.IsKeyDown(Keys.Escape) && !laststate.IsKeyDown(Keys.Escape))
                Exit();
            if (getstate.IsKeyDown(Keys.Enter) && !laststate.IsKeyDown(Keys.Enter))
            {
                isGameStarted = !isGameStarted;
            }
#endif
            if (getstate.IsKeyDown(Keys.Escape) && !laststate.IsKeyDown(Keys.Escape))
            {
                isGameStarted = !isGameStarted;
            }
            laststate = getstate;
        }

        /// <summary>
        /// Updates current scene and world.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;

            GameState();
            PlayerInput();

            // Updates current scene
            currentScene.Update();

            if (CurrentWorld.forCompleation()) // if conmpleation peramitors have been meet it changes to the next world.
            {
                ChangeWorld();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws currentworld/scene
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            CurrentWorld.Draw(_spriteBatch);
            currentScene.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Changes to the next world, if there is a next world
        /// /// </summary>
        private void ChangeWorld()
        {
            worldNumber++;
            if (worldNumber < worldLayouts.Count)
            {
                CurrentWorld = new World(parallax[worldNumber], playerLocations[worldNumber], worldSizes[worldNumber], worldLayouts[worldNumber], worldCompleationParamitors[worldNumber]);
                foreach (GameObject go in CurrentWorld.GameObjects)
                    go.LoadContent(content);
            }
        }

        /// <summary>
        /// Sofie- Generates all of the possible worlds
        /// </summary>
        private void GenerateWorlds()
        {
            parallax = new List<List<ParallaxLayer>>
            {
                new List<ParallaxLayer>
                {
                    new ParallaxLayer(World.sun, 5f, 1,new Vector2(-900,0),true),
                    new ParallaxLayer(World.cloud,20f,1,new Vector2(-400,0),true),
                    new ParallaxLayer(World.ground, 0f, 1, new Vector2(0,-600)),
                    new ParallaxLayer(World.tree, 2f, 7, new Vector2(0,-200)),
                    new ParallaxLayer(World.tree, 3f, 7, new Vector2(100,-250)),
                },
                new List<ParallaxLayer>
                {
                   new ParallaxLayer(World.sun, 5f, 1,new Vector2(-900,0),true),
                    new ParallaxLayer(World.cloud,20f,1,new Vector2(-400,0),true),
                    new ParallaxLayer(World.ground, 0f, 1, new Vector2(0,-600)),
                    new ParallaxLayer(World.tree, 3f, 7,new Vector2(100,-250)),
                },
                new List<ParallaxLayer>
                {
                   new ParallaxLayer(World.sun, 5f, 1,new Vector2(-900,0),true),
                    new ParallaxLayer(World.ground, 0f, 1, new Vector2(0,-600)),
                    new ParallaxLayer(World.tree, 2f, 7, new Vector2(0,-200)),
                    new ParallaxLayer(World.tree, 3f, 7, new Vector2(100,-250)),
                },
            };

            playerLocations = new List<Vector2>
            {
                new Vector2(0*World.GridResulution,8*World.GridResulution),
                new Vector2(0*World.GridResulution,8*World.GridResulution),
                new Vector2(0*World.GridResulution,8*World.GridResulution)
            };

            worldSizes = new List<Vector2>
            {
                new Vector2(20,11),
                new Vector2(20,11),
                new Vector2(20,11)
            };
            worldLayouts = new List<List<GameObject>>
            {
                new List<GameObject>
                {
                    new PickUp("Sword", 3f, 9, new Vector2(64, 32), (Player p) => {p.PickupWeapon(new Sword("Sword", 75, 5)); }),
                    new Enemy(9, 4, "Sword"),
                    new Enemy(8, 4, "Sword"),
                    new Platform(0, 7, 10, 1,true),
                    new Platform(9, 8, 2, 1,true),
                    new Enemy(18, 8, "Sword"),
                },
                new List<GameObject>
                {
                      new PickUp("doublejump", new Vector2(360, 538), new Vector2(64, 64), (Player p) => { ++p.JumpAmount; }),

                    new Platform(200, 368, 400, 64),
                    new Platform(200, 518, 100, 130),
                    new Platform(500, 568, 100, 130),
                    new Platform(1000, 368, 100, 100),
                    new Platform(1000, 568, 100, 100),

                    new Enemy(6, 3.5f, "Bow"),
                    new Enemy(5, 3.5f, "Bow"),
                    new Enemy(18, 4, "Bow"),
                    new Enemy(10, 4, "Sword"),
                    new Enemy(11, 4, "Sword"),
                },
                new List<GameObject>
                {
                     new PickUp("BowTest",4 ,4 , new Vector2(64, 64), (Player p) => {p.PickupWeapon(new Bow("Falcon Bow", 75, 10, 5)); }),

                    new Platform(200, 368, 400, 64),
                    new Platform(200, 568, 100, 130),

                    new Enemy(9, 3.5f, "Sword"),
                    new Enemy(10, 4, "Bow"),
                    new Enemy(6, 3.5f, "Sword"),
                    new Enemy(16, 4, "Bow"),
                    new Enemy(17, 4, "Bow"),
                    new Enemy(18, 4, "Sword"),
                    new Enemy(15, 4, "Sword"),
                }
            };
            worldCompleationParamitors = new List<World.CompleationParamitor>
            {
                () =>
                {
                    if (World.Player.points>=10)
                        {
                        World.Player.crouched = false;
                        return true;
                    }
                    else
                        return false;
                },
                () =>
                {
                    if (World.Player.points>=30)
                         {
                        World.Player.crouched = false;
                        return true;
                    }
                    else
                        return false;
                },
                () =>
                {
                    return false;
                }
            };
        }
    }
}