using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AIIG4.Model.InnerModel.Entities;
using AIIG4.Model.InnerModel.BehaviourClasses.GraphMovingBehaviourClasses;

namespace AIIG4.Model.InnerModel.BehaviourClasses.AutonomousBehaviourClasses
{
	class ShootingBehaviour : GraphMovingBehaviour
	{

		//Constants

		private const int CHECK_INTERVAL = 2000;
		private const int INITIAL_ELAPSED_TIME = 0;


		//Fields

		private int elapsedTimeSinceLastCheck;

		//Constructors

		public ShootingBehaviour(GraphMovingEntity host)
			: base(host)
		{
		}



		//Methods

		public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
		{
			ProceedCheckIntervalTime(gameTime);

			if (elapsedTimeSinceLastCheck >= CHECK_INTERVAL)
			{
				Shoot();

				elapsedTimeSinceLastCheck = INITIAL_ELAPSED_TIME;
			}
		}

		private void ProceedCheckIntervalTime(GameTime gameTime)
		{
			this.elapsedTimeSinceLastCheck += gameTime.ElapsedGameTime.Milliseconds;
		}

		private void Shoot()
		{

			Entity closestEntity = null;
			float shortestDistanceFound = 0;
			LinkedList<Entity> targets = MainModel.Instance.EntityManagement.GetEntitiesForType(EntityManager.EntityType.FlockMember);
			foreach(Entity target in targets)
			{
				float currentEntityDistance = (target.Position - this.Host.Position).LengthSquared();

				if (currentEntityDistance < shortestDistanceFound)
				{
					shortestDistanceFound = currentEntityDistance;
					closestEntity = target;
				}
			}
			//Vector2 heading = Host.Position - closestEntity.Position;
			//heading.Normalize();
			//this.Host.Heading = heading;
		}
	}
}
