using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AIIG.Model.StateBehaviours;

namespace AIIG.Model
{
    class MainModel
    {

        //Fields

        private static MainModel instance;

        private EventManager eventManagement;

        private Area area;

		private List<Entity> entities;

		private Pill pill;
		private Hare hare;
		private Cow cow;



        //Constructors

        private MainModel()
        {
            instance = this;

            eventManagement = new EventManager();

			area = AreaFactory.CreateArea();

			entities = new List<Entity>();

			pill = new Pill(MainGame.Instance.Content.Load<Texture2D>("GameAssets/pill"));
			entities.Add(pill);

			hare = new Hare(MainGame.Instance.Content.Load<Texture2D>("GameAssets/rabbit-3"));
			entities.Add(hare);

			cow = new Cow(MainGame.Instance.Content.Load<Texture2D>("GameAssets/lemmling_Cartoon_cow"));
			entities.Add(cow);

            new Wandering(cow);
            new AStarChase(Entity.State.LookingForPill, cow, pill, Entity.State.Chasing);
            new AStarChase(Entity.State.Chasing, cow, hare, Entity.State.Wandering);
            
            
            cow.CurrentState = Entity.State.Wandering;
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

		public List<Entity> Entities
		{
			get { return entities; }
		}

		public Pill Pill
		{
			get { return pill; }
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
			Hare.Update(gameTime);
        }
    }
}
