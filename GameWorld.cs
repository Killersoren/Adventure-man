using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Adventure_man
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public GameTime gameTime;

        public SpriteFont font;

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

        public enum Direction
        {
            Right,
            Left
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
            menu = new Menu();
            ui = new UI();

            _graphics.PreferredBackBufferWidth = (int)CurrentWorld.screenSize.X;
            _graphics.PreferredBackBufferHeight = (int)CurrentWorld.screenSize.Y;

            SceenSize = (GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Sets spritefont
            font = Content.Load<SpriteFont>("Font"); // My font was brokne

            collisionTexture = Content.Load<Texture2D>("CollisionTexture");


            foreach (GameObject o in CurrentWorld.Objects)
            {
                o.LoadContent(content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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
            if (getstate.IsKeyDown(Keys.Enter) && !laststate.IsKeyDown(Keys.Enter))
            {
                isGameStarted = !isGameStarted;
            }
            laststate = getstate;

            // Updates current scene
            currentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            //For getting feedback
#if DEBUG
            _spriteBatch.DrawString(font, $"Player pos= {World.Player.Location.X},{World.Player.Location.Y}", Vector2.Zero, Color.White);
            _spriteBatch.DrawString(font, $"Player Weapon cooldown ={World.Player.CurrentWeapon.cooldown}", new Vector2(0,font.LineSpacing), Color.White);
            _spriteBatch.DrawString(font, $"Player pos= {World.Player.Location.X},{World.Player.Location.Y}", new Vector2(0, font.LineSpacing*2), Color.White);
            _spriteBatch.DrawString(font, $"Player pos= {World.Player.Location.X},{World.Player.Location.Y}", new Vector2(0, font.LineSpacing*3), Color.White);
#endif

            CurrentWorld.Draw(_spriteBatch);
            currentScene.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}