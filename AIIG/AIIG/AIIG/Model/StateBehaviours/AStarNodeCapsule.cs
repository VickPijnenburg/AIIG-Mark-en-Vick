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
        private int? shortestDistance;
        private AStarNodeCapsule previousRouteNode;



        //Constructors

        public AStarNodeCapsule(Node node)
        {
            this.node = node;
        }

        

        //Properties

        public Node Node
        {
            get { return node; }
        }

        public int? ShortestDistance
        {
            get { return shortestDistance; }
            set { shortestDistance = value; }
        }

        public AStarNodeCapsule PreviousRouteNode
        {
            get { return previousRouteNode; }
            set { previousRouteNode = value; }
        }
    }
}
