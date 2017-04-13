namespace CG_A2.Core {

/*--------------------------------------
 * USINGS
 *------------------------------------*/

using Microsoft.Xna.Framework;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Represents a camera.</summary>
public abstract class Camera {
    /*--------------------------------------
     * PUBLIC PROPERTIES
     *------------------------------------*/

    public Vector3 Up        { get; set; } = Vector3.Up;
    public Vector3 Position  { get; set; }
    public Matrix Projection { get; set; } =
        Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), 1.6f, 1.0f, 1000.0f);

    /*--------------------------------------
     * PUBLIC METHODS
     *------------------------------------*/

    public abstract Matrix ViewMatrix();

}

}
