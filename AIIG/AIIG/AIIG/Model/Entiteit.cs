using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AIIG.View;

namespace AIIG.Model
{
	class Entiteit
	{

        //Fields

		private Node node;
        private Texture2D texture;



        //Constructors

        public Entiteit(Texture2D startTexture)
        {
            texture = startTexture;
        }



        //Properties

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }



        //Methods

        public void Draw(GameTime gameTime)
        {
            MainView.Instance.SpriteBatch.Draw(texture, node.Position, Color.White);
        }
	}
}
