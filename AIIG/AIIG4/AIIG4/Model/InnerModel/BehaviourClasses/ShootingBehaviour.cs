using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.Entities;
using Microsoft.Xna.Framework;

namespace AIIG4.Model.InnerModel.BehaviourClasses
{
    public class ShootingBehaviour : Behaviour
    {

        //Constants

        private const int SHOT_INTERVAL = 3000;


        //Fields

        private int elapsedTimeSinceLastShot;



        //Constructors

        public ShootingBehaviour(Entity host)
            : base(host)
        {
            
        }



        //Methods

        public override void Update(GameTime gameTime)
        {
            ProceedTime(gameTime);
            ShootAsNeeded();

            base.Update(gameTime);
        }

        private void ProceedTime(GameTime gameTime)
        {
            elapsedTimeSinceLastShot += gameTime.ElapsedGameTime.Milliseconds;
        }

        private void ShootAsNeeded()
        {
            if (elapsedTimeSinceLastShot >= SHOT_INTERVAL)
            {
                Shoot();
            }
        }

        private void Shoot()
        {

        }


    }
}
