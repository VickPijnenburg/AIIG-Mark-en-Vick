using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AIIG4.Model.InnerModel.BehaviourClasses;
using AIIG4.Model.InnerModel.Entities;
using AIIG4.Model.InnerModel.BehaviourClasses.AutonomousBehaviourClasses;

namespace AIIG4.Model
{
    public class MainModel
    {

        //////////////////////////////
        //Properties//
        //////////////////////////////

        private static MainModel instance;

        private AutonomousEntity cow;



        //////////////////////////////
        //Constructors//
        //////////////////////////////

        private MainModel()
        {
            instance = this;

            MakeCow();
        }



        //////////////////////////////
        //Properties//
        //////////////////////////////

        public static MainModel Instance
        {
            get
            {
                if (instance == null)
                {
                    new MainModel();
                }
                return instance;
            }
        }

        public AutonomousEntity Cow
        {
            get { return this.cow; }
        }



        //////////////////////////////
        //Methods//
        //////////////////////////////

        private void MakeCow()
        {
            this.cow = new AutonomousEntity(MainGame.Instance.Content.Load<Texture2D>("GameAssets/lemmling_Cartoon_cow"));
            this.cow.Position = new Vector2(300, 200);

            new InputSteeringBehaviour(this.cow);
            new ConstantPropulsion(this.cow, 0.07f);
            new ChaseSteering(this.cow, 0.1f);
        }

        public void Update(GameTime gameTime)
        {
            this.cow.Update(gameTime);
        }
    }
}
