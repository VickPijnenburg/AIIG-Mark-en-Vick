using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIIG.Model.StateBehaviours
{
    public class AStarNodeCapsule
    {

        //Fields

        private Node node;
        private Node endNode;

        private int? estimatedDistanceToEnd;
        private int? shortestDistance;

        private AStarNodeCapsule previousRouteNode;



        //Constructors

        public AStarNodeCapsule(Node node, Node endNode)
        {
            this.node = node;
            this.endNode = endNode;
        }

        

        //Properties

        public Node Node
        {
            get { return node; }
        }

        public int EstimatedDistanceToEnd
        {
            get
            {
                if (this.estimatedDistanceToEnd == null)
                {
                    double xSquared = Math.Pow((this.Node.Position.X - this.endNode.Position.X), 2);
                    double ySquared = Math.Pow((this.Node.Position.Y - this.endNode.Position.Y), 2);
                    this.estimatedDistanceToEnd = (int) Math.Sqrt(xSquared + ySquared) / 156;
                    Console.WriteLine("Estimated distance to end for node " + this.Node.ID + " is " + this.estimatedDistanceToEnd);
                }

                return (int) estimatedDistanceToEnd;
            }
        }

        public int? ShortestDistance
        {
            get { return shortestDistance; }
            set { shortestDistance = value; }
        }

        public int Priority
        {
            get
            {
                int nonNullShortestDistance = (this.ShortestDistance == null) ? 0 : (int)this.ShortestDistance;

                return nonNullShortestDistance + this.EstimatedDistanceToEnd;
            }
        }

        public AStarNodeCapsule PreviousRouteNode
        {
            get { return previousRouteNode; }
            set { previousRouteNode = value; }
        }

    }
}
