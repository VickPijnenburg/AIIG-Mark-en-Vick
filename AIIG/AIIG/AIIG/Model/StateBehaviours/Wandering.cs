using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AIIG.Model.StateBehaviours
{
	class Wandering : StateBehaviour
	{

        //Constants

        private const int INITIAL_COUNT = 4;

		//Fields

		private int count;


		//Constructors

		public Wandering(Entity host)
			: base(Entity.State.Wandering, host)
		{
			count = INITIAL_COUNT;
		}


		//Methods

		public override void Update(GameTime gameTime)
		{
			MoveToRandomAttachedNode();
			updateCount();
		}

        public override void Reset()
        {
            count = INITIAL_COUNT;
        }

		private void MoveToRandomAttachedNode()
		{
			int targetNodeIndex = DetermineTargetNode();
			Host.Node = Host.Node.AttachedNodes.ElementAt(targetNodeIndex);
		}

		private int DetermineTargetNode()
		{
			Random randomGenerator = new Random();
			return randomGenerator.Next(Host.Node.AttachedNodes.Count - 1);
		}

		private void updateCount()
		{
			if (count > 0 && MainModel.Instance.EventManagement.CowShouldMove)
			{
				count--;
			}

			if(count == 0)
			{

				MainModel.Instance.Cow.CurrentState = Entity.State.LookingForPill;
			}
		}
	}
}
