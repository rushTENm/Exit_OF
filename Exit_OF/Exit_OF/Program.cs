using System;

namespace Exit_OF
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Exit_OF game = new Exit_OF())
            {
                game.Run();
            }
        }
    }
#endif
}

