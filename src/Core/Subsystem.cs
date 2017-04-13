namespace CG_A1.Core {

/*--------------------------------------
 * USINGS
 *------------------------------------*/

using System;
using System.Collections.Generic;
using System.Threading;

using Microsoft.Xna.Framework;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Represents the base class for a single subsytem.</summary>
public abstract class Subsystem {
    /*--------------------------------------
     * PUBLIC PROPERTIES
     *------------------------------------*/

    /// <summary>Gets or sets the scene that the subsystem is used in.</summary>
    public Scene Scene { get; set; }

    /*--------------------------------------
     * PUBLIC METHODS
     *------------------------------------*/

    /// <summary>Performs cleanup logic.</summary>
    public virtual void Cleanup() {
    }

    /// <summary>Performs draw logic specific to the subsystem.</summary>
    /// <param name="t">The total game time, in seconds.</param>
    /// <param name="dt">The elapsed time since last call, in seconds.</param>
    public virtual void Draw(float t, float dt) {
    }

    /// <summary>Performs initialization logic.</summary>
    public virtual void Init() {
    }

    /// <summary>Performs update logic specific to the subsystem.</summary>
    /// <param name="t">The total game time, in seconds.</param>
    /// <param name="dt">The elapsed time since last call, in seconds.</param>
    public virtual void Update(float t, float dt) {
    }
}

}
