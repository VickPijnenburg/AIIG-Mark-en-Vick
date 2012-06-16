using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.Entities;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AIIG4.Model.InnerModel.BehaviourClasses.AutonomousBehaviourClasses;
using AIIG4.Model.InnerModel.Entities.AStarEntityClasses;
using AIIG4.Model.InnerModel.BehaviourClasses.GraphMovingBehaviourClasses;
using AIIG4.Model.InnerModel.Entities.FlockEntityClasses;

namespace AIIG4.Model.InnerModel.Factories
{
    public abstract class EntityFactory
    {

        //////////////////////////////
        //Constants//
        //////////////////////////////


        /*randomGenerator*/

        private static readonly Random randomGenerator = new Random();


        /*Turret*/

        private const String TURRET_TEXTURE_NAME = "GameAssets/Turret";


        /*Cows*/

        private const int NUMBER_OF_COWS = 20;

        private const String COW_TEXTURE_NAME = "GameAssets/lemmling_Cartoon_cow";
        private const float COW_SCALE = 0.7f;
        private const float COW_PROPULSION = 0.05f;
        private const float COW_STEERING_FORCE = 0.04f;


        /*Projectiles*/

        private const String PROJECTILE_TEXTURE_NAME = "GameAssets/laser";
        private const float PROJECTILE_PROPULSION = 0.1f;



        //////////////////////////////
        //Methods//
        //////////////////////////////


        /*Start entities*/

        public static void CreateStartEntities()
        {
            CreateTurret();
            CreateCowFlock();
        }


        /*Turret*/

        private static void CreateTurret()
        {

            //Creating turret

            Texture2D turretTexture = MainGame.Instance.Content.Load<Texture2D>(TURRET_TEXTURE_NAME);
            AStarMovementEntity turret = new AStarMovementEntity(
                EntityManager.EntityType.Turret,
                turretTexture,
                MainModel.Instance.Graph.AllNodes.First.Value);

            //Adding behaviour

            GroupChasingGraphMovement cowChasing = new GroupChasingGraphMovement(turret, EntityManager.EntityType.FlockMember);
        }


        /*Cows*/

        private static void CreateCowFlock()
        {
            Flock flock = new Flock();
            for (int i = 0; i < NUMBER_OF_COWS; i++)
            {
                CreateCow(flock, CreateRandomPosition(), CreateRandomHeading());
            }
        }

        private static void CreateCow(Flock flock, Vector2 startPosition, Vector2 startHeading)
        {
            //Creating cow

            Texture2D cowTexture = MainGame.Instance.Content.Load<Texture2D>(COW_TEXTURE_NAME);
            FlockEntity cow = new FlockEntity(EntityManager.EntityType.FlockMember, cowTexture, flock)
                {
                    Scale = COW_SCALE,
                    Position = startPosition,
                    Heading = startHeading
                };


            //Adding behaviour

            new ConstantPropulsion(cow, COW_PROPULSION);
            new FlockSteering(cow, COW_STEERING_FORCE);
        }


        /*Projectile*/

        public static void CreateProjectile(Vector2 startPosition, Vector2 startHeading)
        {
            //creating entity
            Texture2D projectileTexture = MainGame.Instance.Content.Load<Texture2D>(PROJECTILE_TEXTURE_NAME);
            AutonomousEntity projectile = new AutonomousEntity(EntityManager.EntityType.Projectile, projectileTexture)
                {
                    Position = startPosition,
                    Heading = startHeading
                };

            //adding behaviour
            new ConstantPropulsion(projectile, PROJECTILE_PROPULSION);
        }


        /*Convenience*/

        private static Vector2 CreateRandomPosition()
        {
            float randomX = (float)randomGenerator.NextDouble() * MainGame.Instance.GameAreaRect.Width;
            float randomY = (float)randomGenerator.NextDouble() * MainGame.Instance.GameAreaRect.Height;

            return new Vector2(randomX, randomY);
        }

        private static Vector2 CreateRandomHeading()
        {
            double randomAngle = (randomGenerator.NextDouble() * Math.PI * 2.0f);

            return new Vector2((float)Math.Cos(randomAngle), (float)Math.Sin(randomAngle));
        }

    }
}
