using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AIIG.Model
{
	public class Pill : Entity
	{
		//Constructors

		public Pill(Texture2D startTexture)
			: base(startTexture, State.Idle)
		{
			GoToRandomEmptyNode();
		}
	}
}
