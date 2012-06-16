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

            Node node0 = new Node(graph, new Vector2(400, 100));
            Node node1 = new Node(graph, new Vector2(800, 100));
            Node node2 = new Node(graph, new Vector2(150, 300));
            Node node3 = new Node(graph, new Vector2(510, 280));
            Node node4 = new Node(graph, new Vector2(690, 280));
            Node node5 = new Node(graph, new Vector2(1050, 300));
            Node node6 = new Node(graph, new Vector2(150, 450));
            Node node7 = new Node(graph, new Vector2(510, 440));
            Node node8 = new Node(graph, new Vector2(690, 440));
            Node node9 = new Node(graph, new Vector2(1050, 450));
            Node node10 = new Node(graph, new Vector2(400, 650));
            Node node11 = new Node(graph, new Vector2(800, 650));

            new Edge(graph, node0, node2, 2);
            new Edge(graph, node0, node3, 1);
            new Edge(graph, node1, node4, 1);
            new Edge(graph, node1, node5, 2);
            new Edge(graph, node2, node3, 1);
            new Edge(graph, node2, node6, 3);
            new Edge(graph, node3, node4, 2);
            new Edge(graph, node3, node7, 2);
            new Edge(graph, node4, node5, 1);
            new Edge(graph, node4, node8, 2);
            new Edge(graph, node5, node9, 3);
            new Edge(graph, node6, node7, 1);
            new Edge(graph, node6, node10, 2);
            new Edge(graph, node7, node8, 2);
            new Edge(graph, node7, node10, 1);
            new Edge(graph, node8, node9, 1);
            new Edge(graph, node8, node11, 1);
            new Edge(graph, node9, node11, 2);

            return graph;
        }
    }
}
