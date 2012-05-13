using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AIIG.View;

namespace AIIG.Model
{
	class Entity
	{

        //Fields

		private Node node;
        private Texture2D texture;



        //Constructors

        public Entity(Texture2D startTexture)
        {
            texture = startTexture;
        }



		//Properties

		public Texture2D Texture
		{
			get { return texture; }
			set { texture = value; }
		}

		public Node Node
		{
			get { return node; }
			set { node = value; }
		}


        //Methods

		public void Draw(GameTime gameTime)
		{
			MainView.Instance.SpriteBatch.Draw(Texture, Node.Position, Color.White);
		}
	}
}
