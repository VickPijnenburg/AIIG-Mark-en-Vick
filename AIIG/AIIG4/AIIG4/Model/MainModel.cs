using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIIG4.Model.InnerModel;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AIIG4.Model.InnerModel.BehaviourClasses;

namespace AIIG4.Model
{
    public class MainModel
    {

        //////////////////////////////
        //Properties//
        //////////////////////////////

        private static MainModel instance;

        private Entity cow;
        private Entity hare;



        //////////////////////////////
        //Constructors//
        //////////////////////////////

        private MainModel()
        {
            instance = this;

            MakeCow();
            MakeHare();
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

        public Entity Cow
        {
            get { return this.cow; }
        }

        public Entity Hare
        {
            get { return this.hare; }
        }


        //////////////////////////////
        //Methods//
        //////////////////////////////

        private void MakeCow()
        {
            this.cow = new Entity(MainGame.Instance.Content.Load<Texture2D>("GameAssets/lemmling_Cartoon_cow"));
            this.cow.Position = new Vector2(200, 200);

            this.Cow.AddBehaviour(new InputSteeringBehaviour());
            this.Cow.AddBehaviour(new ConstantPropulsion(0.07f));
            this.Cow.AddBehaviour(new ChaseSteering(0.1f));
        }

        private void MakeHare()
        {
            this.hare = new Hare(MainGame.Instance.Content.Load<Texture2D>("GameAssets/rabbit-3"));
            this.hare.Position = new Vector2(100, 300);

            this.hare.AddBehaviour(new ConstantPropulsion(0.06f));
            this.hare.AddBehaviour(new Wandering(0.1f));
        }

        public void Update(GameTime gameTime)
        {
            this.cow.Update(gameTime);
            this.hare.Update(gameTime);
        }
    }
}
