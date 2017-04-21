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
    // TODO: Skapa dom utifrån 0.0 för att få rätt origo
    private static readonly Vector3 FRONT_TOP_LEFT     = new Vector3(-0.5f, 0.5f, 0.5f);
    private static readonly Vector3 FRONT_TOP_RIGHT    = new Vector3(0.5f, 0.5f, 0.5f);
    private static readonly Vector3 FRONT_BOTTOM_LEFT  = new Vector3(-0.5f, -0.5f, 0.5f);
    private static readonly Vector3 FRONT_BOTTOM_RIGHT = new Vector3(0.5f, -0.5f, 0.5f);
    private static readonly Vector3 BACK_TOP_LEFT      = new Vector3(-0.5f, 0.5f, -0.5f);
    private static readonly Vector3 BACK_TOP_RIGHT     = new Vector3(0.5f, 0.5f, -0.5f);
    private static readonly Vector3 BACK_BOTTOM_LEFT   = new Vector3(-0.5f, -0.5f, -0.5f);
    private static readonly Vector3 BACK_BOTTOM_RIGHT  = new Vector3(0.5f, -0.5f, -0.5f);

    private Color color;

    public BoxMesh(Color color) {
        this.color = color;

        SetUpVertices();
        SetUpVertexBuffer();
    
        SetUpIndices();
        SetUpIndexBuffer();

    }

    private void SetUpVertices(){
        List<VertexPositionColor> vertexList = new List<VertexPositionColor>(36);

        // Front face
        vertexList.Add(new VertexPositionColor(FRONT_TOP_LEFT,     color));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_RIGHT, color));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT,  color));
        vertexList.Add(new VertexPositionColor(FRONT_TOP_LEFT,     color));
        vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT,    color));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_RIGHT, color));

        // Top facen
        vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT,      color));
        vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT,    color));
        vertexList.Add(new VertexPositionColor(FRONT_TOP_LEFT,     color));
        vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT,      color));
        vertexList.Add(new VertexPositionColor(BACK_TOP_RIGHT,     color));
        vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT,    color));

        // Right face
        vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT,    color));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT,  color));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_RIGHT, color));
        vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT,    color));
        vertexList.Add(new VertexPositionColor(BACK_TOP_RIGHT,     color));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT,  color));

        // Bottom face
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT,  color));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT,  color));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_LEFT,   color));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT,  color));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_RIGHT, color));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT,  color));

        // Left face
        vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT,      color));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT,  color));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_LEFT,   color));
        vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT,      color));
        vertexList.Add(new VertexPositionColor(FRONT_TOP_LEFT,     color));
        vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT,  color));

        // Back face
        vertexList.Add(new VertexPositionColor(BACK_TOP_RIGHT,     color));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_LEFT,   color));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT,  color));
        vertexList.Add(new VertexPositionColor(BACK_TOP_RIGHT,     color));
        vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT,      color));
        vertexList.Add(new VertexPositionColor(BACK_BOTTOM_LEFT,   color));

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
