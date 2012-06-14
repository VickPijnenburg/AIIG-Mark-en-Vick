using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.GraphClasses;
using Microsoft.Xna.Framework;

namespace AIIG4.Model.InnerModel.Factories
{
    public abstract class GraphFactory
    {

        //Methods

        public static Graph CreateGraph()
        {
            Graph graph = new Graph();

            Node node0 = new Node(graph, new Vector2(500, 50));
            Node node1 = new Node(graph, new Vector2(50, 50));
            Node node2 = new Node(graph, new Vector2(300, 70));
            Node node3 = new Node(graph, new Vector2(400, 140));
            Node node4 = new Node(graph, new Vector2(90, 300));
            Node node5 = new Node(graph, new Vector2(450, 300));
            Node node6 = new Node(graph, new Vector2(600, 400));
            Node node7 = new Node(graph, new Vector2(150, 200));

            Edge edge0 = new Edge(graph, node0, node3, 1);
            Edge edge1 = new Edge(graph, node1, node2, 1);
            Edge edge2 = new Edge(graph, node4, node3, 3);
            Edge edge3 = new Edge(graph, node6, node5, 3);
            Edge edge4 = new Edge(graph, node3, node5, 2);
            Edge edge5 = new Edge(graph, node1, node4, 1);
            Edge edge6 = new Edge(graph, node2, node7, 1);
            Edge edge7 = new Edge(graph, node7, node1, 3);
            Edge edge8 = new Edge(graph, node2, node3, 2);
            Edge edge9 = new Edge(graph, node6, node3, 3);
            Edge edge10 = new Edge(graph, node6, node4, 1);
            Edge edge11 = new Edge(graph, node0, node6, 3);

            return graph;
        }
    }
}
