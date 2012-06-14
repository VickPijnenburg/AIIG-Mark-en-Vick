using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using AIIG4.Model.InnerModel.BehaviourClasses;
using Microsoft.Xna.Framework;
using AIIG4.View;

namespace AIIG4.Model.InnerModel.Entities
{
	public class Entity
	{

        //////////////////////////////
        //Constants//
        //////////////////////////////

        private static readonly Vector2 INITIAL_POSITION = Vector2.Zero;
        private static readonly Vector2 INITIAL_HEADING = new Vector2(0, -1);
        private static readonly Vector2 INITIAL_SIDE = new Vector2(-1, 0);


        //////////////////////////////
        //Fields//
        //////////////////////////////

        private Texture2D texture;

        private Vector2 position;
        private Vector2 heading;
        private Vector2 side;

        private LinkedList<Behaviour> behaviours;



		//////////////////////////////
		//Constructors//
		//////////////////////////////

		public Entity(Texture2D startTexture)
		{
            this.texture = startTexture;

            this.position = INITIAL_POSITION;
            this.heading = INITIAL_HEADING;
            this.side = INITIAL_SIDE;

            this.behaviours = new LinkedList<Behaviour>();
		}



        //////////////////////////////
        //Properties//
        //////////////////////////////

        public virtual Vector2 Position
        {
            get { return this.position; }
            set
            {
                if (this.position != value)
                {
                    this.position = value;
                }
            }
        }

        public virtual Vector2 Heading
        {
            get { return this.heading; }
            set
            {
                if (this.heading != value)
                {
                    this.heading = value;
                    this.side = new Vector2(this.Heading.Y, -this.Heading.X);
                }
            }
        }

        public virtual Vector2 Side
        {
            get { return this.side; }
            set
            {
                if (this.side != value)
                {
                    this.side = value;
                    this.heading = new Vector2(-this.Side.Y, this.Side.X);
                }
            }
        }
        
        protected virtual float RotationToUse
        {
            get { return (float)Math.Atan2(this.Heading.X, -this.Heading.Y); }
        }

        public Vector2 Origin
        {
            get { return new Vector2((this.texture.Width * 0.5f), (this.texture.Height * 0.5f)); }
        }



        //////////////////////////////
        //Methods//
        //////////////////////////////


        /*Behaviour addition methods*/

        public void AddBehaviour(Behaviour behaviour)
        {
            this.behaviours.AddLast(behaviour);
        }


        /*Update methods*/

        public virtual void Update(GameTime gameTime)
        {
            UpdateBehaviours(gameTime);
        }

        private void UpdateBehaviours(GameTime gameTime)
        {
            foreach (Behaviour behaviour in this.behaviours)
            {
                behaviour.Update(gameTime);
            }
        }


        /*Draw methods*/

        public virtual void Draw(GameTime gameTime)
        {
            MainView.Instance.SpriteBatch.Draw(
                this.texture,
                this.Position,
                null,
                Color.White,
                this.RotationToUse,
                this.Origin,
                1.0f,
                SpriteEffects.None,
                0);
        }
	}
}
