namespace CG_A2.Components {

/*--------------------------------------
 * USINSG
 *------------------------------------*/

using System;

using Core;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Provides a way to attach extra logic to entities.</summary>
public class CLogic: Component {
    /*--------------------------------------
     * PUBLIC PROPERTIES
     *------------------------------------*/

    /// <summary>Gets or sets the logic update rate.</summary>
    public float InvUpdateRate { get; set; } = 1.0f/30.0f;

    /// <summary>Gets or sets the logic update function.</summary>
    public Action<float, float> UpdateFunc { get; set; }

    /// <summary>Reserved for use internally by the associated subsystem. Do not
    ///          change.</summary>
    public float UpdateTimer { get; set; }
}

}
