using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AIIG.Model
{
	public class Hare : Entity
	{
		//Constructors

		public Hare(Texture2D startTexture)
			: base(startTexture, State.Wandering)
		{
			GoToRandomEmptyNode();
		}

		public override void Update(GameTime gameTime)
		{
			if (Node == MainModel.Instance.Cow.Node)
			{
				GoToRandomEmptyNode();
			}
		}
	}
}
