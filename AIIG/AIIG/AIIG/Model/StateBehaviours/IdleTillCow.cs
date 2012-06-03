using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIIG.Model.StateBehaviours
{
    public class IdleTillCow : StateBehaviour
    {

        //Constructors

        public IdleTillCow(Entity host)
            : base(Entity.State.Idle, host)
        { }



        //Methods

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (MainModel.Instance.Cow.Node == Host.Node)
            {
                Host.CurrentState = Entity.State.Fleeing;
            }

            base.Update(gameTime);
        }

    }
}
