namespace CG_A2.Utils {

/*--------------------------------------
 * USINGS
 *------------------------------------*/
using System.Collections.Generic;
using CG_A2.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BoxMesh {

    public VertexPositionNormalTexture[] Vertices { get; set; }
    public VertexBuffer VB { get; set; }

    public int[] Indices { get; set; }
    public IndexBuffer IB { get; set; }
    
    private static readonly Vector3 FRONT_TOP_LEFT     = new Vector3(-0.5f, 1, 0.5f);
    private static readonly Vector3 FRONT_TOP_RIGHT    = new Vector3(0.5f,  1, 0.5f);
    private static readonly Vector3 FRONT_BOTTOM_LEFT  = new Vector3(-0.5f, 0, 0.5f);
    private static readonly Vector3 FRONT_BOTTOM_RIGHT = new Vector3(0.5f,  0, 0.5f);
    private static readonly Vector3 BACK_TOP_LEFT      = new Vector3(-0.5f, 1, -0.5f);
    private static readonly Vector3 BACK_TOP_RIGHT     = new Vector3(0.5f,  1, -0.5f);
    private static readonly Vector3 BACK_BOTTOM_LEFT   = new Vector3(-0.5f, 0, -0.5f);
    private static readonly Vector3 BACK_BOTTOM_RIGHT  = new Vector3(0.5f,  0, -0.5f);

    private static readonly Vector3 RIGHT    = new Vector3( 1,  0,  0); // +X
    private static readonly Vector3 LEFT     = new Vector3(-1,  0,  0); // -X
    private static readonly Vector3 UP       = new Vector3( 0,  1,  0); // +Y
    private static readonly Vector3 DOWN     = new Vector3( 0, -1,  0); // -Y
    private static readonly Vector3 FORWARD  = new Vector3( 0,  0,  1); // +Z
    private static readonly Vector3 BACKWARD = new Vector3( 0,  0, -1); // -Z


    public BoxMesh() {

        SetUpVertices();
        SetUpVertexBuffer();
    
        SetUpIndices();
        SetUpIndexBuffer();

    }

    private void SetUpVertices(){
        List<VertexPositionNormalTexture> vertexList = new List<VertexPositionNormalTexture>(36);

        // Front face
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_LEFT,     FORWARD, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_RIGHT, FORWARD, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT,  FORWARD, new Vector2(1, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_LEFT,     FORWARD, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT,    FORWARD, new Vector2(0, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_RIGHT, FORWARD, new Vector2(0, 1)));

        // Top face
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT,      UP, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT,    UP, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_LEFT,     UP, new Vector2(1, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT,      UP, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_RIGHT,     UP, new Vector2(0, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT,    UP, new Vector2(0, 1)));

        // Right face
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT,    RIGHT, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT,  RIGHT, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_RIGHT, RIGHT, new Vector2(1, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT,    RIGHT, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_RIGHT,     RIGHT, new Vector2(0, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT,  RIGHT, new Vector2(0, 1)));

        // Bottom face
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT,  DOWN, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT,  DOWN, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_LEFT,   DOWN, new Vector2(1, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT,  DOWN, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_RIGHT, DOWN, new Vector2(0, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT,  DOWN, new Vector2(0, 1)));

        // Left face
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT,      LEFT, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT,  LEFT, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_LEFT,   LEFT, new Vector2(1, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT,      LEFT, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_LEFT,     LEFT, new Vector2(0, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT,  LEFT, new Vector2(0, 1)));

        // Back face
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_RIGHT,     BACKWARD, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_LEFT,   BACKWARD, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT,  BACKWARD, new Vector2(1, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_RIGHT,     BACKWARD, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT,      BACKWARD, new Vector2(0, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_LEFT,   BACKWARD, new Vector2(0, 1)));

        Vertices = vertexList.ToArray(); 
    }

    private void SetUpVertexBuffer(){
        VB = new VertexBuffer(Game1.Inst.GraphicsDevice, typeof(VertexPositionNormalTexture), 
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
