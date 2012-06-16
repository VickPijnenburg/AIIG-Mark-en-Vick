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

        private EntityManager.EntityType entityTypeToFleeFrom;

		private float steeringForce;
		private float force;
        private float detectionDistance;



        //Constructors

        public Flee(AutonomousEntity host, EntityManager.EntityType entityTypeToFleeFrom, float steeringForce, float force, float detectionDistance)
            :base(host)
		{
            this.entityTypeToFleeFrom = entityTypeToFleeFrom;

			this.steeringForce = steeringForce;
			this.force = force;
            this.detectionDistance = detectionDistance;
        }



        //Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            FleeAsNeeded();
        }

        private void FleeAsNeeded()
        {
            Vector2 relativeThreatPosition = CalulateThreatHeading();

            if (relativeThreatPosition != Vector2.Zero)
            {
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

        private Vector2 CalulateThreatHeading()
        {
            LinkedList<Entity> possibleNeighbours = MainModel.Instance.EntityManagement.GetEntitiesForType(this.entityTypeToFleeFrom);

            Vector2 fleeHeading = Vector2.Zero;

            foreach (Entity possibleNeighbour in possibleNeighbours)
            {
                if(possibleNeighbour != this.Host)
                {
                    Vector2 threatRelativePosition = possibleNeighbour.Position - this.Host.Position;
                    float threatDistanceSquared = threatRelativePosition.LengthSquared();
                    if (threatDistanceSquared <= (this.detectionDistance * this.detectionDistance))
                    {
                        fleeHeading += (threatRelativePosition / threatDistanceSquared);
                    }
                }

                fleeHeading.Normalize();
            }

            return fleeHeading;
        }
	}
}
