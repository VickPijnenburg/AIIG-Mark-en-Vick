using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace AIIG4.Model
{
	class Entity
	{

        //////////////////////////////
        //Fields//
        //////////////////////////////

        private Texture2D texture;


		//////////////////////////////
		//Constructors//
		//////////////////////////////

		public Entity(Texture2D startTexture)
		{
            this.texture = startTexture;
		}
	}
}
