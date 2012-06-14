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

        //Constants

        private static readonly Rectangle VIEW_RECT = new Rectangle(0, 0, 800, 600);
        private static SpriteFont font;



        //Fields

        private static MainView instance;

        private SpriteBatch spriteBatch;



        //Constructors

        private MainView()
        {
            instance = this;

            spriteBatch = new SpriteBatch(MainGame.Instance.GraphicsDevice);
            font = MainGame.Instance.Content.Load<SpriteFont>("GameAssets/gameFont");
        }



        //Properties

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

        public static SpriteFont Font
        {
            get { return font; }
        }


        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public Rectangle ViewRect
        {
            get { return VIEW_RECT; }
        }


        //Methods

        public void Draw(GameTime gameTime)
        {
            MainGame.Instance.GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();

            DrawEdges(gameTime);
            DrawNodes(gameTime);
            DrawEntities(gameTime);

            SpriteBatch.End();
        }

        private void DrawEdges(GameTime gameTime)
        {
            foreach (Edge edge in MainModel.Instance.Area.AllEdges)
            {
                edge.Draw(gameTime);
            }
        }

        private void DrawNodes(GameTime gameTime)
        {
            foreach (Node node in MainModel.Instance.Area.AllNodes)
            {
                node.Draw(gameTime);
            }
        }

        private void DrawEntities(GameTime gameTime)
		{
			foreach(Entity entity in MainModel.Instance.Entities)
			{
				entity.Draw(gameTime);
			}
        }
    }
}
