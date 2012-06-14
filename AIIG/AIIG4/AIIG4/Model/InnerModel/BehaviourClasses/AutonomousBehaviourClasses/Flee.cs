using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AIIG4.Model.InnerModel.Entities;

namespace AIIG4.Model.InnerModel.BehaviourClasses.AutonomousBehaviourClasses
{
	class Flee : AutonomousBehaviour
	{

		//Fields

        private Entity entityToFleeFrom;

		private float steeringForce;
		private float force;
        private float activeDistance;



        //Constructors

        public Flee(AutonomousEntity host, Entity entityToFleeFrom, float steeringForce, float force, float activeDistance)
            :base(host)
		{
            this.entityToFleeFrom = entityToFleeFrom;

			this.steeringForce = steeringForce;
			this.force = force;
            this.activeDistance = activeDistance;
        }



        //Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float squaredActiveDistance = activeDistance * activeDistance;
            float squaredCowDistance = (Host.Position - this.entityToFleeFrom.Position).LengthSquared();

            Console.WriteLine("AD: " + squaredActiveDistance + " CD: " + squaredCowDistance);

            if (squaredCowDistance  < squaredActiveDistance)
            {
                GoFlee();
            }
        }

        private void GoFlee()
        {
            Vector2 relativeThreatPosition = (this.entityToFleeFrom.Position - Host.Position);

            float dotProductSide = Vector2.Dot(Host.Side, relativeThreatPosition);
            float dotProductHeading = Vector2.Dot(Host.Heading, relativeThreatPosition);

            if (dotProductSide < 0)
            {
                if (dotProductHeading < 0 || dotProductSide < 40)
                {
                    Host.ApplyForce(Host.Side * steeringForce);
                }
            }
            else
            {
                if (dotProductHeading < 0 || dotProductSide > 40)
                {
                    Host.ApplyForce(Host.Side * -steeringForce);
                }
            }

            Host.ApplyForce(Host.Heading * this.force);
        }
	}
}
