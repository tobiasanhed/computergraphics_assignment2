namespace CG_A2.Subsystems {

/*--------------------------------------
 * USINGS
 *------------------------------------*/

using Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Components;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Represents a renderingsubsystem.</summary>
public class LogicSubsystem: Subsystem {
    /*--------------------------------------
     * PUBLIC METHODS
     *------------------------------------*/

    /// <summary>Performs update logic specific to the subsystem.</summary>
    /// <param name="t">The total game time, in seconds.</param>
    /// <param name="dt">The elapsed time since last call, in seconds.</param>
    public override void Update(float t, float dt) {
        base.Update(t, dt);

        foreach (var entity in Scene.GetEntities<CLogic>()) {
            var logic = entity.GetComponent<CLogic>();

            var timer = logic.UpdateTimer + dt;

            var invUpdateRate = logic.InvUpdateRate;
            while (timer > invUpdateRate) {
                logic.UpdateFunc(t, dt);
                timer -= invUpdateRate;
            }

            logic.UpdateTimer = timer;
        }
    }
}

}
