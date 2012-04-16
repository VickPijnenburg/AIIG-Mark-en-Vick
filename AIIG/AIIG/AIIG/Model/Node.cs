using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AIIG.Model
{
    class Node
    {
        //Fields

        private Vector2 position;
        private LinkedList<Edge> edges;



        //Constructors

        public Node(Vector2 position)
        {
            this.position = position;
            edges = new LinkedList<Edge>();
        }



        //Properties

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }



        //Methods

        public void LinkToEdge(Edge edge)
        {
            edges.AddLast(edge);
        }
    }
}
