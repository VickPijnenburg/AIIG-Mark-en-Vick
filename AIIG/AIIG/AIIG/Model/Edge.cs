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

        private int cost;
        private Texture2D lineTexture;
        private Texture2D costTexture;


        //Constructors

        public Edge(Area area, Node node1, Node node2, int cost)
        {
            area.AllEdges.AddLast(this);
            this.node1 = node1;
            this.node2 = node2;

            node1.LinkToEdge(this);
            node2.LinkToEdge(this);

            this.lineTexture = CreateLineTexture();

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

        public void Draw(GameTime gameTime)
        {
            DrawLine();
            DrawCost();
        }

        private void DrawLine()
        {
            MainView.Instance.SpriteBatch.Draw(lineTexture, Vector2.Zero, Color.White);
        }

        private void DrawCost()
        {
            Texture2D castCostTexture = (Texture2D)costTexture;
            Vector2 costPosition = ((Node1.Position + Node2.Position) / 2);
            costPosition -= new Vector2(castCostTexture.Width / 2, castCostTexture.Height / 2);
            MainView.Instance.SpriteBatch.Draw((Texture2D)costTexture, costPosition, Color.White);
        }

        private Texture2D CreateLineTexture()
        {
            Rectangle viewRect = MainView.Instance.ViewRect;
            Texture2D lineTexture = new Texture2D(MainGame.Instance.GraphicsDevice, viewRect.Width, viewRect.Height);

            LineDrawingDevice.SetBHLine
                (
                lineTexture,
                LineDrawingDevice.VectorToPoint(node1.Position),
                LineDrawingDevice.VectorToPoint(node2.Position),
                Color.Black
                );

            return lineTexture;
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
