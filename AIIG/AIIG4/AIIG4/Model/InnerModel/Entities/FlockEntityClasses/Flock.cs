using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIIG4.Model.InnerModel.Entities.FlockEntityClasses
{
    public class Flock
    {

        //Fields

        private SortedDictionary<int, FlockEntity> flockMembers;



        //Constructors

        public Flock()
        {
            this.flockMembers = new SortedDictionary<int, FlockEntity>();
        }



        //Properties

        public IEnumerable<FlockEntity> Members
        {
            get { return this.flockMembers.Values; }
        }



        //Methods

        public void InsertFlockEntity(FlockEntity flockEntity)
        {
            this.flockMembers[flockEntity.Id] = flockEntity;
        }

        public void RemoveFlockEntity(FlockEntity flockEntity)
        {
            if (this.flockMembers.ContainsKey(flockEntity.Id))
            {
                this.flockMembers.Remove(flockEntity.Id);
            }
        }
    }
}
