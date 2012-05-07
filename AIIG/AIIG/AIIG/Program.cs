using System;

namespace AIIG
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MainGame game = MainGame.Instance)
            {
                game.Run();
            }
        }
    }
#endif
}

