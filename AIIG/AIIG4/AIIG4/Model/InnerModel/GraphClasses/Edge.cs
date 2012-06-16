using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AIIG4.View;

namespace AIIG4.Model.InnerModel.GraphClasses
{
    public class Edge
    {
        //Fields

        private Node node1;
        private Node node2;

        private int cost;
        private Texture2D costTexture;


        //Constructors

        public Edge(Graph graph, Node node1, Node node2, int cost)
        {
            graph.AllEdges.AddLast(this);
            this.node1 = node1;
            this.node2 = node2;

            node1.LinkToEdge(this);
            node2.LinkToEdge(this);

            this.cost = cost;
            costTexture = DetermineCostTexture();
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

        public int Cost
        {
            get { return cost; }
        }



        //Methods

        public Texture2D DrawLineOnGraphTexture(Texture2D texture)
        {
            LineDrawingDevice.SetBHLine
                (
                texture,
                LineDrawingDevice.VectorToPoint(node1.Position),
                LineDrawingDevice.VectorToPoint(node2.Position),
                Color.Black
                );

            return texture;
        }

        public void Draw(GameTime gameTime)
        {
            DrawCost();
        }

        private void DrawCost()
        {
            Vector2 costPosition = ((Node1.Position + Node2.Position) / 2);
            costPosition -= new Vector2(this.costTexture.Width / 2, this.costTexture.Height / 2);
            MainView.Instance.SpriteBatch.Draw(this.costTexture, costPosition, Color.White);
        }

        private Texture2D DetermineCostTexture()
        {
            if (cost == 1)
            {
                return MainGame.Instance.Content.Load<Texture2D>("GameAssets/cost1");
            }
            else if (cost == 2)
            {
                return MainGame.Instance.Content.Load<Texture2D>("GameAssets/cost2");
            }
            else if (cost == 3)
            {
                return MainGame.Instance.Content.Load<Texture2D>("GameAssets/cost3");
            }
            else
            {
                return MainGame.Instance.Content.Load<Texture2D>("GameAssets/costUnknown");
            }
        }
    }
}
