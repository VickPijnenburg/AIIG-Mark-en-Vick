using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AIIG.Model.StateBehaviours;

namespace AIIG.Model
{
	public class Cow : Entity
	{

		//Constructors

		public Cow(Texture2D startTexture)
			: base(startTexture, State.Chasing)
		{
			GoToRandomEmptyNode();
		}



        //Methods

        public override void Update(GameTime gameTime)
        {
            if (MainModel.Instance.EventManagement.CowShouldMove)
            {
                base.Update(gameTime);
            }

            ChangeStateAsNeeded();
        }

        public void ChangeStateAsNeeded()
        {
            if (MainModel.Instance.Pill.Node == this.Node)
            {
                this.CurrentState = State.Chasing;
            }
            if (MainModel.Instance.Hare.Node == this.Node)
            {
                this.CurrentState = State.Wandering;
            }
        }

        protected override Color ColorToUse
        {
            get
            {
                switch (this.CurrentState)
                {
                    case State.Wandering:
                        {
                            return Color.Cyan;
                        }
                    case State.LookingForPill:
                        {
                            return Color.Orange;
                        }
                    case State.Chasing:
                        {
                            return Color.Red;
                        }
                    default:
                        {
                            return Color.Gray;
                        }
                }
            }
        }
	}
}
