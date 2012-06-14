using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.Entities;

namespace AIIG4.Model.InnerModel
{
    public class Flock
    {

        private SortedDictionary<int, Entity> entities;

        public Flock()
        {
            this.entities = new SortedDictionary<int, Entity>();
        }
    }
}
