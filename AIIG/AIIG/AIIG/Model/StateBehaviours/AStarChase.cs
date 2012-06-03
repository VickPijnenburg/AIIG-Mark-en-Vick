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

        private LinkedList<Node> currentRoute;
        private Entity target;
        private Entity.State nextState;


        //Constructors

        public AStarChase(Entity.State state, Entity host, Entity target, Entity.State nextState)
            : base(state, host)
        {
            this.target = target;
            this.nextState = nextState;

            this.currentRoute = new LinkedList<Node>();
        }



        //Methods

        public override void Update(GameTime gameTime)
        {
            if (this.currentRoute.Count == 0)
            {
                AStarNodeCapsule endAStarNodeCapsule = FindCapsuleWithAStar();
                this.currentRoute = GetRouteFromEndAStarNodeCapsule(endAStarNodeCapsule);
            }
            if (this.currentRoute.Count > 0)
            {
                Host.Node = currentRoute.First.Value;
                currentRoute.RemoveFirst();
            }
            if (Host.Node == this.target.Node)
            {
                Host.CurrentState = this.nextState;
            }
        }

        private AStarNodeCapsule FindCapsuleWithAStar()
        {
            Dictionary<int, AStarNodeCapsule> capsuleMap;
            SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> closedList;
            SortedDictionary<int, SortedDictionary<int, AStarNodeCapsule>> openList;
            SetupAStarStart(out capsuleMap, out closedList, out openList, Host.Node);

            bool failed = false;
            while (!failed)
            {
                 if (closedList.Last().Value.Last().Value.Node == this.target.Node)
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
            AddNodeCapsuleToClosedList(closedList, capsuleMap[startPoint.ID]);
        }

        private Dictionary<int, AStarNodeCapsule> SetupCapsuleMap()
        {
            Dictionary<int, AStarNodeCapsule> capsuleMap = new Dictionary<int, AStarNodeCapsule>();

            foreach (Node node in MainModel.Instance.Area.AllNodes)
            {
                capsuleMap.Add(node.ID, new AStarNodeCapsule(node, this.target.Node));
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
                    AddNodeCapsuleToOpenList(openList, adjacent);
                }
                else if ((int)adjacent.ShortestDistance > possibleNewShortestDistance)
                {
                    RemoveNodeCapsuleFromOpenList(openList, adjacent);
                    adjacent.ShortestDistance = possibleNewShortestDistance;
                    adjacent.PreviousRouteNode = newAddition;
                    AddNodeCapsuleToOpenList(openList, adjacent);
                }
                
            }
        }



        /*Convenience*/

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
                System.Console.WriteLine("Closed node " + capsule.Node.ID);
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
