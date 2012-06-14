using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AIIG4.Model.InnerModel.Entities;

namespace AIIG4.Model.InnerModel.BehaviourClasses
{
    public class Behaviour
    {

        //////////////////////////////
        //Fields//
        //////////////////////////////

        private Entity host;



        //////////////////////////////
        //Constructors//
        //////////////////////////////

        public Behaviour(Entity host)
        {
            this.host = host;
            host.AddBehaviour(this);
        }



        //////////////////////////////
        //Properties//
        //////////////////////////////

        public Entity Host
        {
            get { return this.host; }
        }



        //////////////////////////////
        //Methods//
        //////////////////////////////

        public virtual void Update(GameTime gameTime)
        {
            //Override this
        }
    }
}
