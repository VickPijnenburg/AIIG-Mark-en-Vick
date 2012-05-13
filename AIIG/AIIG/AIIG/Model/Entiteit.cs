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

        public Vector2 Origin
        {
            get { return new Vector2(Width / 2, Height / 2); }
        }

        public int Width
        {
            get { return texture.Width; }
        }

        public int Height
        {
            get { return texture.Height; }
        }


        //Methods

		public void randomNode()
		{

			Node newNode = null;
			bool foundOne = false;

			while (foundOne != true)
			{
				Random random = new Random();
				int randomNumber = random.Next(0, MainModel.Instance.Area.AllNodes.Count - 1);
				newNode = MainModel.Instance.Area.AllNodes.ElementAt(randomNumber);
				if (MainModel.Instance.Cow != null)
				{
					if (newNode != MainModel.Instance.Cow.node)
					{
						foundOne = true;
						Node = newNode;
					}
				}
				else
				{
					foundOne = true;
					Node = newNode;
				}
			}
		}

		public void Draw(GameTime gameTime)
		{
			MainView.Instance.SpriteBatch.Draw(Texture, (Node.Position - Origin), Color.White);
		}
	}
}
