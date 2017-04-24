namespace CG_A2.Components {

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Core;

public class CBody: Component {
    public Vector3 Position;
    public Vector3 Velocity;
    public float Heading;
    public float BoundingRadius { get; set; } = 3.0f;
    public bool Movable { get; set; } = false;
}

}
