using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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



        //Methods

        public void Draw(GameTime gameTime)
        {
            foreach (Edge edge in this.AllEdges)
            {
                edge.Draw(gameTime);
            }
            foreach (Node node in this.AllNodes)
            {
                node.Draw(gameTime);
            }
        }

    }
}
