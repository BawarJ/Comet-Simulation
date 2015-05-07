using System;

namespace CometSimulation
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Main simulation = new Main())
            {
                simulation.Run();
            }
        }
    }
#endif
}

