namespace CG_A2.Utils {

/*--------------------------------------
 * USINGS
 *------------------------------------*/
using System.Collections.Generic;
using CG_A2.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BoxMesh {

    public VertexPositionColor[] Vertices { get; set; }
    public VertexBuffer VB { get; set; }

    public int[] Indices { get; set; }
    public IndexBuffer IB { get; set; }

    private static readonly Vector3 FRONT_TOP_LEFT     = new Vector3(-0.5f, 0.5f, 0.5f);
    private static readonly Vector3 FRONT_TOP_RIGHT    = new Vector3(0.5f, 0.5f, 0.5f);
    private static readonly Vector3 FRONT_BOTTOM_LEFT  = new Vector3(-0.5f, -0.5f, 0.5f);
    private static readonly Vector3 FRONT_BOTTOM_RIGHT = new Vector3(0.5f, -0.5f, 0.5f);
    private static readonly Vector3 BACK_TOP_LEFT      = new Vector3(-0.5f, 0.5f, -0.5f);
    private static readonly Vector3 BACK_TOP_RIGHT     = new Vector3(0.5f, 0.5f, -0.5f);
    private static readonly Vector3 BACK_BOTTOM_LEFT   = new Vector3(-0.5f, -0.5f, -0.5f);
    private static readonly Vector3 BACK_BOTTOM_RIGHT  = new Vector3(0.5f, -0.5f, -0.5f);

    /*private static readonly Texture2D TEXTURE_1      = Game1.Inst.Content.Load<Texture2D>("Textures/skybox1"); 
    private static readonly Texture2D TEXTURE_2      = Game1.Inst.Content.Load<Texture2D>("Textures/skybox2"); 
    private static readonly Texture2D TEXTURE_3      = Game1.Inst.Content.Load<Texture2D>("Textures/skybox3"); 
    private static readonly Texture2D TEXTURE_4      = Game1.Inst.Content.Load<Texture2D>("Textures/skybox4"); 
    private static readonly Texture2D TEXTURE_TOP    = Game1.Inst.Content.Load<Texture2D>("Textures/skyboxtop"); 
    private static readonly Texture2D TEXTURE_BOTTOM = Game1.Inst.Content.Load<Texture2D>("Textures/skyboxbottom");
*/
    // Normals
    /*private static readonly Vector3 RIGHT    = new Vector3( 1,  0,  0); // +X
    private static readonly Vector3 LEFT     = new Vector3(-1,  0,  0); // -X
    private static readonly Vector3 UP       = new Vector3( 0,  1,  0); // +Y
    private static readonly Vector3 DOWN     = new Vector3( 0, -1,  0); // -Y
    private static readonly Vector3 FORWARD  = new Vector3( 0,  0,  1); // +Z
    private static readonly Vector3 BACKWARD = new Vector3( 0,  0, -1); // -Z
*/


    public BoxMesh() {

        SetUpVertices();
        SetUpVertexBuffer();
    
        SetUpIndices();
        SetUpIndexBuffer();

    }

    private void SetUpVertices(){
        List<VertexPositionColor> vertexList = new List<VertexPositionColor>(36);

        // Front face
        vertexList.Add(new VertexPositionColor(FRONT_TOP_LEFT,     Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_RIGHT, Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT,  Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_TOP_LEFT,     Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT,    Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_RIGHT, Color.Black));

        // Top facen
        vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT,      Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT,    Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_TOP_LEFT,     Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT,      Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_TOP_RIGHT,     Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT,    Color.Black));

        // Right face
        vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT,    Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT,  Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_RIGHT, Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT,    Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_TOP_RIGHT,     Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT,  Color.Black));

        // Bottom face
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT,  Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT,  Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_LEFT,   Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT,  Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_RIGHT, Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT,  Color.Black));

        // Left face
        vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT,      Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT,  Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_LEFT,   Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT,      Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_TOP_LEFT,     Color.Black));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT,  Color.Black));

        // Back face
        vertexList.Add(new VertexPositionColor(BACK_TOP_RIGHT,     Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_LEFT,   Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT,  Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_TOP_RIGHT,     Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT,      Color.Black));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_LEFT,   Color.Black));

        Vertices = vertexList.ToArray(); 
    }

    private void SetUpVertexBuffer(){
        VB = new VertexBuffer(Game1.Inst.GraphicsDevice, typeof(VertexPositionColor), 
            Vertices.Length, BufferUsage.None);
        VB.SetData(Vertices);        
    }
    
    private void SetUpIndices(){
        List<int> indexList = new List<int>(36);
        
        for(int i = 0; i < 36; ++i){
            indexList.Add(i);
        }

        Indices = indexList.ToArray();
    }

    private void SetUpIndexBuffer(){
        IB = new IndexBuffer(Game1.Inst.GraphicsDevice, typeof(int), Indices.Length,
            BufferUsage.None);
        IB.SetData(Indices);
    }

}
}
