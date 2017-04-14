namespace CG_A2.Components {

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Core;
using BoundingGeometries;

public class CHeightmap: Component {
    public IndexBuffer IndexBuffer { get; set; }
    public VertexBuffer VertexBuffer { get; set; }

    public Matrix Transform { get; set; } = Matrix.Identity;

    public int NumVertices { get; set; }
    public int NumTriangles { get; set; }

    public BoundingBox BoundingBox { get; set; }
}

}
