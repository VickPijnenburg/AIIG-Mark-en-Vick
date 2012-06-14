using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.Entities;

namespace AIIG4.Model.InnerModel.BehaviourClasses.AutonomousBehaviourClasses
{
    public class AutonomousBehaviour : Behaviour
    {

        //Constructors

        public AutonomousBehaviour(AutonomousEntity host)
            : base(host)
        { }



        //Properties

        new public AutonomousEntity Host
        {
            get { return (AutonomousEntity) base.Host; }
        }
    }
}
