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
using AIIG4.Model.InnerModel.GraphClasses;
using AIIG4.Model.InnerModel.Factories;

namespace AIIG4.Model
{
    public class MainModel
    {

        //////////////////////////////
        //Properties//
        //////////////////////////////

        private static MainModel instance;

        private Graph graph;

        private EntityManager entityManagement;



        //////////////////////////////
        //Constructors//
        //////////////////////////////

        private MainModel()
        {
            instance = this;

            this.graph = GraphFactory.CreateGraph();
            this.entityManagement = new EntityManager();

            EntityFactory.CreateStartEntities();
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

        public Graph Graph
        {
            get { return this.graph; }
        }

        public EntityManager EntityManagement
        {
            get { return this.entityManagement; }
        }



        //////////////////////////////
        //Methods//
        //////////////////////////////

        public void Update(GameTime gameTime)
        {
            this.EntityManagement.Update(gameTime);
        }
    }
}
