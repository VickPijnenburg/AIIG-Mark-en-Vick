using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AIIG4.Model;
using Microsoft.Xna.Framework.Graphics;

namespace AIIG4.View
{
	public class MainView
	{

        //////////////////////////////
        //Constants//
        //////////////////////////////

        private static readonly Rectangle VIEW_RECT = new Rectangle(0, 0, 800, 600);
        


		//////////////////////////////
		//Properties//
		//////////////////////////////

		private static MainView instance;

        private static SpriteFont font;

        private SpriteBatch spriteBatch;

        

		//////////////////////////////
		//Constructors//
		//////////////////////////////

		private MainView()
		{
			instance = this;

            font = MainGame.Instance.Content.Load<SpriteFont>("GameAssets/gameFont");

            this.spriteBatch = new SpriteBatch(MainGame.Instance.GraphicsDevice);
		}



		//////////////////////////////
		//Properties//
		//////////////////////////////

		public static MainView Instance
		{
			get
			{
				if (instance == null)
				{
					new MainView();
				}
				return instance;
			}
		}

        public SpriteBatch SpriteBatch
        {
            get { return this.spriteBatch; }
        }

        public Rectangle ViewRect
        {
            get { return VIEW_RECT; }
        }

        public static SpriteFont Font
        {
            get { return font; }
        }



        //////////////////////////////
        //Methods//
        //////////////////////////////

        public void Draw(GameTime gameTime)
        {
            this.SpriteBatch.Begin();

            MainModel.Instance.Cow.Draw(gameTime);
            MainModel.Instance.Hare.Draw(gameTime);

            this.SpriteBatch.End();
        }
	}
}
