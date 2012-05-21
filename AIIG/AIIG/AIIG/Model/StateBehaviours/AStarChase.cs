using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AIIG.Model.StateBehaviours
{
    public class AStarChase : StateBehaviour
    {

        //Fields

        LinkedList<Node> route;



        //Constructors

        public AStarChase(Entity host)
            : base(Entity.State.Chasing, host)
        { }



        //Methods

        public override void Update(GameTime gameTime)
        {
            if (this.route == null)
            {
                AStarNodeCapsule endAStarNodeCapsule = FindCapsuleWithAStar(Host.Node, MainModel.Instance.Hare.Node);
                this.route = GetRouteFromEndAStarNodeCapsule(endAStarNodeCapsule);
            }

            //Every time the cow steps: remove first node from route.
        }

        private AStarNodeCapsule FindCapsuleWithAStar(Node startPoint, Node endPoint)
        {
            Dictionary<Node, AStarNodeCapsule> capsuleMap;
            SortedDictionary<int, SortedDictionary<Node, AStarNodeCapsule>> closedList;
            SortedDictionary<int, SortedDictionary<Node, AStarNodeCapsule>> openList;
            SetupAStarStart(out capsuleMap, out closedList, out openList, startPoint);

            bool failed = false;
            while (!failed)
            {
                if (closedList.Last().Value.Last().Value.Node == endPoint)
                {
                    return closedList.Last().Value.Last().Value;
                }

                PerformAStarStep(capsuleMap, closedList, openList);
            }

            return null;
        }

        private LinkedList<Node> GetRouteFromEndAStarNodeCapsule(AStarNodeCapsule endCapsule)
        {
            LinkedList<Node> route = new LinkedList<Node>();

            AStarNodeCapsule currentCapsule = endCapsule;
            while (currentCapsule != null)
            {
                route.AddFirst(endCapsule.Node);
                currentCapsule = currentCapsule.PreviousRouteNode;
            }

            return route;
        }


        /*Setup methods*/
        private void SetupAStarStart(out Dictionary<Node, AStarNodeCapsule> capsuleMap, out SortedDictionary<int, SortedDictionary<Node, AStarNodeCapsule>> closedList, out SortedDictionary<int, SortedDictionary<Node, AStarNodeCapsule>> openList, Node startPoint)
        {
            capsuleMap = SetupCapsuleMap();

            closedList = new SortedDictionary<int, SortedDictionary<Node, AStarNodeCapsule>>();
            openList = new SortedDictionary<int, SortedDictionary<Node, AStarNodeCapsule>>();

            capsuleMap[startPoint].ShortestDistance = 0;
            AddNodeCapsuleToList(closedList, capsuleMap[startPoint]);
        }

        private Dictionary<Node, AStarNodeCapsule> SetupCapsuleMap()
        {
            Dictionary<Node, AStarNodeCapsule> capsuleMap = new Dictionary<Node, AStarNodeCapsule>();

            foreach (Node node in MainModel.Instance.Area.AllNodes)
            {
                capsuleMap.Add(node, new AStarNodeCapsule(node));
            }

            return capsuleMap;
        }


        /*AStar steps*/

        private void PerformAStarStep(Dictionary<Node, AStarNodeCapsule> capsuleMap, SortedDictionary<int, SortedDictionary<Node, AStarNodeCapsule>> closedList, SortedDictionary<int, SortedDictionary<Node, AStarNodeCapsule>> openList)
        {
            ApplyShortestDistanceToAdjacentsOfNewAddition(capsuleMap, closedList, openList);

            if (openList.Count > 0)
            {
                AStarNodeCapsule newClosedNode = openList.First().Value.First().Value;
                RemoveNodeCapsuleFromList(openList, newClosedNode);
                AddNodeCapsuleToList(closedList, newClosedNode);
            }
        }



        private void ApplyShortestDistanceToAdjacentsOfNewAddition(Dictionary<Node, AStarNodeCapsule> capsuleMap, SortedDictionary<int, SortedDictionary<Node, AStarNodeCapsule>> closedList, SortedDictionary<int, SortedDictionary<Node, AStarNodeCapsule>> openList)
        {
            AStarNodeCapsule newAddition = closedList.Last().Value.Last().Value;
            foreach (Edge edge in newAddition.Node.Edges)
            {
                AStarNodeCapsule adjacent = GetAdjacent(capsuleMap, newAddition, edge);
                if (adjacent.ShortestDistance == null)
                {
                    adjacent.ShortestDistance = edge.Cost;
                    adjacent.PreviousRouteNode = newAddition;
                    AddNodeCapsuleToList(openList, adjacent);
                }
                else
                {
                    int possibleNewShortestDistance = (int)newAddition.ShortestDistance + edge.Cost;
                    if ((int)adjacent.ShortestDistance > possibleNewShortestDistance)
                    {
                        RemoveNodeCapsuleFromList(openList, adjacent);
                        adjacent.ShortestDistance = possibleNewShortestDistance;
                        adjacent.PreviousRouteNode = newAddition;
                        AddNodeCapsuleToList(openList, adjacent);
                    }
                }
            }
        }



        /*Convenience*/

        private void AddNodeCapsuleToList(SortedDictionary<int, SortedDictionary<Node, AStarNodeCapsule>> list, AStarNodeCapsule capsule)
        {
            if (capsule.ShortestDistance != null)
            {
                int distance = (int)capsule.ShortestDistance;
                if (!list.ContainsKey(distance))
                {
                    list[distance] = new SortedDictionary<Node, AStarNodeCapsule>();
                }
                list[distance][capsule.Node] = capsule;
            }
        }

        private void RemoveNodeCapsuleFromList(SortedDictionary<int, SortedDictionary<Node, AStarNodeCapsule>> list, AStarNodeCapsule capsule)
        {
            list[(int)capsule.ShortestDistance].Remove(capsule.Node);
            if (list[(int)capsule.ShortestDistance].Count == 0)
            {
                list.Remove((int)capsule.ShortestDistance);
            }
        }

        private AStarNodeCapsule GetAdjacent(Dictionary<Node, AStarNodeCapsule> capsuleMap, AStarNodeCapsule capsule, Edge edge)
        {
            if (edge.Node1 == capsule.Node)
            {
                return capsuleMap[edge.Node2];
            }
            else
            {
                return capsuleMap[edge.Node1];
            }
        }
    }
}
