using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AIIG4.Model.InnerModel.Entities;
using AIIG4.Model.InnerModel.BehaviourClasses.GraphMovingBehaviourClasses;

namespace AIIG4.Model.InnerModel.BehaviourClasses.AutonomousBehaviourClasses
{
    class TargetingBehaviour : GraphMovingBehaviour
	{

		//Constants

		private const int CHECK_INTERVAL = 180;
		private const int INITIAL_ELAPSED_TIME = 0;
        


		//Fields

        private Entity currentTarget;
		private int elapsedTimeSinceLastCheck;



		//Constructors

		public TargetingBehaviour(GraphMovingEntity host)
			: base(host)
		{
            this.currentTarget = null;
            this.elapsedTimeSinceLastCheck = INITIAL_ELAPSED_TIME;
		}



		//Methods

		public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
		{
			ProceedCheckIntervalTime(gameTime);

			if (elapsedTimeSinceLastCheck >= CHECK_INTERVAL)
			{
				UpdateCurrentTarget();
				elapsedTimeSinceLastCheck = INITIAL_ELAPSED_TIME;
			}
            if (this.currentTarget != null)
            {
                UpdateHostHeading();
            }
		}

		private void ProceedCheckIntervalTime(GameTime gameTime)
		{
			this.elapsedTimeSinceLastCheck += gameTime.ElapsedGameTime.Milliseconds;
		}

        private void UpdateCurrentTarget()
		{
			LinkedList<Entity> targets = MainModel.Instance.EntityManagement.GetEntitiesForType(EntityManager.EntityType.FlockMember);

            float? shortestDistanceFound = null;

			foreach(Entity target in targets)
			{
				float currentEntityDistance = (target.Position - this.Host.Position).LengthSquared();

				if ((shortestDistanceFound == null)
                    || currentEntityDistance < shortestDistanceFound)
				{
					shortestDistanceFound = currentEntityDistance;
					this.currentTarget = target;
				}
			}
		}

        private void UpdateHostHeading()
        {
            Vector2 newHeading = this.currentTarget.Position - this.Host.Position;
            newHeading.Normalize();
            this.Host.Heading = newHeading;
        }
	}
}
