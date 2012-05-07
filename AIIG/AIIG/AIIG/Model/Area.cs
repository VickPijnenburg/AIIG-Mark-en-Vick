using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIIG.Model
{
    public class Area
    {

        //Fields

        private LinkedList<Node> allNodes;
        private LinkedList<Edge> allEdges;



        //Constructors

        public Area()
        {
            allNodes = new LinkedList<Node>();
            allEdges = new LinkedList<Edge>();
        }



        //Properties

        public LinkedList<Node> AllNodes
        {
            get { return allNodes; }
        }

        public LinkedList<Edge> AllEdges
        {
            get { return allEdges; }
        }

    }
}
