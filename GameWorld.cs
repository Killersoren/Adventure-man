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
        // 

        // Parallax Textures
        public Texture2D tree;
        public Texture2D cloud;
        public Texture2D ground;
        public Texture2D sun;
        //

        // Scenes
        private Scene menu;
        private Scene ui;
        private Scene currentScene;
        public bool isGameStarted = false;
        //

        //new public static GameServiceContainer Services;

        public World CurrentWorld;
        public int worldNumber = 0;

        public List<Vector2> playerLocations;
        public List<Vector2> worldSizes;
        public List<List<GameObject>> worldLayouts;
        public List<World.CompleationParamitor> worldCompleationParamitors;
        public List<List<ParallaxLayer>> parallax;

        //private Texture2D collisionTexture;

        public (int x, int y) SceenSize;
        private KeyboardState laststate;
        private Song backgroundMusic;

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
            //Services = base.Services;
            //screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            content = Content;
            gameTime = new GameTime();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //CurrentWorld = new World(
            //    new List<GameObject>
            //        {
            //        new PickUp("doublejump", new Vector2(100, 200), new Vector2(64, 64), (Player p) => { ++p.JumpAmount; }),
            //        new PickUp("BowTest",4 ,6 , new Vector2(64, 64), (Player p) => {p.PickupWeapon(new Bow("Falcon Bow", 100, 10, 5, p)); }),
            //        new PickUp("Sword",8 ,6.5f, new Vector2(64, 32), (Player p) => {p.PickupWeapon(new Sword("Sword", 100, 10, 5, p)); }),
            //        new Player(0, 5),
            //        new Enemy(9, 4),
            //        new Platform(0, 7, 13, 1,true),
            //        new Platform(4, 4, 2, 1,true),
            //        new Platform(7, 2, 2, 1,true),
            //        new PickUp("", new Vector2(500, 50), new Vector2(100, 100), (Player p) =>
            //            {
            //            Program.AdventureMan.CurrentWorld = new World(
            //                new List<GameObject>
            //                {
            //                p,
            //                new Platform(0, 650, 1920, 64),
            //                new Platform(300, 500, 100, 64),
            //                new Platform(200, 300, 100, 64),
            //                new Platform(450, 200, 100, 64),
            //                new Enemy(9, 4),
            //            }
            //        );
            //        }
            //    ),
            //    }
            //);
            GenerateWorlds();
            CurrentWorld = new World(parallax[worldNumber], playerLocations[worldNumber], worldSizes[worldNumber], worldLayouts[worldNumber], worldCompleationParamitors[worldNumber]);

            _graphics.PreferredBackBufferWidth = (int)CurrentWorld.worldSize.X;
            _graphics.PreferredBackBufferHeight = (int)CurrentWorld.worldSize.Y;
            SceenSize = (GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height);

            // Creates each scene
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
            //

            foreach (GameObject o in CurrentWorld.Objects)
            {
                o.LoadContent(content);
            }

            ///MUSIC
            backgroundMusic = Content.Load<Song>("Background Music");
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
        }

        protected override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;



            // Ras - Pauses game by changing scene and not running game updates
            if (isGameStarted)
            {
                currentScene = ui;
                CurrentWorld.Update();
            }
            else
            {
                currentScene = menu;
            }
            //

            // Ras - gets player input,
            // if input = escape, sets isgamestarted
            // Debug : swaps escape with enter 
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

            // Updates current scene
            currentScene.Update();

            if (CurrentWorld.forCompleation())
            {
                ChangeWorld();
            }
           

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //_spriteBatch.Begin(SpriteSortMode.BackToFront);
            _spriteBatch.Begin();

            //For getting feedback
            //#if DEBUG
            //            _spriteBatch.DrawString(font, $"Player pos= {World.Player.Location.X},{World.Player.Location.Y}", Vector2.Zero, Color.White);
            //            _spriteBatch.DrawString(font, $"Player Weapon cooldown ={World.Player.CurrentWeapon.cooldown}", new Vector2(0,font.LineSpacing), Color.White);
            //            _spriteBatch.DrawString(font, $"Player Health= {World.Player.health}", new Vector2(0, font.LineSpacing*2), Color.White);
            //            _spriteBatch.DrawString(font, $"", new Vector2(0, font.LineSpacing*3), Color.White);
            //#endif

            CurrentWorld.Draw(_spriteBatch);
            currentScene.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

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

        private void GenerateWorlds()
        {
            // Ras - Textures for parallax, called from gameworld loadcontent or ? 
            tree = Program.AdventureMan.content.Load<Texture2D>("tree");
            cloud = Program.AdventureMan.content.Load<Texture2D>("clouds");
            ground = Program.AdventureMan.content.Load<Texture2D>("ground");
            sun = Program.AdventureMan.content.Load<Texture2D>("sun");

            parallax = new List<List<ParallaxLayer>>
            {
                new List<ParallaxLayer>
                {
                    new ParallaxLayer(sun, 5f, 1,new Vector2(-900,0),true),
                    new ParallaxLayer(cloud,20f,1,new Vector2(-400,0),true),
                    new ParallaxLayer(ground, 0f, 1, new Vector2(0,-600)),
                    new ParallaxLayer(tree, 2f, 7, new Vector2(0,-200)),
                    new ParallaxLayer(tree, 3f, 7, new Vector2(100,-250)),
                },
                new List<ParallaxLayer>
                {
                   new ParallaxLayer(sun, 5f, 1,new Vector2(-900,0),true),
                    new ParallaxLayer(cloud,20f,1,new Vector2(-400,0),true),
                    new ParallaxLayer(ground, 0f, 1, new Vector2(0,-600)),
                    new ParallaxLayer(tree, 3f, 7,new Vector2(100,-250)),
                },
                new List<ParallaxLayer>
                {
                   new ParallaxLayer(sun, 5f, 1,new Vector2(-900,0),true),
                    new ParallaxLayer(ground, 0f, 1, new Vector2(0,-600)),
                    new ParallaxLayer(tree, 2f, 7, new Vector2(0,-200)),
                    new ParallaxLayer(tree, 3f, 7, new Vector2(100,-250)),
                },

            };

            playerLocations = new List<Vector2>
            {
                new Vector2(0*World.GridResulution,8*World.GridResulution),
                new Vector2(0*World.GridResulution,1*World.GridResulution),
                new Vector2(0*World.GridResulution,1*World.GridResulution)
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
                       new PickUp("Sword",3 ,10f, new Vector2(64, 32), (Player p) => {p.PickupWeapon(new Sword("Sword", 100, 10, 5, p)); }),
                    new Enemy(9, 4, "Sword"),
                    new Enemy(8, 4, "Sword"),
                    new Platform(0, 7, 10, 1,true),
                    new Platform(9, 8, 2, 1,true),
                    new Enemy(18, 8, "Sword"),

                },
                new List<GameObject>
                {
                      new PickUp("doublejump", new Vector2(360, 570), new Vector2(64, 64), (Player p) => { ++p.JumpAmount; }),

                    new Platform(200, 400, 400, 64),
                    new Platform(200, 550, 100, 130),
                    new Platform(500, 600, 100, 130),
                    new Platform(1000, 400, 100, 100),
                    new Platform(1000, 600, 100, 100),

                    new Enemy(6, 4, "Bow"),
                    new Enemy(5, 4, "Bow"),
                    new Enemy(18, 4, "Bow"),
                    new Enemy(10, 4, "Sword"),
                    new Enemy(11, 4, "Sword"),

                },
                new List<GameObject>
                {

                     new PickUp("BowTest",4 ,4 , new Vector2(64, 64), (Player p) => {p.PickupWeapon(new Bow("Falcon Bow", 100, 10, 5, p)); }),

                    new Platform(200, 400, 400, 64),
                    new Platform(200, 600, 100, 130),

                    new Enemy(9, 4, "Sword"),
                    new Enemy(10, 4, "Bow"),
                    new Enemy(6, 4, "Sword"),
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
                    //int enemies=0;
                    //foreach(GameObject go in Program.AdventureMan.CurrentWorld.GameObjects)
                    //    if (go is Enemy)
                    //        enemies++;

                    //if (enemies==0)
                    //    return true;
                    //else
                    //    return false;
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
                    //int enemies=0;
                    //foreach(GameObject go in Program.AdventureMan.CurrentWorld.GameObjects)
                    //    if (go is Enemy)
                    //        enemies++;

                    //if (enemies==0)
                    //    return true;
                    //else
                    //    return false;
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