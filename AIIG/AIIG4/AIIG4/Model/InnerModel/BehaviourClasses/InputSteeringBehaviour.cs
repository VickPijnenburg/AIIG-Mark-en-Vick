using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AIIG4.Model.InnerModel.BehaviourClasses
{
    public class InputSteeringBehaviour : Behaviour
    {

        private const float FORCE = 0.1f;
        private const float STEER_FORCE = 0.055f;

        public InputSteeringBehaviour()
        {

        }


        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Vector2 steerForce = Vector2.Zero;

            KeyboardState currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                steerForce += Host.Heading * FORCE;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                steerForce += Host.Side * STEER_FORCE;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                steerForce -= Host.Side * STEER_FORCE;
            }

            this.Host.ApplySteeringForce(steerForce);

            base.Update(gameTime);
        }
    }
}
