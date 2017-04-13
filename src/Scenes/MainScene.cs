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

    private void LoadHeightmap() { // TODO: Dela upp heightmapen i olika meshes.
        var heightmap = Game1.Inst.Content.Load<Texture2D>("Textures/US_Canyon");

        var pixels = new Color[heightmap.Width*heightmap.Height];
        heightmap.GetData<Color>(pixels);

        var indices = new List<int>();
        var vertices = new List<VertexPositionNormalTexture>();

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
            }
        }

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

        VertexBuffer vb = new VertexBuffer(Game1.Inst.GraphicsDevice, typeof (VertexPositionNormalTexture), v.Length, BufferUsage.WriteOnly);
        vb.SetData<VertexPositionNormalTexture>(v);
        IndexBuffer ib = new IndexBuffer(Game1.Inst.GraphicsDevice, typeof (int), indices.Count, BufferUsage.WriteOnly);
        ib.SetData(indices.ToArray());

        heightmap.Dispose();

        var cmp = new CHeightmap {
            VertexBuffer = vb,
            IndexBuffer = ib,
            NumVertices = vertices.Count,
            NumTriangles = indices.Count
        };

        var e = new Entity();
        e.AddComponents(cmp);

        AddEntity(e);
    }
}

}
