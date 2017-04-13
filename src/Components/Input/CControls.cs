namespace CG_A1.Components.Input {

/*--------------------------------------
 * USINSG
 *------------------------------------*/

using System.Collections.Generic;

using Core;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Contains input state information..</summary>
public class CControls: Component {
    /*--------------------------------------
     * PUBLIC PROPERTIES
     *------------------------------------*/

    public Dictionary<string, float> Controls { get; } =
        new Dictionary<string, float>();
}

}
