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

        public Entity Cow
        {
            get { return this.cow; }
        }


        //////////////////////////////
        //Methods//
        //////////////////////////////

        private void MakeCow()
        {
            this.cow = new Entity(MainGame.Instance.Content.Load<Texture2D>("GameAssets/lemmling_Cartoon_cow"));
            this.cow.Position = new Vector2(200, 200);
            this.Cow.AddBehaviour(new InputSteeringBehaviour());
        }

        public void Update(GameTime gameTime)
        {
            this.cow.Update(gameTime);
        }
    }
}
