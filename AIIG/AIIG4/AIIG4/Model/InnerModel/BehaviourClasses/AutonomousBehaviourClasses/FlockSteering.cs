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
        //Constants//
        //////////////////////////////

        private const float COLLISION_AVOID_PRIORITY = 2.0f;
        private const float FLOCK_PRIORITY = 4.0f;
        private const float ALIGN_PRIORITY = 6.0f;

        private const float DETECTION_DISTANCE = 115.0f;



        //////////////////////////////
        //Fields//
        //////////////////////////////

        private float steerForce;



        //////////////////////////////
        //Constructors//
        //////////////////////////////

        public FlockSteering(FlockEntity host, float steerForce)
            : base(host)
        {
            this.steerForce = steerForce;
        }



        //////////////////////////////
        //Properties//
        //////////////////////////////

        new public FlockEntity Host
        {
            get { return (FlockEntity)base.Host; }
        }



        //////////////////////////////
        //Methods//
        //////////////////////////////

        public override void Update(GameTime gameTime)
        {
            LinkedList<FlockEntity> neighbours = CollectNeighbours();

            if (neighbours.Count > 0)
            {
                Vector2 force = this.Host.Heading;

                ApplyCollisionAvoidance(neighbours, ref force);
                ApplyFlocking(neighbours, ref force);
                ApplyAllign(neighbours, ref force);

                force.Normalize();
                force *= this.steerForce;

                this.Host.ApplyForce(force);
            }

            base.Update(gameTime);
        }

        private void ApplyCollisionAvoidance(LinkedList<FlockEntity> neighbours, ref Vector2 force)
        {
            LinkedList<FlockEntity> neighboursToAvoid = CollectNeighbours();

            if (neighboursToAvoid.Count > 0)
            {
                Vector2 collisionAvoidanceVector = this.Host.Position;
                collisionAvoidanceVector -= CalculateAveragePosition(neighbours);
                collisionAvoidanceVector.Normalize();

                float priority = COLLISION_AVOID_PRIORITY;
                priority /= collisionAvoidanceVector.LengthSquared();
                priority *= DETECTION_DISTANCE;

                collisionAvoidanceVector *= priority;

                force += collisionAvoidanceVector;
            }
        }

        private void ApplyFlocking(LinkedList<FlockEntity> neighbours, ref Vector2 force)
        {
            if (neighbours.Count > 0)
            {
                Vector2 flockVector = CalculateAveragePosition(neighbours);
                flockVector -= this.Host.Position;
                flockVector *= FLOCK_PRIORITY;

                force += flockVector;
            }
        }

        private void ApplyAllign(LinkedList<FlockEntity> neighbours, ref Vector2 force)
        {
            if (neighbours.Count > 0)
            {
                Vector2 flockVector = CalculateAverageHeading(neighbours);
                flockVector *= ALIGN_PRIORITY;

                force += flockVector;
            }
        }




        /*Convenience*/

        private LinkedList<FlockEntity> CollectNeighbours()
        {
            LinkedList<FlockEntity> neighbours = new LinkedList<FlockEntity>();

            foreach (FlockEntity possibleNeighbour in this.Host.Flock.Members)
            {
                if (possibleNeighbour != this.Host)
                {
                    float possibleNeighbourDistanceSquared = (possibleNeighbour.Position - this.Host.Position).LengthSquared();
                    if (possibleNeighbourDistanceSquared <= (DETECTION_DISTANCE * DETECTION_DISTANCE))
                    {
                        neighbours.AddLast(possibleNeighbour);
                    }
                }
            }

            return neighbours;
        }

        private Vector2 CalculateAveragePosition(LinkedList<FlockEntity> neighbours)
        {
            Vector2 averageNeighbourPosition = Vector2.Zero;

            if (neighbours.Count > 0)
            {
                foreach (FlockEntity neighbour in neighbours)
                {
                    averageNeighbourPosition += neighbour.Position;
                }

                return (averageNeighbourPosition / neighbours.Count);
            }
            else
            {
                return Host.Position;
            }
        }

        private Vector2 CalculateAverageHeading(LinkedList<FlockEntity> neighbours)
        {
            Vector2 averageHeading = Vector2.Zero;

            if (neighbours.Count > 0)
            {
                foreach (FlockEntity neighbour in neighbours)
                {
                    averageHeading += neighbour.Heading;
                }

                return (averageHeading / neighbours.Count);
            }
            else
            {
                return Host.Heading;
            }
        }
    }
}
