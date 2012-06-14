using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIIG4.Model.InnerModel.GraphClasses
{
    public class Graph
    {

        //Fields

        private LinkedList<Node> allNodes;
        private LinkedList<Edge> allEdges;



        //Constructors

        public Graph()
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
