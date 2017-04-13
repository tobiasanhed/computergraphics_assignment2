namespace CG_A1.Subsystems {

/*--------------------------------------
 * USINGS
 *------------------------------------*/

using Microsoft.Xna.Framework.Input;

using Components.Input;
using Core;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Provides a subsystem for handling input.</summary>
public class InputSubsystem: Subsystem {
    /*--------------------------------------
     * PUBLIC METHODS
     *------------------------------------*/

    /// <summary>Performs draw logic specific to the subsystem.</summary>
    /// <param name="t">The total game time, in seconds.</param>
    /// <param name="dt">The elapsed time since last call, in seconds.</param>
    public override void Draw(float t, float dt) {
        // We're using the draw method here because input can really only change
        // from frame-to-frame, not multiple times within a single frame.
        base.Draw(t, dt);

        var keyboard = Keyboard.GetState();
        foreach (var entity in Scene.GetEntities<CInput>()) {
            var input = entity.GetComponent<CInput>();

            input.ResetControls?.Invoke();

            foreach (var e in input.KeyMap) {
                if (!keyboard.IsKeyDown(e.Key)) {
                    continue;
                }

                // Invoke the function associated with the key that is being
                // pressed.
                e.Value();
            }
        }
    }
}

}
