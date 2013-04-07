using System;

namespace Exit_2R
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Exit_2R game = new Exit_2R())
            {
                game.Run();
            }
        }
    }
#endif
}

