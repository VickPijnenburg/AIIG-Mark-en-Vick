using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using AIIG4.Model.InnerModel.BehaviourClasses;
using Microsoft.Xna.Framework;
using AIIG4.View;

namespace AIIG4.Model.InnerModel
{
	public class Entity
	{

        //////////////////////////////
        //Constants//
        //////////////////////////////

        private static readonly Vector2 INITIAL_STEERING_FORCE = Vector2.Zero;

        private const float DEFAULT_MASS = 50;
        private const float DEFAULT_MAX_SPEED = 100.0f;
        private const float DEFAULT_MAX_FORCE = 100;
        private const float DEFAULT_MAX_TURN_RATE = 100;

        private static readonly Vector2 INITIAL_HEADING = new Vector2(0, -1);
        private static readonly Vector2 INITIAL_SIDE = new Vector2(-1, 0);



        //////////////////////////////
        //Fields//
        //////////////////////////////

        private Texture2D texture;

        private LinkedList<Behaviour> behaviours;

        private float mass;
        private float maxSpeed;
        private float maxForce;
        private float maxTurnRate;

        private Vector2 position;
        private Vector2 velocity;
        private Vector2 heading;
        private Vector2 side;
        private Vector2 currentSteeringForce;



		//////////////////////////////
		//Constructors//
		//////////////////////////////

		public Entity(Texture2D startTexture)
		{
            this.texture = startTexture;

            this.behaviours = new LinkedList<Behaviour>();

            this.mass = DEFAULT_MASS;
            this.maxSpeed = DEFAULT_MAX_SPEED;
            this.maxForce = DEFAULT_MAX_FORCE;
            this.maxTurnRate = DEFAULT_MAX_TURN_RATE;

            this.heading = INITIAL_HEADING;
            this.side = INITIAL_SIDE;

            this.currentSteeringForce = INITIAL_STEERING_FORCE;
		}



        //////////////////////////////
        //Properties//
        //////////////////////////////

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public Vector2 Heading
        {
            get { return this.heading; }
        }

        public Vector2 Side
        {
            get { return this.side; }
        }

        protected virtual float RotationToUse
        {
            get
            {
                return (float)Math.Atan2(this.Heading.X, -this.Heading.Y);
            }
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
            behaviour.Host = this;
        }

        /*Update methods*/

        public virtual void Update(GameTime gameTime)
        {
            RefreshSteeringForce();
            UpdateBehaviours(gameTime);
            Move(gameTime);
        }

        private void RefreshSteeringForce()
        {
            this.currentSteeringForce = INITIAL_STEERING_FORCE;
        }

        private void UpdateBehaviours(GameTime gameTime)
        {
            foreach (Behaviour behaviour in this.behaviours)
            {
                behaviour.Update(gameTime);
            }
        }


        /*Movement methods*/

        public void ApplySteeringForce(Vector2 steeringForce)
        {
            this.currentSteeringForce += steeringForce;
        }

        private void Move(GameTime gameTime)
        {
            UpdateVelocity(gameTime);

            if (this.velocity.LengthSquared() > 0.000000001f)
            {
                UpdateHeading(gameTime);
                UpdatePosition(gameTime);
            }
        }

        private void UpdateVelocity(GameTime gameTime)
        {
            ApplyFrictionToVelocity(gameTime);

            Vector2 acceleration = this.currentSteeringForce / this.mass;
            this.velocity += acceleration * gameTime.ElapsedGameTime.Milliseconds;
            

            if (TooFast())
            {
                this.velocity.Normalize();
                this.velocity *= maxSpeed;
            }
        }

        private void ApplyFrictionToVelocity(GameTime gameTime)
        {
            ApplySteeringForce(-this.velocity * 0.3f);
        }

        private void UpdateHeading(GameTime gameTime)
        {
            if (this.velocity.LengthSquared() > 0.000000001f)
            {
                this.heading = this.velocity;
                this.heading.Normalize();

                this.side = new Vector2(this.heading.Y, -this.heading.X);
            }
        }

        private void UpdatePosition(GameTime gameTime)
        {
            this.Position += this.velocity * gameTime.ElapsedGameTime.Milliseconds;

            WrapAround();
        }

        private void WrapAround()
        {
            if ((this.position.X >= MainGame.FIELD_WIDTH) || (this.position.X < 0.0f))
            {
                this.position.X %= MainGame.FIELD_WIDTH;
            }
            if(this.position.X < 0.0f)
            {
                this.position.X += MainGame.FIELD_WIDTH;
            }

            if ((this.position.Y >= MainGame.FIELD_HEIGHT) || (this.position.Y < 0.0f))
            {
                this.position.Y %= MainGame.FIELD_HEIGHT;
            }
            if (this.position.Y < 0.0f)
            {
                this.position.Y += MainGame.FIELD_HEIGHT;
            }
        }


        /*Check methods*/

        private bool TooFast()
        {
            float maxSpeedSquared = this.maxSpeed * this.maxSpeed;

            return (this.velocity.LengthSquared() > maxSpeedSquared);
        }


        /*Draw methods*/

        public virtual void Draw(GameTime gameTime)
        {
            MainView.Instance.SpriteBatch.Draw(this.texture, this.Position, null, Color.White, this.RotationToUse, this.Origin, 1.0f, SpriteEffects.None, 0);
        }
	}
}
