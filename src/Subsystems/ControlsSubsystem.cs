namespace CG_A2.Subsystems {

/*--------------------------------------
 * USINGS
 *------------------------------------*/

using System;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using Components.Input;
using Core;
using Components;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Provides a subsystem for responding to controls.</summary>
public class ControlsSubsystem: Subsystem {
    /*--------------------------------------
     * PUBLIC METHODS
     *------------------------------------*/

    /// <summary>Performs update logic specific to the subsystem.</summary>
    /// <param name="t">The total game time, in seconds.</param>
    /// <param name="dt">The elapsed time since last call, in seconds.</param>
    public override void Update(float t, float dt) {
        base.Update(t, dt);

        foreach (var entity in Scene.GetEntities<CControls>()) {
            var controls = entity.GetComponent<CControls>();

            if (controls.Controls.ContainsKey("Turn") && controls.Controls["Turn"] != 0) {
                var body = entity.GetComponent<CBody>();
                body.Heading += 2.5f*dt*controls.Controls["Turn"];
            }


            // TODO: This is a hack lol.
            if (controls.Controls.ContainsKey("Up") && controls.Controls["Up"] != 0.0) {
                var body = entity.GetComponent<CBody>();
                body.Velocity.X += (float)Math.Cos(body.Heading)*30.0f*dt * controls.Controls["Up"];
                body.Velocity.Z += (float)Math.Sin(body.Heading)*30.0f*dt * controls.Controls["Up"];

            }
        }
    }
}

}
