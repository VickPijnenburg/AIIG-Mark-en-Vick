using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.Entities;
using Microsoft.Xna.Framework;

namespace AIIG4.Model.InnerModel
{
    public class EntityManager
    {

        //////////////////////////////
        //Enums//
        //////////////////////////////

        public enum EntityType
        {
            Turret,
            FlockMember,
            Projectile
        }


        //////////////////////////////
        //Fields//
        //////////////////////////////

        SortedDictionary<EntityType, LinkedList<Entity>> entities;



        //////////////////////////////
        //Constructors//
        //////////////////////////////

        public EntityManager()
        {
            entities = new SortedDictionary<EntityType, LinkedList<Entity>>();

            foreach(EntityType entityType in Enum.GetValues(typeof(EntityType)))
            {
                entities[entityType] = new LinkedList<Entity>();
            }
        }



        //////////////////////////////
        //Methods//
        //////////////////////////////


        /*Entity addition*/

        public void AddEntity(Entity entity)
        {
            entities[entity.EntityType].AddLast(entity);
        }


        /*Accessors*/

        public LinkedList<Entity> GetEntitiesForType(EntityType entityType)
        {
            return new LinkedList<Entity>(this.entities[entityType]);
        }


        /*Update methods*/

        public void Update(GameTime gameTime)
        {
            foreach (LinkedList<Entity> entitiesOfOneType in this.entities.Values)
            {
                UpdateEntitiesOfOneType(entitiesOfOneType, gameTime);
            }
        }

        private void UpdateEntitiesOfOneType(LinkedList<Entity> entityList, GameTime gameTime)
        {
            LinkedListNode<Entity> nextNode = entityList.First;

            while(nextNode != null)
            {
                LinkedListNode<Entity> currentNode = nextNode;
                nextNode = currentNode.Next;

                if (!currentNode.Value.IsRemoved)
                {
                    currentNode.Value.Update(gameTime);
                }
                if(currentNode.Value.IsRemoved)
                {
                    entityList.Remove(currentNode);
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (LinkedList<Entity> entityList in this.entities.Values)
            {
                foreach (Entity entity in entityList)
                {
                    entity.Draw(gameTime);
                }
            }
        }
    }
}
