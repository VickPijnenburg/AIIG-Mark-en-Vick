using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AIIG.Model
{
	class Hare : Entity
	{
		//Constructors

		public Hare(Texture2D startTexture)
			: base(startTexture)
		{
			randomNode();
		}

		public void Update(GameTime gameTime)
		{
			if (Node == MainModel.Instance.Cow.Node)
			{
				randomNode();
			}
		}
	}
}
