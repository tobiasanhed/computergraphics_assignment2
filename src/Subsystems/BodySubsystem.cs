namespace CG_A2.Subsystems {

/*--------------------------------------
 * USINGS
 *------------------------------------*/

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using Components.Input;
using Core;
using Components;

using static System.Math;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Provides a subsystem for responding to controls.</summary>
public class BodySubsystem: Subsystem {
    /*--------------------------------------
     * PUBLIC METHODS
     *------------------------------------*/

    /// <summary>Performs update logic specific to the subsystem.</summary>
    /// <param name="t">The total game time, in seconds.</param>
    /// <param name="dt">The elapsed time since last call, in seconds.</param>
    public override void Update(float t, float dt) {
        base.Update(t, dt);

        foreach (var entity in Scene.GetEntities<CBody>()) {
            var body = entity.GetComponent<CBody>();

            // Apply linear drag.
            body.Velocity -= 1.0f*dt*body.Velocity;
            body.Position += dt*body.Velocity;

            foreach (var e2 in Scene.GetEntities<CBody>()) {
                if (e2.ID <= entity.ID) {
                    continue;
                }

                var body2 = e2.GetComponent<CBody>();

                var m1 = entity.GetComponent<CModel>();
                var m2 = e2.GetComponent<CModel>();

                var p1 = body.Position;//new Vector3(m1.Transform.M41, m1.Transform.M42, m1.Transform.M43);
                var p2 = body2.Position;//new Vector3(m2.Transform.M41, m2.Transform.M42, m2.Transform.M43);

                p1.Y = p2.Y = 0.0f;

                var distSquared = Vector3.Dot(p2 - p1, p2 - p1);

                var r1 = body.BoundingRadius + body2.BoundingRadius;
                var r2 = r1;
                r2 *= r2;
                if (distSquared >= r2) {
                    continue;
                }

                var d = (float)Sqrt(r2);
                var pd = d - r1;

                var n = (p2 - p1);
                n.Normalize();

                if (body.Movable) {
                    body.Position -= n*pd*1.01f;
                    body.Velocity *= -1.0f;
                }
                else if (body2.Movable) {
                    body2.Position += n*pd*1.0f;
                    body2.Velocity *= -1.0f;

                }
            }
        }
    }
}

}
