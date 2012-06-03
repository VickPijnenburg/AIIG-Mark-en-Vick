using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AIIG.Model.StateBehaviours;

namespace AIIG.Model
{
	public class Cow : Entity
	{

		//Constructors

		public Cow(Texture2D startTexture)
			: base(startTexture, State.Chasing)
		{
			randomNode();
		}



        //Methods

        public override void Update(GameTime gameTime)
        {
            if (MainModel.Instance.EventManagement.CowShouldMove)
            {
                base.Update(gameTime);
            }
        }
	}
}
