using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIIG.Model
{
    public class EventManager
    {
        
        //Fields

        private bool cowShouldMove;



        //Constructors

        public EventManager()
        { }



        //Properties

        public bool CowShouldMove
        {
            get { return cowShouldMove; }
            set { cowShouldMove = value; }
        }
    }
}
