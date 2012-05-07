using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AIIG.Model;
using Microsoft.Xna.Framework.Graphics;

namespace AIIG.View
{
    public class MainView
    {
        private static MainView instance;


        private MainView()
        {
            instance = this;
        }

        public static MainView Instance
        {
            get
            {
                if (instance == null)
                {
                    new MainView();
                }
                return instance;
            }
        }

        public void Draw(GameTime gameTime)
        {
            Texture2D texture = new Texture2D(MainGame.Instance.GraphicsDevice, 200, 200);
            foreach (Edge edge in MainModel.Instance.Area.AllEdges)
            {
                edge.Draw(gameTime, 
            }
        }
    }
}
