using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AIIG4.Model;

namespace AIIG4.View
{
	public class MainView
	{

		//////////////////////////////
		//Properties//
		//////////////////////////////

		private static MainView instance;



		//////////////////////////////
		//Constructors//
		//////////////////////////////

		private MainView()
		{
			instance = this;
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



        //////////////////////////////
        //Methods//
        //////////////////////////////

        public void Draw(GameTime gameTime)
        {
            MainGame.Instance.SpriteBatch.Begin();

            MainModel.Instance.Cow.Draw(gameTime);
            MainModel.Instance.Hare.Draw(gameTime);

            MainGame.Instance.SpriteBatch.End();
        }
	}
}
