using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.Entities;

namespace AIIG4.Model.InnerModel.BehaviourClasses.AutonomousBehaviourClasses
{
    public class ConstantPropulsion : AutonomousBehaviour
    {

        //Fields

        private float force;



        //Constructors
        public ConstantPropulsion(AutonomousEntity host, float force)
            : base(host)
        {
            this.force = force;
        }



        //Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Host.ApplyForce(Host.Heading * this.force);
        }
    }
}
