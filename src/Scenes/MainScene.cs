namespace CG_A2.Scenes {

/*--------------------------------------
 * USINGS
 *------------------------------------*/

using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Components;
using Components.Input;
using Core;
using Subsystems;

using static System.Math;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

public class MainScene: Scene {
    /*--------------------------------------
     * PUBLIC METHODS
     *------------------------------------*/

    /// <summary>Performs initialization logic.</summary>
    public override void Init() {
    	AddSubsystems(new      BodySubsystem(),
                      new  ControlsSubsystem(),
                      new     InputSubsystem(),
                      new     LogicSubsystem(),
                      new RenderingSubsystem());

        //var chopper = new Entity();

        //var model = Game1.Inst.Content.Load<Model>("Models/Chopper");

        // TODO: This is crap.
        /*CModel modmod;
        var cntrls = new CControls { };
        var controls = cntrls.Controls;
        chopper.AddComponents(
            new CBody { Velocity = new Vector3(0f, 0f, 1.0f) },
            modmod = new CModel {
                Model = model
            },

            cntrls,

            new CInput {
                KeyMap = {
                    { Keys.Up, () => {
                          controls["Up"] = 1.0f;
                      } },
                    { Keys.Down, () => {
                          controls["Up"] = -1.0f;
                      } },
                    { Keys.Left, () => {
                          controls["Turn"] = -1.0f;
                      } },
                    { Keys.Right, () => {
                          controls["Turn"] = 1.0f;
                      } }
                },
                ResetControls = () => {
                    controls["Up"] = 0.0f;
                    controls["Turn"] = 0.0f;
                }
            },

            new CLogic {
                UpdateFunc = (t, dt) => {
                    // TODO: Spin propellah
                    //System.Console.WriteLine("LOL", t);
                }
            });

        AddEntity(chopper);*/

        LoadHeightmap();

    	base.Init();
    }

    private VertexPositionNormalTexture CreateHeightmapVertex(Color[] pixels, int width, int height, int i, int j) {
        var index = i + j*width;
        var color = pixels[index];

        var x = (float)i / (float)width  - 0.5f;
        var y = (float)j / (float)height - 0.5f;
        var z = color.R / 255.0f         - 0.5f + 0.7f;

        //System.Console.WriteLine("{0}, {1} {2}", x, y, z);

        var ss = 9.0f;
        return new VertexPositionNormalTexture {
            Position = new Vector3(ss*20.0f*x, ss*-4.0f*z, ss*20.0f*y),
            //Position = new Vector3(ss*20.0f*x, 100.0f*x*x, ss*20.0f*y),
            Normal = Vector3.Zero,
            TextureCoordinate = new Vector2(i * 0.005f, j * 0.005f) // TODO: Det här är nog inte bästa lösningen, hehe
        };
    }

    private void LoadHeightmap() {
        var heightmap = Game1.Inst.Content.Load<Texture2D>("Textures/US_Canyon");
        var indices   = new List<int>();
        var pixels    = new Color[heightmap.Width*heightmap.Height];
        var vertices  = new List<VertexPositionNormalTexture>();

        // Read heightmap color data into the pixels array.
        heightmap.GetData<Color>(pixels);


        // Create two 1D-arrays containing all vertices and indices,
        // respectively.

        var q = 2;
        for (var j = 0; j < heightmap.Height/q; j++) {
            for (var i = 0; i < heightmap.Width/q; i++) {
                // skapa triangel med index 0, 1, 2
                var v0 = CreateHeightmapVertex(pixels, heightmap.Width, heightmap.Height, i*q, j*q);
                vertices.Add(v0);
            }
        }

        for (var j = 0; j < heightmap.Height/q-1; j++) {
            for (var i = 0; i < heightmap.Width/q-1; i++) {
                var a = i+j*(heightmap.Width/q);
                var b = (i+1)+j*(heightmap.Width/q);
                var c = (i+1)+(j+1)*(heightmap.Width/q);
                var d = i+(j+1)*(heightmap.Width/q);

                indices.Add(a);
                indices.Add(b);
                indices.Add(c);
                indices.Add(a);
                indices.Add(c);
                indices.Add(d);

                //System.Console.WriteLine(a + ", " + b + ", " + c + ", " + d + " - gammalt");
                //System.Threading.Thread.Sleep(1000);
            }
        }

        // Compute smooth normals.

        var v = vertices.ToArray();

        for(int i = 0; i < indices.Count - 2; i += 3){
            var a = indices[i];
            var b = indices[i+1];
            var c = indices[i+2];
            var N = Vector3.Cross(v[b].Position - v[a].Position, v[c].Position - v[a].Position);

            //N.Normalize();

            v[a].Normal -= N;
            v[b].Normal -= N;
            v[c].Normal -= N;
        }

        for(int i = 0; i < vertices.Count; i++) {
            v[i].Normal.Normalize();
        }

        // sqrt of number of meshes to create lol
        var num = 16;

        // aliases
        var g = Game1.Inst.GraphicsDevice;

        // HAVE FUN DEBUGGING THIS ROFL!!! H4XX0RZ $UPR3M3!!!!1
        for(var j=0;j<num;j++){for(var i=0;i<num;i++){var loli=(i==num-1)?1:0;
        var il=new List<int>();var ww=heightmap.Width/q;var a0=0;var baseidx=i*
        heightmap.Width/(num*q)+j*heightmap.Height/(num*q)*(heightmap.Width/q);
        var lolj=(j==num-1)?1:0;var vl=new List<VertexPositionNormalTexture>();
        for(var b=0;b<=heightmap.Height/(num*q)-lolj;b++){for(var a=0;a<=
        heightmap.Width/(num*q)-loli;a++){var i0=a+b*(ww);var i1=(a+1)+b*(ww);
        var i2=(a+1)+(b+1)*(ww);var i3=a+(b+1)*(ww);a0+=4;il.Add(a0);vl.Add(v[
        baseidx+i0]);il.Add(a0+1);vl.Add(v[baseidx+i1]); il.Add(a0+2);il.Add(a0)
        ;vl.Add(v[baseidx+i2]);il.Add(a0+2);il.Add(a0+3);vl.Add(v[baseidx+i3]);}
        }

                var vbo = new VertexBuffer(g, typeof (VertexPositionNormalTexture), vl.Count, BufferUsage.WriteOnly);
                var ibo = new  IndexBuffer(g, typeof (int    /* heylo lol */     ), il.Count, BufferUsage.WriteOnly);

                vbo.SetData<VertexPositionNormalTexture>(vl.ToArray());
                ibo.SetData   /* align tics */          (il.ToArray());


                var minP = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
                var maxP = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);


                // absolutely retarded way of finding min and max vertices (in reality, we already know the squares of each segments lol)
                foreach (var p in vl) {
                    minP.X = (float)Min(minP.X, p.Position.X);
                    minP.Y = (float)Min(minP.Y, p.Position.Y);
                    minP.Z = (float)Min(minP.Z, p.Position.Z);
                    maxP.X = (float)Max(maxP.X, p.Position.X);
                    maxP.Y = (float)Max(maxP.Y, p.Position.Y);
                    maxP.Z = (float)Max(maxP.Z, p.Position.Z);
                }

                //System.Console.WriteLine(vl.Count + ", " + il.Count);
                var cmp = new CHeightmap {
                    VertexBuffer = vbo,
                    IndexBuffer = ibo,
                    NumVertices = vl.Count,
                    NumTriangles = il.Count/3,
                    BoundingBox = new BoundingBox(minP, maxP)
                };

                var e = new Entity();
                e.AddComponents(cmp);

                AddEntity(e);
            }
        }
    }
}

}
