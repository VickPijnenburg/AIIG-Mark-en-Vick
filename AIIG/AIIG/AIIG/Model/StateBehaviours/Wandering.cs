using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AIIG.Model.StateBehaviours
{
	class Wandering : StateBehaviour
	{
		public Wandering(Entity host)
			: base(Entity.State.Wandering, host)
		{}

		public override void Update(GameTime gameTime)
		{
			MoveToRandomAttachedNode();
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
	}
}
