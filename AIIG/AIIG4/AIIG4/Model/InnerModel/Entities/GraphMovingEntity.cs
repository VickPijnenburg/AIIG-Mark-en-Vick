using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using AIIG4.Model.InnerModel.GraphClasses;
using Microsoft.Xna.Framework;

namespace AIIG4.Model.InnerModel.Entities
{
    public class GraphMovingEntity : Entity
    {

        //////////////////////////////
        //Constants//
        //////////////////////////////

        private const int INITIAL_ELAPSED_TIME = 0;
        private const int MILLISECONDS_PER_COST_POINT = 1000;



        //////////////////////////////
        //Fields//
        //////////////////////////////

        private Node positionNode;
        private Edge movementEdge;

        private int elapsedTimeMoving;



        //////////////////////////////
        //Constructors//
        //////////////////////////////

        public GraphMovingEntity(EntityManager.EntityType entityType, Texture2D startTexture, Node startNode)
            : base(entityType, startTexture)
        {
            this.positionNode = startNode;
            this.Position = positionNode.Position;

            this.elapsedTimeMoving = INITIAL_ELAPSED_TIME;
        }



        //////////////////////////////
        //Properties//
        //////////////////////////////

        public Node PositionNode
        {
            get { return this.positionNode; }
            set { this.positionNode = value; }
        }

        public bool IsMoving
        {
            get { return (this.movementEdge != null); }
        }



        //////////////////////////////
        //Methods//
        //////////////////////////////


        /*Move over edge method*/

        public void MoveOverEdge(Edge edge)
        {
            this.movementEdge = edge;
        }


        /*Update methods*/

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            MoveAsNeeded(gameTime);
        }


        /*Movement methods*/

        private void MoveAsNeeded(GameTime gameTime)
        {
            if (this.IsMoving)
            {
                ProceedMoveTime(gameTime);

                int totalMoveTime = movementEdge.Cost * MILLISECONDS_PER_COST_POINT;

                if (elapsedTimeMoving >= totalMoveTime)
                {
                    ArriveAtNode();
                }
                else
                {
                    SetNewPosition(totalMoveTime);
                }
            }
        }

        private void ProceedMoveTime(GameTime gameTime)
        {
            this.elapsedTimeMoving += gameTime.ElapsedGameTime.Milliseconds;
        }

        private void ArriveAtNode()
        {
            this.PositionNode = DetermineNodeToMoveTo();
            this.Position = this.PositionNode.Position;
            this.movementEdge = null;
            this.elapsedTimeMoving = INITIAL_ELAPSED_TIME;
        }

        private void SetNewPosition(int totalMoveTime)
        {
            Vector2 movedDistance = CalculateDistanceToMove();
            movedDistance *= elapsedTimeMoving;
            movedDistance /= totalMoveTime;

            this.Position = this.PositionNode.Position + movedDistance;
        }


        /*Convenience*/

        private Node DetermineNodeToMoveTo()
        {
            if (this.movementEdge != null)
            {
                if (this.movementEdge.Node1 == this.PositionNode)
                {
                    return this.movementEdge.Node2;
                }
                else
                {
                    return this.movementEdge.Node1;
                }
            }
            else
            {
                return null;
            }
        }

        private Vector2 CalculateDistanceToMove()
        {
            Node nodeToMoveTo = DetermineNodeToMoveTo();

            if (nodeToMoveTo != null)
            {
                return (nodeToMoveTo.Position - this.PositionNode.Position);
            }
            else
            {
                Console.WriteLine("Fout! Edge is niet aan de huidige node gelinkt.");
                return Vector2.Zero;
            }
        }
    }
}
