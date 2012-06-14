using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.Entities;

namespace AIIG4.Model.InnerModel.BehaviourClasses.AutonomousBehaviourClasses
{
    public class Wandering : AutonomousBehaviour
    {

        //Fields

        private float maxForce;
        private Random randomGenerator;


        //Constructors

        public Wandering(AutonomousEntity host, float maxForce)
            : base(host)
        {
            this.maxForce = maxForce;
            this.randomGenerator = new Random();
        }



        //Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float randomSteerForce = ((float)this.randomGenerator.NextDouble() - 0.5f) * maxForce * 2;

            this.Host.ApplyForce(Host.Side * randomSteerForce);

            base.Update(gameTime);
        }
    }
}
