namespace CG_A1.Components.Input {

/*--------------------------------------
 * USINSG
 *------------------------------------*/

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

using Core;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Contains input state information..</summary>
public class CInput: Component {
    /*--------------------------------------
     * PUBLIC PROPERTIES
     *------------------------------------*/

    public Dictionary<Keys, Action> KeyMap { get; } =
        new Dictionary<Keys, Action>();

    public Action ResetControls { get; set; }
}

}
