using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using AIIG.Model;
using AIIG.View;
using AIIG.Controller;

namespace AIIG
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {

        private static MainGame instance;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;



        public MainGame()
        {
            instance = this;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        public static MainGame Instance
        {
            get
            {
                if (instance == null)
                {
                    new MainGame();
                }

                return instance;
            }
        }



        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MainController.Instance.Update(gameTime);
            MainModel.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            MainView.Instance.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
