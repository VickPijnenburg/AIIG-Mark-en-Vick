using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIIG4.View
{
    public class MainView
    {

        //////////////////////////////
        //Properties//
        //////////////////////////////

        private static MainView instance;



        //////////////////////////////
        //Constructors//
        //////////////////////////////

        private MainView()
        {
            instance = this;
        }



        //////////////////////////////
        //Properties//
        //////////////////////////////

        public static MainView Instance
        {
            get
            {
                if (instance == null)
                {
                    new MainView();
                }
                return instance;
            }
        }
    }
}
