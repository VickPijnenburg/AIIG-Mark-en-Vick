using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIIG.Model.StateBehaviours
{
    public class FleeToEmptyNode : StateBehaviour
    {

        //Constructors

        public FleeToEmptyNode(Entity host)
            : base (Entity.State.Fleeing, host)
        { }



        //Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Host.GoToRandomEmptyNode();
            Host.CurrentState = Entity.State.Idle;
            base.Update(gameTime);
        }

    }
}
