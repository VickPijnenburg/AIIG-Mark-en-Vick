using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AIIG4.Model.InnerModel.Entities
{
    public class AutonomousEntity : Entity
    {

        //////////////////////////////
        //Constants//
        //////////////////////////////

        private const float DEFAULT_MASS = 50;
        private const float DEFAULT_MAX_SPEED = 100.0f;

        private static readonly Vector2 INITIAL_FORCE = Vector2.Zero;



        //////////////////////////////
        //Fields//
        //////////////////////////////

        private float mass;
        private float maxSpeed;
        private bool wrapsAround;

        private Vector2 velocity;
        private Vector2 currentForce;



        //////////////////////////////
        //Constructors//
        //////////////////////////////

        public AutonomousEntity(EntityManager.EntityType entityType, Texture2D startTexture, bool wrapsAround)
            : base(entityType, startTexture)
        {
            this.mass = DEFAULT_MASS;
            this.maxSpeed = DEFAULT_MAX_SPEED;
            this.wrapsAround = wrapsAround;

            this.currentForce = INITIAL_FORCE;
        }



        //////////////////////////////
        //Methods//
        //////////////////////////////


        /*Force addition methods*/

        public void ApplyForce(Vector2 steeringForce)
        {
            this.currentForce += steeringForce;
        }


        /*Update methods*/

        public override void Update(GameTime gameTime)
        {
            RefreshSteeringForce();

            base.Update(gameTime);

            Move(gameTime);
        }

        private void RefreshSteeringForce()
        {
            this.currentForce = INITIAL_FORCE;
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
            ApplyFrictionToVelocity(gameTime);

            Vector2 acceleration = this.currentForce / this.mass;
            this.velocity += acceleration * gameTime.ElapsedGameTime.Milliseconds;

            if (TooFast())
            {
                this.velocity.Normalize();
                this.velocity *= maxSpeed;
            }
        }

        private void ApplyFrictionToVelocity(GameTime gameTime)
        {
            ApplyForce(-this.velocity * 0.3f);
        }

        private void UpdateHeading(GameTime gameTime)
        {
            if (this.velocity.LengthSquared() > 0.000000001f)
            {
                Vector2 newHeading = this.velocity;
                newHeading.Normalize();
                this.Heading = newHeading;
            }
        }

        private void UpdatePosition(GameTime gameTime)
        {
            this.Position += this.velocity * gameTime.ElapsedGameTime.Milliseconds;

            if (DetermineIsOutOfBounds())
            {
                if (this.wrapsAround)
                {
                    WrapAround();
                }
                else
                {
                    this.RemoveFromGame();
                }
            }
        }

        private void WrapAround()
        {
            Vector2 newPosition = this.Position;

            if ((newPosition.X >= MainGame.Instance.GameAreaRect.Width)
                || (newPosition.X <= -MainGame.Instance.GameAreaRect.Width))
            {
                newPosition.X %= MainGame.Instance.GameAreaRect.Width;
            }
            if (newPosition.X < 0.0f)
            {
                newPosition.X += MainGame.Instance.GameAreaRect.Width;
            }

            if ((newPosition.Y >= MainGame.Instance.GameAreaRect.Height)
                || (newPosition.Y <= -MainGame.Instance.GameAreaRect.Height))
            {
                newPosition.Y %= MainGame.Instance.GameAreaRect.Height;
            }
            if (newPosition.Y < 0.0f)
            {
                newPosition.Y += MainGame.Instance.GameAreaRect.Height;
            }

            this.Position = newPosition;
        }

        private bool DetermineIsOutOfBounds()
        {
            Rectangle gameAreaRect = MainGame.Instance.GameAreaRect;
            return (this.Position.X < gameAreaRect.X
                || this.Position.X > gameAreaRect.Width
                || this.Position.Y < gameAreaRect.Y
                || this.Position.Y > gameAreaRect.Height);
        }


        /*Check methods*/

        private bool TooFast()
        {
            float maxSpeedSquared = this.maxSpeed * this.maxSpeed;

            return (this.velocity.LengthSquared() > maxSpeedSquared);
        }

    }
}
