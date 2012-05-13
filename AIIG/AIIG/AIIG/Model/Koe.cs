using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace AIIG.Model
{
	class Cow : Entity
	{
		//Constructors

		public Cow(Texture2D startTexture)
			: base(startTexture)
		{
			Node = MainModel.Instance.Area.AllNodes.Last.Value;
		}
	}
}
