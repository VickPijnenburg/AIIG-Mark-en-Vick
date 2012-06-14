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

        //Fields

        Node positionNode;
        Edge movementEdge;

        float edgeMovementProgress;

        bool realPositionIsUpToDate;



        //Constructors

        public GraphMovingEntity(Texture2D startTexture, Node startNode)
            : base(startTexture)
        {
            this.positionNode = startNode;

            this.Position = positionNode.Position;
            this.realPositionIsUpToDate = true;
        }



        //Properties

        public Node PositionNode
        {
            get { return this.positionNode; }
            set
            {
                if (this.positionNode != value)
                {
                    this.positionNode = value;

                    this.realPositionIsUpToDate = false;
                }
            }
        }

        public Edge MovementEdge
        {
            get { return this.movementEdge; }
            set
            {
                if (this.movementEdge != value)
                {
                    this.movementEdge = value;

                    this.realPositionIsUpToDate = false;
                }
            }
        }

        public float EdgeMovementProgress
        {
            get { return this.edgeMovementProgress; }
            set
            {
                if (this.edgeMovementProgress != value)
                {
                    this.edgeMovementProgress = value;

                    this.realPositionIsUpToDate = false;
                }
            }
        }

        public override Vector2 Position
        {
            get
            {
                if(!this.realPositionIsUpToDate)
                {
                    UpdateBasePosition();
                }
                return base.Position;
            }
        }



        //Methods

        private void UpdateBasePosition()
        {
            if (this.positionNode != null)
            {
                if (this.movementEdge != null)
                {
                    base.Position = CalculateRealPositionOnMovementEdge();
                }
                else
                {
                    base.Position = this.positionNode.Position;
                }
            }
            else
            {
                Console.WriteLine("Warning! GraphMovingEntity has no position node!");
                base.Position = Vector2.Zero;
            }

            this.realPositionIsUpToDate = true;
        }

        private Vector2 CalculateRealPositionOnMovementEdge()
        {
            Vector2 realPosition = this.PositionNode.Position;

            if (this.MovementEdge.Node1 == this.PositionNode)
            {
                realPosition += MovedEdgeDistanceForNodes(this.MovementEdge.Node1, this.MovementEdge.Node2);
            }
            else if (this.MovementEdge.Node2 == this.PositionNode)
            {
                realPosition += MovedEdgeDistanceForNodes(this.MovementEdge.Node2, this.MovementEdge.Node1);
            }
            else
            {
                Console.WriteLine("Fout! Edge is niet aan de huidige node gelinkt.");
            }

            return realPosition;
        }

        private Vector2 MovedEdgeDistanceForNodes(Node startNode, Node endNode)
        {
            if (this.edgeMovementProgress > 0.0f)
            {
                return (endNode.Position - startNode.Position) * this.edgeMovementProgress;
            }
            else
            {
                return startNode.Position;
            }
        }
    }
}
