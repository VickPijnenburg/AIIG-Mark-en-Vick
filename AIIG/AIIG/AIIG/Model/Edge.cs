using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AIIG.View;

namespace AIIG.Model
{
    public class Edge
    {
        //Fields

        private Node node1;
        private Node node2;



        //Constructors

        public Edge(Area area, Node node1, Node node2)
        {
            area.AllEdges.AddLast(this);
            this.node1 = node1;
            this.node2 = node2;

            node1.LinkToEdge(this);
            node2.LinkToEdge(this);
        }



        //Properties

        public Node Node1
        {
            get { return node1; }
        }

        public Node Node2
        {
            get { return node2; }
        }



        //Methods

        public void Draw(GameTime gameTime, Texture2D texture)
        {
            LineDrawingDevice.SetBHLine
                (
                texture,
                LineDrawingDevice.VectorToPoint(node1.Position),
                LineDrawingDevice.VectorToPoint(node2.Position),
                Color.Black
                );
        }
    }
}
