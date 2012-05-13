using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AIIG.Model
{
    class MainModel
    {

        //Fields

        private static MainModel instance;

        private EventManager eventManagement;

        private Area area;

		private Hare hare;
		private Cow cow;


        //Constructors

        private MainModel()
        {
            instance = this;

            eventManagement = new EventManager();

			area = AreaFactory.CreateArea();
			hare = new Hare(MainGame.Instance.Content.Load<Texture2D>("GameAssets/rabbit-3"));
			cow = new Cow(MainGame.Instance.Content.Load<Texture2D>("GameAssets/lemmling_Cartoon_cow"));
        }

        
        //Properties

        public static MainModel Instance
        {
            get
            {
                if (instance == null)
                {
                    new MainModel();
                }

                return instance;
            }
        }

        public EventManager EventManagement
        {
            get { return eventManagement; }
        }

        public Area Area
        {
            get { return area; }
        }

		public Hare Hare
		{
			get { return hare; }
		}

		public Cow Cow
		{
			get { return cow; }
		}


		//Methods

        public void Update(GameTime gameTime)
        {
            Cow.Update(gameTime);
            //Hare.Update(gameTime);
        }
    }
}
