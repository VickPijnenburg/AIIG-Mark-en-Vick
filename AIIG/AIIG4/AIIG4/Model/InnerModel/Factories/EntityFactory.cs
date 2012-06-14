using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel.Entities;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AIIG4.Model.InnerModel.BehaviourClasses.AutonomousBehaviourClasses;

namespace AIIG4.Model.InnerModel.Factories
{
    public abstract class EntityFactory
    {

        //////////////////////////////
        //Constants//
        //////////////////////////////


        /*Cow*/

        private static readonly Vector2 COW_START_POSITION = new Vector2(300, 200);
        private const float COW_PROPULSION = 0.07f;

        /*Projectile*/

        private const String PROJECTILE_TEXTURE_NAME = "GameAssets/laser";
        private const float PROJECTILE_PROPULSION = 0.1f;



        //////////////////////////////
        //Methods//
        //////////////////////////////


        /*Start entities*/

        public static void CreateStartEntities()
        {
            CreateCow();
        }



        /*Cow*/

        private static void CreateCow()
        {
            //Creating cow

            Texture2D cowTexture = MainGame.Instance.Content.Load<Texture2D>("GameAssets/lemmling_Cartoon_cow");
            AutonomousEntity cow = new AutonomousEntity(EntityManager.EntityType.FlockMember, cowTexture)
                {
                    Position = COW_START_POSITION
                };


            //Adding behaviour

            new InputSteeringBehaviour(cow);
            new ConstantPropulsion(cow, COW_PROPULSION);
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

    }
}
