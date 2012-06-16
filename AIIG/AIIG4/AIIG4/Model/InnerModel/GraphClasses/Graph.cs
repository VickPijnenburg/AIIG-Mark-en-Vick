using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AIIG4.View;

namespace AIIG4.Model.InnerModel.GraphClasses
{
    public class Graph
    {

        //Fields

        private LinkedList<Node> allNodes;
        private LinkedList<Edge> allEdges;

        private Texture2D graphLinesTexture;
        private bool graphLinesUpToDate;



        //Constructors

        public Graph()
        {
            allNodes = new LinkedList<Node>();
            allEdges = new LinkedList<Edge>();

            this.graphLinesUpToDate = false;
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

        public void InsertEdge(Edge edge)
        {
            this.allEdges.AddLast(edge);
            this.graphLinesUpToDate = false;
        }

        public void Draw(GameTime gameTime)
        {
            if (!this.graphLinesUpToDate)
            {
                RebuildGraphLinesTexture();
            }

            DrawGraphLines();
            DrawEdgeCosts(gameTime);
            DrawNodes(gameTime);
        }

        private void DrawGraphLines()
        {
            MainView.Instance.SpriteBatch.Draw(this.graphLinesTexture, MainGame.Instance.GameAreaRect, Color.White);
        }

        private void DrawEdgeCosts(GameTime gameTime)
        {
            foreach (Edge edge in this.AllEdges)
            {
                edge.Draw(gameTime);
            }
        }

        private void DrawNodes(GameTime gameTime)
        {
            foreach (Node node in this.AllNodes)
            {
                node.Draw(gameTime);
            }
        }


        private void RebuildGraphLinesTexture()
        {
            this.graphLinesTexture = new Texture2D(
                MainGame.Instance.GraphicsDevice,
                MainGame.Instance.GameAreaRect.Width,
                MainGame.Instance.GameAreaRect.Height);

            foreach (Edge edge in this.AllEdges)
            {
                graphLinesTexture = edge.DrawLineOnGraphTexture(graphLinesTexture);
            }

            this.graphLinesUpToDate = true;
        }
    }
}
