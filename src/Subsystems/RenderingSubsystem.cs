namespace CG_A2.Subsystems {

/*--------------------------------------
 * USINGS
 *------------------------------------*/

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Components;
using Core;
using Components.Input;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Represents a renderingsubsystem.</summary>
public class RenderingSubsystem: Subsystem {
    /*--------------------------------------
     * PUBLIC PROPERTIES
     *------------------------------------*/

    /// <summary>Gets or sets the camera used to draw the world.</summary>
    public Camera Camera { get; set; }

    /*--------------------------------------
     * PUBLIC METHODS
     *------------------------------------*/
    private BasicEffect bEffect = new BasicEffect(Game1.Inst.GraphicsDevice);
    private float turnDelta;
    private Texture2D groundTexture = Game1.Inst.Content.Load<Texture2D>("Textures/grass");
    private Texture2D pagaFaceTexture = Game1.Inst.Content.Load<Texture2D>("Textures/paga3");
    private SkyBox skyBox = new SkyBox(new Vector3(1000, 1000, 1000), new Vector3(0, -100, 0));
    /// <summary>Performs draw logic specific to the subsystem.</summary>
    /// <param name="t">The total game time, in seconds.</param>
    /// <param name="dt">The elapsed time since last call, in seconds.</param>
    public override void Draw(float t, float dt) {


        Game1.Inst.GraphicsDevice.Clear(Color.CornflowerBlue);
        base.Draw(t, dt);

        // Ritar denna först pga att den stänger av z-axeln när den ritar.
        skyBox.Draw(t, dt, bEffect);

        foreach (var entity in Scene.GetEntities<CModel>()) {
            var model = entity.GetComponent<CModel>();
            var control = entity.GetComponent<CControls>();
            var b = entity.GetComponent<CBody>();

            var tilt2 = Matrix.Identity;
            if(control != null && control.Controls.ContainsKey("Turn")){
                turnDelta += (control.Controls["Turn"] - turnDelta) * dt;
                tilt2 = Matrix.Identity;//Matrix.CreateRotationZ(-turnDelta * 0.18f * 0.65f * b.Velocity.Length());
            }
            var r = new Vector3(b.Velocity.Z, 0, -b.Velocity.X);
            r.Normalize();
            var a = b.Velocity.Length() * 0.05f * 0.65f;
            var tilt = Matrix.Identity;//Matrix.CreateFromAxisAngle(r, a);

            Matrix T;
            if (r.Length() > 0.0001f) {
                T = tilt2 * Matrix.CreateRotationY(-b.Heading-0.5f*3.141592653589f) * tilt * Matrix.CreateTranslation(b.Position.X, b.Position.Y, b.Position.Z);
            }
            else {
                T = tilt2 * Matrix.CreateRotationY(-b.Heading-0.5f*3.141592653589f) * Matrix.CreateTranslation(b.Position.X, b.Position.Y, b.Position.Z);
            }
            var m = model.Transform * T;


            Matrix[] transforms = new Matrix[model.Model.Bones.Count];
            model.Model.CopyAbsoluteBoneTransformsTo(transforms);

            if(model.IsTarget){
                //((LookAtCamera)Camera).Target = new Vector3(m.M41, m.M42*0.0f, m.M43);
                //var ta = ((LookAtCamera)Camera).Target;
                var p = b.Position;
                var c = ((LookAtCamera)Camera).Position;
                var dist = 60f;
                var yDist = -40f;
                var h = b.Heading;

                // Vi positionerar kamera utifrån karaktärens heading (h), p = karaktärerns position, c = kamerans position, t = kamerans target, dist = avstånd till objektet
                // yDist = höjd för kameran, samt t = p -- alltså att kamerans target är position för karaktären.
                // Då gäller c=p-[d*sin(h + pi/2), y, (-d)*cos(h + pi/2)]

                c = Vector3.Subtract(p, new Vector3((float)(dist * Math.Sin(h + Math.PI * 0.5f)), yDist, (float)((-dist) * Math.Cos(h + Math.PI * 0.5f))));

                ((LookAtCamera)Camera).Target = p;
                ((LookAtCamera)Camera).Position = c;
    
                /*var temp = transforms[3];
                transforms[1] *= Matrix.CreateRotationY(t*20f);
                transforms[3] *= Matrix.CreateTranslation(-transforms[3].M41, -transforms[3].M42, -transforms[3].M43);
                transforms[3] *= Matrix.CreateRotationX(t*-20f);
                transforms[3] *= Matrix.CreateTranslation(temp.M41, temp.M42, temp.M43);*/
            }            


            foreach (var mesh in model.Model.Meshes) {
                foreach (BasicEffect effect in mesh.Effects) {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * m;
                    effect.View = Camera.ViewMatrix();
                    effect.Projection = Camera.Projection;
                }

                mesh.Draw();
            }

        }



        var viewFrustum = new BoundingFrustum(Camera.ViewMatrix() * Camera.Projection);


        foreach (var entity in Scene.GetEntities<CHeightmap>()) {
            var heightmap = entity.GetComponent<CHeightmap>();

            if (!viewFrustum.Intersects(heightmap.BoundingBox)) {
                continue;
                // outside camera view
            }

            Game1.Inst.GraphicsDevice.SetVertexBuffer(heightmap.VertexBuffer);
			Game1.Inst.GraphicsDevice.Indices = heightmap.IndexBuffer;

            bEffect.EnableDefaultLighting();
            bEffect.LightingEnabled = true;
            bEffect.VertexColorEnabled = false;
            //bEffect.DiffuseColor = Color.Cyan.ToVector3();

            bEffect.World = heightmap.Transform;
            bEffect.View = Camera.ViewMatrix();
            bEffect.Projection = Camera.Projection;
            bEffect.TextureEnabled = true;
            bEffect.Texture = groundTexture;

            foreach (var pass in bEffect.CurrentTechnique.Passes) {
                pass.Apply();
                Game1.Inst.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, heightmap.NumTriangles);
            }
        }
    }

    /// <summary>Performs initialization logic.</summary>
    public override void Init() {
        // Create a default camera.
        Camera = new LookAtCamera {
            //Position = new Vector3(-24, 18, 16)
        };
    }
}

}
