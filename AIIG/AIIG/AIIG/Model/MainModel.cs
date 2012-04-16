using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIIG.Model
{
    class MainModel
    {
        private static MainModel instance;

        private MainModel()
        {
            instance = this;
        }

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
