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

        private static readonly Vector2 INITIAL_TARGET_DIRECTION = Vector2.Zero;



        //////////////////////////////
        //Fields//
        //////////////////////////////

        private Texture2D texture;

        private LinkedList<Behaviour> behaviours;

        private Vector2 targetDirection;



		//////////////////////////////
		//Constructors//
		//////////////////////////////

		public Entity(Texture2D startTexture)
		{
            this.texture = startTexture;

            this.behaviours = new LinkedList<Behaviour>();

            this.targetDirection = INITIAL_TARGET_DIRECTION;
		}



        //////////////////////////////
        //Methods//
        //////////////////////////////


        /*Update methods*/

        public virtual void Update(GameTime gameTime)
        {
            RefreshTargetDirection();
            UpdateBehaviours(gameTime);
            Move(gameTime);
        }

        public void RefreshTargetDirection()
        {
            this.targetDirection = INITIAL_TARGET_DIRECTION;
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
            //TODO: let this thing move to target
        }

	}
}
