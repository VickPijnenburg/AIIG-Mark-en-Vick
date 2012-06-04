using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AIIG4.Model.InnerModel.BehaviourClasses
{
	class Flee : Behaviour
	{
		//Fields
		private float steeringForce;
		private float force;


        //Constructors

		public Flee(float steeringForce, float force)
		{
			this.steeringForce = steeringForce;
			this.force = force;
        }



        //Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
			Vector2 harePositionHeading = (MainModel.Instance.Hare.Position - Host.Position);

			float dotProductSide = Vector2.Dot(Host.Side, harePositionHeading);
			float dotProductHeading = Vector2.Dot(Host.Heading, harePositionHeading);

			if (dotProductSide < 0)
			{
				if (dotProductHeading < 0 || dotProductSide < 40)
				{
					Host.ApplySteeringForce(Host.Side * steeringForce);
				}
			}
			else
			{
				if (dotProductHeading < 0 || dotProductSide > 40)
				{
					Host.ApplySteeringForce(Host.Side * -steeringForce);
				}
			}

			Host.ApplySteeringForce(Host.Heading * this.force);
        }
	}
}
