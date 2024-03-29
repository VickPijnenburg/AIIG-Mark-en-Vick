﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AIIG.View;

namespace AIIG.Model
{
	public class Entity
	{

        //Enums

        public enum State
        {
            Idle,
            Wandering,
            LookingForPill,
            Fleeing,
            Chasing,
            Evading,
            CollisionAvoidance
        }



        //Fields

        private SortedDictionary<State, StateBehaviour> behaviour;
        private State currentState;

        private Texture2D texture;

		private Node node;
		Random random = new Random();
        


        //Constructors

        public Entity(Texture2D startTexture, State startState)
        {
            this.behaviour = new SortedDictionary<State, StateBehaviour>();
            this.currentState = startState;

            this.texture = startTexture;
        }



		//Properties

        public State CurrentState
        {
            get { return currentState; }
            set
            {
                if (this.behaviour.ContainsKey(this.CurrentState))
                {
                    behaviour[CurrentState].Reset();
                }
                currentState = value;
            }
        }

		public Texture2D Texture
		{
			get { return texture; }
			set { texture = value; }
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

        public Node Node
        {
            get { return node; }
            set { node = value; }
        }



        //Methods

        public virtual void Update(GameTime gameTime)
        {
            this.behaviour[CurrentState].Update(gameTime);
        }

		public void GoToRandomEmptyNode()
		{
			Node newNode = null;
			bool foundOne = false;

			while (!foundOne)
			{
				int randomNumber = random.Next(0, MainModel.Instance.Area.AllNodes.Count - 1);
				newNode = MainModel.Instance.Area.AllNodes.ElementAt(randomNumber);

                foundOne = true;
				foreach (Entity entity in MainModel.Instance.Entities)
				{
					if (newNode == entity.Node)
					{
                        foundOne = false;
						break;
					}
				}
			}

            this.Node = newNode;
		}

        public void AddBehaviour(StateBehaviour behaviour)
        {
            this.behaviour[behaviour.State] = behaviour;
        }

		public virtual void Draw(GameTime gameTime)
		{
			MainView.Instance.SpriteBatch.Draw(Texture, (Node.Position - Origin), this.ColorToUse);
		}

        protected virtual Color ColorToUse
        {
            get { return Color.White; }
        }
	}
}
