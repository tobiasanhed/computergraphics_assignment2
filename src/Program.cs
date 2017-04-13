namespace CG_A2 {

/*--------------------------------------
 * USINGS
 *------------------------------------*/

using System;

using Core;
using Scenes;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

public static class Program {
    /*--------------------------------------
     * PRIVATE METHODS
     *------------------------------------*/

    /// <summary>Program entry point.</summary>
    /// <param name="args">The command line arguments.</param>
    [STAThread]
    private static void Main(string[] args) {
        using (var game = new Game1()) {
            game.Run(new MainScene());
        }
    }
}

}
