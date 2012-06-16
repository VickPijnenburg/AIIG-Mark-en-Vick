using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AIIG4.Model.InnerModel.Entities;

namespace AIIG4.Model.InnerModel.BehaviourClasses.AutonomousBehaviourClasses
{
    public class ChaseSteering : ChaseOrFleeBehaviour
    {

        //Constructors

        public ChaseSteering(AutonomousEntity host, EntityManager.EntityType targetType, float steeringForce, float detectionDistance)
            : base(host, targetType, steeringForce, detectionDistance)
        { }



        //Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            ChaseAsNeeded();
        }

        private void ChaseAsNeeded()
        {
            Vector2 relativeTargetPosition = CalulateTargetOrThreatHeading();

            if (relativeTargetPosition != Vector2.Zero)
            {
                float dotProductSide = Vector2.Dot(Host.Side, relativeTargetPosition);
                float dotProductHeading = Vector2.Dot(Host.Heading, relativeTargetPosition);

                if (dotProductSide < 0)
                {
                    if (dotProductHeading < 0 || dotProductSide < 40)
                    {
                        Host.ApplyForce(Host.Side * -this.SteeringForce);
                    }
                }
                else
                {
                    if (dotProductHeading < 0 || dotProductSide > 40)
                    {
                        Host.ApplyForce(Host.Side * this.SteeringForce);
                    }
                }
            }
        }
    }
}
