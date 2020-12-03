using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Adventure_man
{
    public class GameWorld : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public GameTime gameTime;

        public SpriteFont font;
        public SpriteFont altFont;
        

        //new public static GameServiceContainer Services;

        private Scene currentScene;
        public bool isGameStarted = false;

        public World CurrentWorld;

        private Texture2D collisionTexture;

        public ContentManager content;
        public (int x, int y) SceenSize;
        private KeyboardState laststate;

        private Scene menu;
        private Scene ui;

        public enum Direction : int
        {
            Right=1,
            Left=-1
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
            CurrentWorld = new World();

            // Creates each scene
       

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
            font = Content.Load<SpriteFont>("Font"); // My font was brokne
            altFont = Content.Load<SpriteFont>("AltFont");


            collisionTexture = Content.Load<Texture2D>("CollisionTexture");


            foreach (GameObject o in CurrentWorld.Objects)
            {
                o.LoadContent(content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;


            // Updates current scene

            // Pauses game by changing scene and not running game updates
            if (isGameStarted)
            {
                currentScene = ui;
                CurrentWorld.Update();
            }
            else
            {
                currentScene = menu;

            }
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

            currentScene.Update(gameTime);

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //For getting feedback
//#if DEBUG
//            _spriteBatch.DrawString(font, $"Player pos= {World.Player.Location.X},{World.Player.Location.Y}", Vector2.Zero, Color.White);
//            _spriteBatch.DrawString(font, $"Player Weapon cooldown ={World.Player.CurrentWeapon.cooldown}", new Vector2(0,font.LineSpacing), Color.White);
//            _spriteBatch.DrawString(font, $"Player Health= {World.Player.health}", new Vector2(0, font.LineSpacing*2), Color.White);
//            _spriteBatch.DrawString(font, $"", new Vector2(0, font.LineSpacing*3), Color.White);
//#endif

            CurrentWorld.Draw(_spriteBatch);
            currentScene.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}