using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.Entities;
using Microsoft.Xna.Framework;
using AIIG4.Model.InnerModel.Entities.AStarEntityClasses;
using AIIG4.Model.InnerModel.GraphClasses;

namespace AIIG4.Model.InnerModel.BehaviourClasses.GraphMovingBehaviourClasses
{
    public class GroupChasingGraphMovement : GraphMovingBehaviour
    {

        //Constants

        private const int CHECK_INTERVAL = 1000;
        private const int INITIAL_ELAPSED_TIME = 0;


        //Fields

        private EntityManager.EntityType groupToChase;

        private int elapsedTimeSinceLastCheck;
        


        //Constructors

        public GroupChasingGraphMovement(AStarMovementEntity host, EntityManager.EntityType groupToChase)
            : base(host)
        {
            this.groupToChase = groupToChase;
        }



        //Properties

        new public AStarMovementEntity Host
        {
            get { return (AStarMovementEntity)base.Host; }
        }



        //Methods

        public override void Update(GameTime gameTime)
        {
            ProceedCheckIntervalTime(gameTime);

            PlanRouteAsNeeded();
        }

        private void ProceedCheckIntervalTime(GameTime gameTime)
        {
            this.elapsedTimeSinceLastCheck += gameTime.ElapsedGameTime.Milliseconds;
        }

        private void PlanRouteAsNeeded()
        {
            if (elapsedTimeSinceLastCheck >= CHECK_INTERVAL)
            {
                Node targetNode = DetermineNewTargetNode();
                if (targetNode != null
                    && targetNode != Host.PositionNode)
                {
                    Host.PlanRouteToNode(targetNode);
                }

                elapsedTimeSinceLastCheck = INITIAL_ELAPSED_TIME;
            }
        }

        private Node DetermineNewTargetNode()
        {
            Vector2 targetPoint = CalculateAverageTargetGroupPosition();

            float? shortestDistanceFound = null;
            Node targetNode = null;

            foreach (Node node in MainModel.Instance.Graph.AllNodes)
            {
                float nodeDistanceToTargetPoint = (targetPoint - node.Position).LengthSquared();

                if (shortestDistanceFound == null
                    || nodeDistanceToTargetPoint < (float)shortestDistanceFound)
                {
                    shortestDistanceFound = nodeDistanceToTargetPoint;
                    targetNode = node;
                }
            }

            return targetNode;
        }

        private Vector2 CalculateAverageTargetGroupPosition()
        {
            LinkedList<Entity> targetList = MainModel.Instance.EntityManagement.GetEntitiesForType(this.groupToChase);

            Vector2 averageTargetGroupPosition = Vector2.Zero;

            if (targetList.Count > 0)
            {
                foreach (Entity target in targetList)
                {
                    averageTargetGroupPosition += target.Position;
                }

                averageTargetGroupPosition /= targetList.Count;
            }

            return averageTargetGroupPosition;
        }

    }
}
