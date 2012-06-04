using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AIIG4.Model.InnerModel.BehaviourClasses
{
    public class ChaseSteering : Behaviour
    {

        //Fields

        private float force;



        //Constructors

        public ChaseSteering(float force)
        {
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
