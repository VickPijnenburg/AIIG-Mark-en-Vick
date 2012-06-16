using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.Entities;
using Microsoft.Xna.Framework;

namespace AIIG4.Model.InnerModel.BehaviourClasses.AutonomousBehaviourClasses
{
    public class ChaseOrFleeBehaviour : AutonomousBehaviour
    {

		//Fields

        private EntityManager.EntityType targetOrThreat;

		private float steeringForce;
        private float detectionDistance;



        //Constructors

        public ChaseOrFleeBehaviour(AutonomousEntity host, EntityManager.EntityType targetOrThreat, float steeringForce, float detectionDistance)
            :base(host)
		{
            this.targetOrThreat = targetOrThreat;

			this.steeringForce = steeringForce;
            this.detectionDistance = detectionDistance;
        }



        //Properties

        public float SteeringForce
        {
            get { return this.steeringForce; }
            set { this.steeringForce = value; }
        }



        //Methods

        protected Vector2 CalulateTargetOrThreatHeading()
        {
            LinkedList<Entity> possibleNeighbours = MainModel.Instance.EntityManagement.GetEntitiesForType(this.targetOrThreat);

            Vector2 targetOrThreatHeading = Vector2.Zero;

            foreach (Entity targetOrThreat in possibleNeighbours)
            {
                if (targetOrThreat != this.Host)
                {
                    Vector2 relativePosition = targetOrThreat.Position - this.Host.Position;
                    float distanceSquared = relativePosition.LengthSquared();
                    if (distanceSquared <= (this.detectionDistance * this.detectionDistance))
                    {
                        targetOrThreatHeading += (relativePosition / distanceSquared);
                    }
                }

                targetOrThreatHeading.Normalize();
            }

            return targetOrThreatHeading;
        }
    }
}
