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

        public LinkedList<Node> AttachedNodes
        {
            get
            {
                LinkedList<Node> result = new LinkedList<Node>();
                foreach (Edge edge in edges)
                {
                    if (edge.Node1 == this)
                    {
                        result.AddLast(edge.Node2);
                    }
                    else
                    {
                        result.AddLast(edge.Node1);
                    }
                }
                return result;
            }
        }


        //Methods

        public void LinkToEdge(Edge edge)
        {
            edges.AddLast(edge);
        }


    }
}
