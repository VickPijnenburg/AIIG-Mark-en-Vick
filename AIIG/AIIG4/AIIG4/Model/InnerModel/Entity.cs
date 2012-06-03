using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using AIIG4.Model.InnerModel.BehaviourClasses;
using Microsoft.Xna.Framework;

namespace AIIG4.Model.InnerModel
{
	public class Entity
	{

        //////////////////////////////
        //Constants//
        //////////////////////////////

        private static readonly Vector2 INITIAL_STEERING_FORCE = Vector2.Zero;



        //////////////////////////////
        //Fields//
        //////////////////////////////

        private Texture2D texture;

        private LinkedList<Behaviour> behaviours;

        private float mass;
        private float maxSpeed;
        private float maxForce;
        private float maxTurnRate;

        private Vector2 Position;
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

            this.currentSteeringForce = INITIAL_STEERING_FORCE;
		}



        //////////////////////////////
        //Properties//
        //////////////////////////////


        //////////////////////////////
        //Methods//
        //////////////////////////////


        /*Update methods*/

        public virtual void Update(GameTime gameTime)
        {
            RefreshSteeringForce();
            UpdateBehaviours(gameTime);
            Move(gameTime);
        }

        public void RefreshSteeringForce()
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
            Vector2 acceleration = this.currentSteeringForce / this.mass;
            this.velocity += acceleration * gameTime.ElapsedGameTime.Milliseconds;
            if (TooFast())
            {
                this.velocity.Normalize();
                this.velocity *= maxSpeed;
            }
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
        }


        /*Check methods*/

        private bool TooFast()
        {
            float maxSpeedSquared = this.maxSpeed * this.maxSpeed;

            return (this.velocity.LengthSquared() > maxSpeedSquared);
        }

	}
}
