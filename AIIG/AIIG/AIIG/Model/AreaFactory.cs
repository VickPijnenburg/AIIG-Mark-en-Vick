﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AIIG.Model
{
    public abstract class AreaFactory
    {
        public static Area CreateArea()
        {
            Area area = new Area();

            Node node1 = new Node(area, new Vector2(10, 30));
            Node node2 = new Node(area, new Vector2(40, 20));
            Node node3 = new Node(area, new Vector2(40, 70));
            Node node4 = new Node(area, new Vector2(90, 80));
            Node node5 = new Node(area, new Vector2(70, 30));
            Node node6 = new Node(area, new Vector2(60, 40));
            Node node7 = new Node(area, new Vector2(10, 100));

            Edge edge1 = new Edge(area, node1, node2);
            Edge edge2 = new Edge(area, node4, node3);
            Edge edge3 = new Edge(area, node6, node7);
            Edge edge4 = new Edge(area, node3, node5);
            Edge edge5 = new Edge(area, node1, node4);
            Edge edge6 = new Edge(area, node2, node7);
            Edge edge7 = new Edge(area, node7, node1);
            Edge edge8 = new Edge(area, node2, node3);

            return area;
        }
    }
}