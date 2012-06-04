using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIIG4.Model.InnerModel.BehaviourClasses
{
    public class ConstantPropulsion : Behaviour
    {

        //Fields

        private float force;



        //Constructors
        public ConstantPropulsion(float force)
        {
            this.force = force;
        }



        //Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Host.ApplySteeringForce(Host.Heading * this.force);
        }
    }
}
