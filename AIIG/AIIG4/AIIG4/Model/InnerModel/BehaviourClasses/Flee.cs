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
        private float activeDistance;


        //Constructors

		public Flee(float steeringForce, float force, float activeDistance)
		{
			this.steeringForce = steeringForce;
			this.force = force;
            this.activeDistance = activeDistance;
        }



        //Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float squaredActiveDistance = activeDistance * activeDistance;
            float squaredCowDistance = (Host.Position - MainModel.Instance.Cow.Position).LengthSquared();

            Console.WriteLine("AD: " + squaredActiveDistance + " CD: " + squaredCowDistance);

            if (squaredCowDistance  < squaredActiveDistance)
            {
                GoFlee();
            }
        }

        private void GoFlee()
        {
            Vector2 cowPositionHeading = (MainModel.Instance.Cow.Position - Host.Position);

            float dotProductSide = Vector2.Dot(Host.Side, cowPositionHeading);
            float dotProductHeading = Vector2.Dot(Host.Heading, cowPositionHeading);

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
