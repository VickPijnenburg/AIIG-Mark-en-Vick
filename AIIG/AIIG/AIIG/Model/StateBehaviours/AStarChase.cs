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

        LinkedList<Node> currentRoute;



        //Constructors

        public AStarChase(Entity host)
            : base(Entity.State.Chasing, host)
        {
            currentRoute = new LinkedList<Node>();
        }



        //Methods

        public override void Update(GameTime gameTime)
        {
            if (this.currentRoute.Count == 0)
            {
                AStarNodeCapsule endAStarNodeCapsule = FindCapsuleWithAStar(Host.Node, MainModel.Instance.Hare.Node);
                this.currentRoute = GetRouteFromEndAStarNodeCapsule(endAStarNodeCapsule);
            }
            if (this.currentRoute.Count > 0)
            {
                Host.Node = currentRoute.First.Value;
                currentRoute.RemoveFirst();
            }
        }

        private AStarNodeCapsule FindCapsuleWithAStar(Node startPoint, Node endPoint)
        {
            Dictionary<int, AStarNodeCapsule> capsuleMap;
            SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> closedList;
            SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> openList;
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
                route.AddFirst(currentCapsule.Node);
                currentCapsule = currentCapsule.PreviousRouteNode;
            }
            route.RemoveFirst();

            System.Console.WriteLine("New route:");
            foreach(Node node in route)
            {
                System.Console.WriteLine(node.ID);
            }

            return route;
        }


        /*Setup methods*/
        private void SetupAStarStart(out Dictionary<int, AStarNodeCapsule> capsuleMap, out SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> closedList, out SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> openList, Node startPoint)
        {
            capsuleMap = SetupCapsuleMap();

            closedList = new SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>>();
            openList = new SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>>();

            capsuleMap[startPoint.ID].ShortestDistance = 0;
            AddNodeCapsuleToList(closedList, capsuleMap[startPoint.ID]);
        }

        private Dictionary<int, AStarNodeCapsule> SetupCapsuleMap()
        {
            Dictionary<int, AStarNodeCapsule> capsuleMap = new Dictionary<int, AStarNodeCapsule>();

            foreach (Node node in MainModel.Instance.Area.AllNodes)
            {
                capsuleMap.Add(node.ID, new AStarNodeCapsule(node));
            }

            return capsuleMap;
        }


        /*AStar steps*/

        private void PerformAStarStep(Dictionary<int, AStarNodeCapsule> capsuleMap, SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> closedList, SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> openList)
        {
            ApplyShortestDistanceToAdjacentsOfNewAddition(capsuleMap, closedList, openList);

            if (openList.Count > 0)
            {
                AStarNodeCapsule newClosedNode = openList.First().Value.First().Value;
                RemoveNodeCapsuleFromList(openList, newClosedNode);
                AddNodeCapsuleToList(closedList, newClosedNode);
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
                    AddNodeCapsuleToList(openList, adjacent);
                }
                else if ((int)adjacent.ShortestDistance > possibleNewShortestDistance)
                {
                    RemoveNodeCapsuleFromList(openList, adjacent);
                    adjacent.ShortestDistance = possibleNewShortestDistance;
                    adjacent.PreviousRouteNode = newAddition;
                    AddNodeCapsuleToList(openList, adjacent);
                }
                
            }
        }



        /*Convenience*/

        private void AddNodeCapsuleToList(SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> list, AStarNodeCapsule capsule)
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

        private void RemoveNodeCapsuleFromList(SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> list, AStarNodeCapsule capsule)
        {
            list[(int)capsule.ShortestDistance].Remove(capsule.Node.ID);
            if (list[(int)capsule.ShortestDistance].Count == 0)
            {
                list.Remove((int)capsule.ShortestDistance);
            }
        }

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
    }
}
