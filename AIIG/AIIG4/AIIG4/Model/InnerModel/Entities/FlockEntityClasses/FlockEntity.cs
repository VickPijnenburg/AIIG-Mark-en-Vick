using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace AIIG4.Model.InnerModel.Entities.FlockEntityClasses
{
    public class FlockEntity : AutonomousEntity
    {

        //Fields

        private Flock flock;



        //Constructors

        public FlockEntity(EntityManager.EntityType entityType, Texture2D startTexture, bool wrapsAround, Flock flock)
            : base(entityType, startTexture, wrapsAround)
        {
            this.flock = flock;
            this.flock.InsertFlockEntity(this);
        }



        //Properties

        public Flock Flock
        {
            get { return this.flock; }
        }


        //Methods

        public override void RemoveFromGame()
        {
            base.RemoveFromGame();

            this.flock.RemoveFlockEntity(this);
        }
    }
}
