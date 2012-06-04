using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIIG4.Model.InnerModel.BehaviourClasses
{
    public class Wandering : Behaviour
    {

        //Fields

        private float maxForce;
        private Random randomGenerator;


        //Constructors

        public Wandering(float maxForce)
        {
            this.maxForce = maxForce;
            this.randomGenerator = new Random();
        }



        //Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float randomSteerForce = ((float)this.randomGenerator.NextDouble() - 0.5f) * maxForce * 2;

            this.Host.ApplySteeringForce(Host.Side * randomSteerForce);

            base.Update(gameTime);
        }
    }
}
