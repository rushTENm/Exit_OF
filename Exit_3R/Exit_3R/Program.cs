using System;

namespace Exit_3R
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Exit_3R game = new Exit_3R())
            {
                game.Run();
            }
        }
    }
#endif
}