using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AIIG4.Model.InnerModel.Entities;

namespace AIIG4.Model.InnerModel.BehaviourClasses
{
    public class ChaseSteering : Behaviour
    {

        //Fields

        private float force;
        private Entity target;


        //Constructors

        public ChaseSteering(float force)
        {
            this.force = force;
        }



        //Properties

        public Entity Target
        {
            get { return this.target; }
            set { this.target = value; }
        }



        //Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (this.Target != null)
            {
                Vector2 harePositionHeading = (this.Target.Position - Host.Position);

                float dotProductSide = Vector2.Dot(Host.Side, harePositionHeading);
                float dotProductHeading = Vector2.Dot(Host.Heading, harePositionHeading);

                if (dotProductSide < 0)
                {
                    if (dotProductHeading < 0 || dotProductSide < 40)
                    {
                        Host.ApplySteeringForce(Host.Side * -force);
                    }
                }
                else
                {
                    if (dotProductHeading < 0 || dotProductSide > 40)
                    {
                        Host.ApplySteeringForce(Host.Side * force);
                    }
                }
            }
        }
    }
}
