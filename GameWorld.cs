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

        new public static GameServiceContainer Services;

        private static Scene currentScene;

        public static World currentWorld;
        private World world1;

        Scene menu;
        Scene ui;
        List<Scene> loadedScenes;
        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Services = base.Services;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            world1 = new World();
            currentWorld = world1;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            menu = new Menu();
            loadedScenes = new List<Scene>();
            loadedScenes.Add(menu);


            foreach (GameObject o in currentWorld.Objects)
            {
                o.LoadContent(Content);
            }


        }

        public void StartGame()
        {
            loadedScenes.Clear();
            // ui = new Menu();
            // menu = ui;
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
            }



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
