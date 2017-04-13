namespace CG_A2.Components {

/*--------------------------------------
 * USINSG
 *------------------------------------*/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Core;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

// TODO: Move to own file
public class CBody: Component {
    public Vector3 Position;
    public Vector3 Velocity;
    public float Heading;
}

/// <summary>Contains state information about a model.</summary>
public class CModel: Component {
    /*--------------------------------------
     * PUBLIC PROPERTIES
     *------------------------------------*/

    /// <summary>Gets or sets the model.</summary>
    public Model Model { get; set; }

    /// <summary>Gets or sets the model transform.</summary>
    public Matrix Transform { get; set; } = Matrix.Identity;
}

// TODO: Move to own file.
public class CHeightmap: Component {
    public IndexBuffer IndexBuffer { get; set; }
    public VertexBuffer VertexBuffer { get; set; }

    public Matrix Transform { get; set; } = Matrix.Identity;

    public int NumVertices { get; set; }
    public int NumTriangles { get; set; }
}

}
