namespace CG_A2.Core {

/*--------------------------------------
 * USINGS
 *------------------------------------*/

using System;

using Microsoft.Xna.Framework;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Represents a camera</summary>
public class LookAtCamera : Camera {
    /*--------------------------------------
     * PRIVATE FIELDS
     *------------------------------------*/

    private object m_Target = Vector3.Zero;

    /*--------------------------------------
     * PUBLIC PROPERTIES
     *------------------------------------*/

    /// <summary>Gets or sets the look-at target.</summary>
    public object Target {
        get {
            return m_Target;
        }

        set {
            if (value is Vector3) {
                m_Target = value;
                return;
            }

            throw new ArgumentException("Unsupported target type");
        }
    }

    /*--------------------------------------
     * PUBLIC METHODS
     *------------------------------------*/

    public override Matrix ViewMatrix(){
        if (m_Target is Vector3) {
            return Matrix.CreateLookAt(Position, (Vector3)m_Target, Up);
        }

        return Matrix.Identity;
    }

}

}
