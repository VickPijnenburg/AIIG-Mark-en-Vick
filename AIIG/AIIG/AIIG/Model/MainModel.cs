using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIIG.Model
{
    class MainModel
    {

        //Fields

        private static MainModel instance;

        private Area area;



        //Constructors

        private MainModel()
        {
            instance = this;
            area = new Area();
        }



        //Properties

        public static MainModel Instance
        {
            get
            {
                if (instance == null)
                {
                    new MainModel();
                }

                return instance;
            }
        }

        public Area Area
        {
            get { return area; }
        }
    }
}
