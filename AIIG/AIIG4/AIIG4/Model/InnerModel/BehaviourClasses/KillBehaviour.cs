using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.Entities;
using Microsoft.Xna.Framework;

namespace AIIG4.Model.InnerModel.BehaviourClasses
{
	class KillBehaviour : Behaviour
	{

		//Fields

        private EntityManager.EntityType targetType;



        //Constructors

		public KillBehaviour(Entity host, EntityManager.EntityType targetType)
            : base(host)
        {
            this.targetType = targetType;
        }



        //Methods

        public override void Update(GameTime gameTime)
        {
            LinkedList<Entity> targets = MainModel.Instance.EntityManagement.GetEntitiesForType(this.targetType);

            foreach (Entity target in targets)
            {
                if (DetermineHit(target))
                {
                    target.RemoveFromGame();
                    this.Host.RemoveFromGame();
                    break;
                }
            }
        }

        private bool DetermineHit(Entity target)
        {
            Vector2 targetTopLeft = target.Position - target.Origin;
            return ((this.Host.Position.X > targetTopLeft.X)
                && (this.Host.Position.X < (targetTopLeft.X + target.Width))
                && (this.Host.Position.Y > targetTopLeft.Y)
                && (this.Host.Position.Y < (targetTopLeft.Y + target.Height)));
        }
	}
}
