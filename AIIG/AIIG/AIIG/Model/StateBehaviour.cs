using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AIIG.Model
{
    public class StateBehaviour
    {

        //Fields

        private Entity.State state;
        private Entity host;



        //Constructors

        public StateBehaviour(Entity.State state, Entity host)
        {
            this.state = state;
            this.host = host;

            host.AddBehaviour(this);
        }



        //Properties

        public Entity.State State
        {
            get { return this.state; }
        }

        public Entity Host
        {
            get { return this.host; }
        }



        //Methods

        public virtual void Update(GameTime gameTime)
        {
            //Override this.
        }
    }
}
