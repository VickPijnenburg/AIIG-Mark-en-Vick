using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
            area = AreaFactory.CreateArea();
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

        public void Update(GameTime gameTime)
        {
            //nothing
        }
    }
}
