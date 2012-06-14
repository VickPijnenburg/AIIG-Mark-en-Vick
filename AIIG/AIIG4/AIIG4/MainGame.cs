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
using AIIG4.View;
using AIIG4.Model;

namespace AIIG4
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class MainGame : Game
	{

        //////////////////////////////
        //Constants//
        //////////////////////////////

        private static readonly Rectangle GAME_AREA_RECT = new Rectangle(0, 0, 1200, 700);



        //////////////////////////////
        //Fields//
        //////////////////////////////

        private static MainGame instance;

        GraphicsDeviceManager graphicsDeviceManager;



        //////////////////////////////
        //Constructors//
        //////////////////////////////

		private MainGame()
		{
            instance = this;

            this.graphicsDeviceManager = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}



        //////////////////////////////
        //Properties//
        //////////////////////////////

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

        public GraphicsDeviceManager GraphicsDeviceManager
        {
            get { return this.graphicsDeviceManager; }
        }

        public Rectangle GameAreaRect
        {
            get { return GAME_AREA_RECT; }
        }



        //////////////////////////////
        //Methods//
        //////////////////////////////

		protected override void Initialize()
		{

            this.GraphicsDeviceManager.PreferredBackBufferWidth = this.GameAreaRect.Width;
            this.GraphicsDeviceManager.PreferredBackBufferHeight = this.GameAreaRect.Height;
            this.GraphicsDeviceManager.ApplyChanges();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			

			// TODO: use this.Content to load your game content here
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            MainModel.Instance.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

            MainView.Instance.Draw(gameTime);

			base.Draw(gameTime);
		}
	}
}
