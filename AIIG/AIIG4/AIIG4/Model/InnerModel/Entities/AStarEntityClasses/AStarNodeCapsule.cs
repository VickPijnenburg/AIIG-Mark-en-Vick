using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.GraphClasses;

namespace AIIG4.Model.InnerModel.Entities.AStarEntityClasses
{
    public class AStarNodeCapsule
    {

        //Fields

        private Node node;
        private Node endNode;

        private int? estimatedDistanceToEnd;
        private int? shortestDistance;

        private AStarNodeCapsule previousRouteNode;
        private Edge previousRouteEdge;



        //Constructors

        public AStarNodeCapsule(Node node, Node endNode)
        {
            this.node = node;
            this.endNode = endNode;
        }



        //Properties

        public Node Node
        {
            get { return this.node; }
        }

        public int EstimatedDistanceToEnd
        {
            get
            {
                if (this.estimatedDistanceToEnd == null)
                {
                    double xSquared = Math.Pow((this.Node.Position.X - this.endNode.Position.X), 2);
                    double ySquared = Math.Pow((this.Node.Position.Y - this.endNode.Position.Y), 2);
                    this.estimatedDistanceToEnd = (int)Math.Sqrt(xSquared + ySquared) / 156;
                }

                return (int)estimatedDistanceToEnd;
            }
        }

        public int? ShortestDistance
        {
            get { return this.shortestDistance; }
            set { this.shortestDistance = value; }
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
            get { return this.previousRouteNode; }
            set { this.previousRouteNode = value; }
        }

        public Edge PreviousRouteEdge
        {
            get { return this.previousRouteEdge; }
            set { this.previousRouteEdge = value; }
        }

    }
}
