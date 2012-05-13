using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AIIG.Model
{
	class Cow : Entity
	{
		//Constructors

		public Cow(Texture2D startTexture)
			: base(startTexture)
		{
			randomNode();
		}

        public void Update(GameTime gameTime)
        {
            if (MainModel.Instance.EventManagement.CowShouldMove
                && Node.AttachedNodes.Count > 0)
            {
                MoveToRandomAttachedNode();
            }
        }

        private void MoveToRandomAttachedNode()
        {
            int targetNodeIndex = DetermineTargetNode();
            Node = Node.AttachedNodes.ElementAt(targetNodeIndex);
        }

        private int DetermineTargetNode()
        {
            Random randomGenerator = new Random();
            return randomGenerator.Next(Node.AttachedNodes.Count - 1);
        }
	}
}
