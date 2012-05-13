using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AIIG.Model;

namespace AIIG.Controller
{
    public class MainController
    {

        //Fields

        private static MainController instance;

        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;



        //Constructors

        public MainController()
        {
            instance = this;

            UpdateKeyboardStates();
        }



        //Properties

        public static MainController Instance
        {
            get
            {
                if (instance == null)
                {
                    new MainController();
                }
                return instance;
            }
        }



        //Methods

        public void Update(GameTime gameTime)
        {
            UpdateKeyboardStates();
            UpdateEvents();
        }

        private void UpdateKeyboardStates()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

        private void UpdateEvents()
        {
            MainModel.Instance.EventManagement.CowShouldMove = 
                (currentKeyboardState.IsKeyDown(Keys.Space)
                && !previousKeyboardState.IsKeyDown(Keys.Space)
                );
        }
    }
}
