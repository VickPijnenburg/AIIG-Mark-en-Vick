using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

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
	}
}
