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

            CreateEntities();

            SetUpPillBehaviour();
            SetUpHareBehaviour();
            SetUpCowBehaviour();
        }

        private void CreateEntities()
        {
            this.entities = new List<Entity>();

            this.pill = new Pill(MainGame.Instance.Content.Load<Texture2D>("GameAssets/pill"));
            entities.Add(pill);

            this.hare = new Hare(MainGame.Instance.Content.Load<Texture2D>("GameAssets/rabbit-3"));
            entities.Add(hare);

            this.cow = new Cow(MainGame.Instance.Content.Load<Texture2D>("GameAssets/lemmling_Cartoon_cow"));
            entities.Add(cow);
        }

        private void SetUpPillBehaviour()
        {
            new IdleTillCow(this.Pill);
            new FleeToEmptyNode(this.Pill);

            this.Pill.CurrentState = Entity.State.Idle;
        }

        private void SetUpHareBehaviour()
        {
            new IdleTillCow(this.Hare);
            new FleeToEmptyNode(this.Hare);

            this.Hare.CurrentState = Entity.State.Idle;
        }

        private void SetUpCowBehaviour()
        {
            new Wandering(this.Cow);
            new AStarChase(Entity.State.LookingForPill, this.Cow, this.Pill, Entity.State.Chasing);
            new AStarChase(Entity.State.Chasing, this.Cow, this.Hare, Entity.State.Wandering);

            this.Cow.CurrentState = Entity.State.Wandering;
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
            Pill.Update(gameTime);
        }
    }
}
