using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIIG4.Model
{
    public class MainModel
    {

        //////////////////////////////
        //Properties//
        //////////////////////////////

        private static MainModel instance;



        //////////////////////////////
        //Constructors//
        //////////////////////////////

        private MainModel()
        {
            instance = this;
        }



        //////////////////////////////
        //Properties//
        //////////////////////////////

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
    }
}
