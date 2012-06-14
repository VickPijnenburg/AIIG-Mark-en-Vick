using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.Entities;

namespace AIIG4.Model.InnerModel.BehaviourClasses.GraphMovingBehaviourClasses
{
    public class GraphMovingBehaviour : Behaviour
    {

        //Constructors

        public GraphMovingBehaviour(GraphMovingEntity host)
            : base(host)
        { }



        //Properties

        new public GraphMovingEntity Host
        {
            get { return (GraphMovingEntity)base.Host; }
        }
    }
}
