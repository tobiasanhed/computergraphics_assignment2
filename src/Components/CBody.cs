namespace CG_A2.Components {

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Core;
using BoundingGeometries;

public class CBody: Component {
    public Vector3 Position;
    public Vector3 Velocity;
    public float Heading;
    public BoundingGeometry BoundingGeometry;

}

}
