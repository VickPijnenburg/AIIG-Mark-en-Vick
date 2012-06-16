using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AIIG4.Model.InnerModel.Entities;

namespace AIIG4.Model.InnerModel.BehaviourClasses.AutonomousBehaviourClasses
{
	class Flee : ChaseOrFleeBehaviour
	{

        //Constructors

        public Flee(AutonomousEntity host, EntityManager.EntityType threat, float steeringForce, float detectionDistance)
            : base(host, threat, steeringForce, detectionDistance)
		{ }



        //Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            FleeAsNeeded();
        }

        private void FleeAsNeeded()
        {
            Vector2 relativeThreatPosition = CalulateTargetOrThreatHeading();

            if (relativeThreatPosition != Vector2.Zero)
            {
                float dotProductSide = Vector2.Dot(Host.Side, relativeThreatPosition);
                float dotProductHeading = Vector2.Dot(Host.Heading, relativeThreatPosition);

                if (dotProductSide > 0)
                {
                    if (dotProductHeading > 0 || dotProductSide > 40)
                    {
                        Host.ApplyForce(Host.Side * -this.SteeringForce);
                    }
                }
                else
                {
                    if (dotProductHeading > 0 || dotProductSide < 40)
                    {
                        Host.ApplyForce(Host.Side * this.SteeringForce);
                    }
                }
            }
        }

	}
}
