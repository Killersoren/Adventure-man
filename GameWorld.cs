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

        //new public static GameServiceContainer Services;

        private static Scene currentScene;

        public static World currentWorld;
        private World world1;

        private Texture2D collisionTexture;


        public static ContentManager content;
        public static (int x, int y) SceenSize;

        Scene menu;
        Scene ui;


        public static List<Scene> loadedScenes;
                 
       



        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //Services = base.Services;
            //screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            content = Content;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            world1 = new World();
            currentWorld = world1;
            _graphics.PreferredBackBufferWidth =(int)currentWorld.screenSize.X;
            _graphics.PreferredBackBufferHeight = (int)currentWorld.screenSize.Y;

            SceenSize = (GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            menu = new Menu();
            ui = new UI();

            loadedScenes = new List<Scene>();
            loadedScenes.Add(ui);
            loadedScenes.Add(menu);


            collisionTexture = Content.Load<Texture2D>("CollisionTexture");

            foreach (GameObject o in currentWorld.Objects)
            {
                o.LoadContent();
            }


        }

       

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (GameObject o in currentWorld.Objects)
            {
                o.Update(gameTime);
            }


            foreach (Scene scenes in loadedScenes)
            {
                scenes.Update(gameTime);
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();


            foreach (Scene scenes in loadedScenes)
            {
                scenes.Draw(gameTime, _spriteBatch);

            }

            foreach (GameObject o in currentWorld.Objects)
            {
                o.Draw(gameTime, _spriteBatch);
#if DEBUG
                DrawCollisionBox(o);
#endif

            }



            _spriteBatch.End();

            base.Draw(gameTime);
        }



        private void DrawCollisionBox(GameObject go)
        {
            Rectangle topLine = new Rectangle(go.CollisionBox.X, go.CollisionBox.Y, go.CollisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(go.CollisionBox.X, go.CollisionBox.Y + go.CollisionBox.Height, go.CollisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(go.CollisionBox.X + go.CollisionBox.Width, go.CollisionBox.Y, 1, go.CollisionBox.Height);
            Rectangle leftLine = new Rectangle(go.CollisionBox.X, go.CollisionBox.Y, 1, go.CollisionBox.Height);

            _spriteBatch.Draw(collisionTexture, topLine, Color.Red);
            _spriteBatch.Draw(collisionTexture, bottomLine, Color.Red);
            _spriteBatch.Draw(collisionTexture, rightLine, Color.Red);
            _spriteBatch.Draw(collisionTexture, leftLine, Color.Red);


        }
    }
}
