#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Panzer;
#endregion

namespace Program
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Tank_War_Europa())
                game.Run();
        }
    }
}
