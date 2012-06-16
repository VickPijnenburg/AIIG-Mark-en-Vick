using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.Entities;
using Microsoft.Xna.Framework;
using AIIG4.Model.InnerModel.Entities.FlockEntityClasses;

namespace AIIG4.Model.InnerModel.BehaviourClasses.AutonomousBehaviourClasses
{
    public class FlockSteering : AutonomousBehaviour
    {

        //////////////////////////////
        //Fields//
        //////////////////////////////

        private float steerForce;
        private float maxNeighbourDistance;
        private float collisionAvoidanceDistance;



        //////////////////////////////
        //Constructors//
        //////////////////////////////

        public FlockSteering(AutonomousEntity host, float steerForce, float maxNeighbourDistance)
            : base(host)
        {
            this.steerForce = steerForce;
            this.maxNeighbourDistance = maxNeighbourDistance;
        }



        //////////////////////////////
        //Methods//
        //////////////////////////////

        public override void Update(GameTime gameTime)
        {
            LinkedList<AutonomousEntity> neighbours = CollectNeighbours();

            Vector2 collisionAvoidanceVector = CalculateCollisionAvoidanceVector(neighbours);

            base.Update(gameTime);
        }

        public Vector2 CalculateCollisionAvoidanceVector(LinkedList<AutonomousEntity> neighbours)
        {
            return Vector2.Zero;
        }




        /*Convenience*/

        private LinkedList<AutonomousEntity> CollectNeighbours()
        {
            LinkedList<AutonomousEntity> neighbours = new LinkedList<AutonomousEntity>();

            foreach (Entity possibleNeighbour in MainModel.Instance.EntityManagement.GetEntitiesForType(this.Host.EntityType))
            {
                if (possibleNeighbour is AutonomousEntity)
                {
                    float possibleNeighbourDistanceSquared = (possibleNeighbour.Position - this.Host.Position).LengthSquared();
                    if (possibleNeighbourDistanceSquared <= (maxNeighbourDistance * maxNeighbourDistance))
                    {
                        neighbours.AddLast((AutonomousEntity)possibleNeighbour);
                    }
                }
            }

            return neighbours;
        }

        private LinkedList<FlockEntity> CollectNeighboursToAvoid(LinkedList<AutonomousEntity> neighbours)
        {
            return CollectNeighboursFromListWithinDistance(neighbours, this.collisionAvoidanceDistance);
        }

        private LinkedList<FlockEntity> CollectNeighboursFromListWithinDistance(System.Collections.IEnumerable possibleNeighbours, float distance)
        {
            LinkedList<FlockEntity> neighbours = new LinkedList<FlockEntity>();

            foreach (FlockEntity possibleNeighbour in possibleNeighbours)
            {
                float possibleNeighbourDistanceSquared = (possibleNeighbour.Position - this.Host.Position).LengthSquared();
                if (possibleNeighbourDistanceSquared <= (distance * distance))
                {
                    neighbours.AddLast(possibleNeighbour);
                }
            }

            return neighbours;
        }

    }
}
