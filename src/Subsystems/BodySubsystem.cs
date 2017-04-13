namespace CG_A2.Subsystems {

/*--------------------------------------
 * USINGS
 *------------------------------------*/

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using Components.Input;
using Core;
using Components;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Provides a subsystem for responding to controls.</summary>
public class BodySubsystem: Subsystem {
    /*--------------------------------------
     * PUBLIC METHODS
     *------------------------------------*/

    /// <summary>Performs update logic specific to the subsystem.</summary>
    /// <param name="t">The total game time, in seconds.</param>
    /// <param name="dt">The elapsed time since last call, in seconds.</param>
    public override void Update(float t, float dt) {
        base.Update(t, dt);

        foreach (var entity in Scene.GetEntities<CBody>()) {
            var body = entity.GetComponent<CBody>();

            // Apply linear drag.
            body.Velocity -= 1.0f*dt*body.Velocity;
            body.Position += dt*body.Velocity;
        }
    }
}

}
