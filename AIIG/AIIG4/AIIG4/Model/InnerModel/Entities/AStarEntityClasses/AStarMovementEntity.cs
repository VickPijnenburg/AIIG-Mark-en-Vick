using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.GraphClasses;
using Microsoft.Xna.Framework.Graphics;

namespace AIIG4.Model.InnerModel.Entities.AStarEntityClasses
{
    public class AStarMovementEntity : GraphMovingEntity
    {

        //Fields

        private LinkedList<Edge> currentRouteEdges;
        private LinkedList<Node> currentRouteNodes;

        private Node newTargetNode;



        //Constructors

        public AStarMovementEntity(EntityManager.EntityType entityType, Texture2D startTexture, Node startNode)
            : base(entityType, startTexture, startNode)
        {
            this.currentRouteEdges = new LinkedList<Edge>();
            this.currentRouteNodes = new LinkedList<Node>();

            this.newTargetNode = null;
        }



        //Methods

        public void PlanRouteToNode(Node node)
        {
            if(!currentRouteNodes.Contains(node))
            {
                this.newTargetNode = node;
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (!this.IsMoving)
            {
                if (this.newTargetNode != null)
                {
                    PlanAStarRoute();
                }

                if (this.currentRouteEdges.Count > 0)
                {
                    MoveOverEdge(this.currentRouteEdges.First.Value);
                    this.currentRouteEdges.RemoveFirst();
                    this.currentRouteNodes.RemoveFirst();
                }
            }
        }


        /*AStar*/

        private void PlanAStarRoute()
        {
            AStarNodeCapsule endAStarNodeCapsule = FindCapsuleWithAStarForEndNode(newTargetNode);

            UpdateCurrentRouteWithEndCapsuleData(endAStarNodeCapsule);

            newTargetNode = null;
        }

        private AStarNodeCapsule FindCapsuleWithAStarForEndNode(Node endNode)
        {
            Dictionary<int, AStarNodeCapsule> capsuleMap;
            SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> closedList;
            SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> openList;
            SetupAStarStart(out capsuleMap, out closedList, out openList, this.PositionNode, endNode);

            bool failed = false;
            while (!failed)
            {
                if (closedList.Last().Value.Last().Value.Node == endNode)
                {
                    return closedList.Last().Value.Last().Value;
                }

                PerformAStarStep(capsuleMap, closedList, openList);
            }

            return null;
        }

        private void UpdateCurrentRouteWithEndCapsuleData(AStarNodeCapsule endCapsule)
        {
            currentRouteEdges.Clear();
            currentRouteNodes.Clear();

            AStarNodeCapsule currentCapsule = endCapsule;
            while (currentCapsule.PreviousRouteNode != null)
            {
                currentRouteEdges.AddFirst(currentCapsule.PreviousRouteEdge);
                currentRouteNodes.AddFirst(currentCapsule.Node);
                currentCapsule = currentCapsule.PreviousRouteNode;
            }
        }


        /*AStar steps*/

        private void PerformAStarStep(Dictionary<int, AStarNodeCapsule> capsuleMap, SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> closedList, SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> openList)
        {
            ApplyShortestDistanceToAdjacentsOfNewAddition(capsuleMap, closedList, openList);

            if (openList.Count > 0)
            {
                AStarNodeCapsule newClosedNode = openList.First().Value.First().Value;
                RemoveNodeCapsuleFromOpenList(openList, newClosedNode);
                AddNodeCapsuleToClosedList(closedList, newClosedNode);
            }
        }

        private void ApplyShortestDistanceToAdjacentsOfNewAddition(Dictionary<int, AStarNodeCapsule> capsuleMap, SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> closedList, SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> openList)
        {
            AStarNodeCapsule newAddition = closedList.Last().Value.Last().Value;
            foreach (Edge edge in newAddition.Node.Edges)
            {
                AStarNodeCapsule adjacent = GetAdjacent(capsuleMap, newAddition, edge);
                int possibleNewShortestDistance = (int)newAddition.ShortestDistance + edge.Cost;

                if (adjacent.ShortestDistance == null)
                {
                    adjacent.ShortestDistance = possibleNewShortestDistance;
                    adjacent.PreviousRouteNode = newAddition;
                    adjacent.PreviousRouteEdge = edge;
                    AddNodeCapsuleToOpenList(openList, adjacent);
                }
                else if ((int)adjacent.ShortestDistance > possibleNewShortestDistance)
                {
                    RemoveNodeCapsuleFromOpenList(openList, adjacent);
                    adjacent.ShortestDistance = possibleNewShortestDistance;
                    adjacent.PreviousRouteNode = newAddition;
                    adjacent.PreviousRouteEdge = edge;
                    AddNodeCapsuleToOpenList(openList, adjacent);
                }

            }
        }


        /*AStar preparation*/

        private void SetupAStarStart(out Dictionary<int, AStarNodeCapsule> capsuleMap, out SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> closedList, out SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> openList, Node startNode, Node endNode)
        {
            capsuleMap = SetupCapsuleMap(endNode);

            closedList = new SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>>();
            openList = new SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>>();

            capsuleMap[startNode.ID].ShortestDistance = 0;
            AddNodeCapsuleToClosedList(closedList, capsuleMap[startNode.ID]);
        }

        private Dictionary<int, AStarNodeCapsule> SetupCapsuleMap(Node endNode)
        {
            Dictionary<int, AStarNodeCapsule> capsuleMap = new Dictionary<int, AStarNodeCapsule>();

            foreach (Node node in MainModel.Instance.Graph.AllNodes)
            {
                capsuleMap.Add(node.ID, new AStarNodeCapsule(node, endNode));
            }

            return capsuleMap;
        }


        /*Convenience*/

        private AStarNodeCapsule GetAdjacent(Dictionary<int, AStarNodeCapsule> capsuleMap, AStarNodeCapsule capsule, Edge edge)
        {
            if (edge.Node1 == capsule.Node)
            {
                return capsuleMap[edge.Node2.ID];
            }
            else
            {
                return capsuleMap[edge.Node1.ID];
            }
        }

        private void AddNodeCapsuleToOpenList(SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> list, AStarNodeCapsule capsule)
        {
            int distanceToEnd = capsule.Priority;
            if (!list.ContainsKey(distanceToEnd))
            {
                list[distanceToEnd] = new SortedDictionary<int, AStarNodeCapsule>();
            }
            list[distanceToEnd][capsule.Node.ID] = capsule;
        }

        private void RemoveNodeCapsuleFromOpenList(SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> list, AStarNodeCapsule capsule)
        {
            list[(int)capsule.Priority].Remove(capsule.Node.ID);
            if (list[(int)capsule.Priority].Count == 0)
            {
                list.Remove(capsule.Priority);
            }
        }

        private void AddNodeCapsuleToClosedList(SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> list, AStarNodeCapsule capsule)
        {
            if (capsule.ShortestDistance != null)
            {
                int distance = (int)capsule.ShortestDistance;
                if (!list.ContainsKey(distance))
                {
                    list[distance] = new SortedDictionary<int, AStarNodeCapsule>();
                }
                list[distance][capsule.Node.ID] = capsule;
            }
        }

        
    }
}
