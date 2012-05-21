using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AIIG.View;
using Microsoft.Xna.Framework.Graphics;

namespace AIIG.Model
{
    public class Node
    {
        //Fields

        private Vector2 position;
        private LinkedList<Edge> edges;
        private Texture2D texture;



        //Constructors

        public Node(Area area, Vector2 position)
        {
            area.AllNodes.AddLast(this);
            this.position = position;
            edges = new LinkedList<Edge>();

            texture = MainGame.Instance.Content.Load<Texture2D>("GameAssets/node");
        }



        //Properties

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Origin
        {
            get { return new Vector2(Width / 2, Height / 2); }
        }

        public int Width
        {
            get { return texture.Width; }
        }

        public int Height
        {
            get { return texture.Height; }
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

        public LinkedList<Edge> Edges
        {
            get { return new LinkedList<Edge>(edges); }
        }



        //Methods

        public void LinkToEdge(Edge edge)
        {
            edges.AddLast(edge);
        }

        public void Draw(GameTime gameTime)
        {
            MainView.Instance.SpriteBatch.Draw(texture, (Position - Origin), Color.White);
        }


    }
}
