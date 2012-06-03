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

namespace AIIG4
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class MainGame : Game
	{

        //////////////////////////////
        //Fields//
        //////////////////////////////

        private static MainGame instance;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;



        //////////////////////////////
        //Constructors//
        //////////////////////////////

		private MainGame()
		{
            instance = this;

			graphics = new GraphicsDeviceManager(this);
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



        //////////////////////////////
        //Methods//
        //////////////////////////////

		protected override void Initialize()
		{
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

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}
	}
}
