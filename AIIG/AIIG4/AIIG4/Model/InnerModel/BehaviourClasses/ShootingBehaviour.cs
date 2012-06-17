using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.Entities;
using Microsoft.Xna.Framework;
using AIIG4.Model.InnerModel.Factories;

namespace AIIG4.Model.InnerModel.BehaviourClasses
{
    public class ShootingBehaviour : Behaviour
    {

        //Constants

        private const int SHOT_INTERVAL = 1000;
        private const int INITIAL_TIME_ELAPSED = 0;



        //Fields

        private EntityManager.EntityType targetType;

        private int elapsedTimeSinceLastShot;
        


        //Constructors

        public ShootingBehaviour(Entity host, EntityManager.EntityType targetType)
            : base(host)
        {
            this.targetType = targetType;

            this.elapsedTimeSinceLastShot = INITIAL_TIME_ELAPSED;
        }



        //Properties

        public bool TargetsRemain
        {
            get { return (MainModel.Instance.EntityManagement.GetEntitiesForType(this.targetType).Count > 0); }
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
                this.elapsedTimeSinceLastShot %= SHOT_INTERVAL;
            }
        }

        private void Shoot()
        {
            if (this.TargetsRemain)
            {
                EntityFactory.CreateProjectile(this.targetType, this.Host.Position, this.Host.Heading);
            }
        }


    }
}
