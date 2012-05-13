﻿using System;
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
			Node = MainModel.Instance.Area.AllNodes.Last.Value;
		}

        public void Update(GameTime gameTime)
        {
            if (Node.AttachedNodes.Count > 0)
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
