using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AIIG.Model.StateBehaviours
{
    public class AStarChase : StateBehaviour
    {

        //Constructors

        public AStarChase(Entity host)
            : base(Entity.State.Chasing, host)
        {

        }



        //Methods

        public void Update(GameTime gameTime)
        {

        }
    }
}
