    namespace CG_A2.Core {
    using System.Collections.Generic;

    /*--------------------------------------
     * USINGS
     *------------------------------------*/

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /*--------------------------------------
     * CLASSES
     *------------------------------------*/

    /// <summary>Represents a camera.</summary>
    public class SkyBox {
    /*--------------------------------------
     * PUBLIC PROPERTIES
     *------------------------------------*/


    private Vector3 scale;
    private Vector3 position;
    private Vector3 rotation = Vector3.Zero;
    private Matrix objectWorld;

    private VertexPositionNormalTexture[] vertices;
    private VertexBuffer vb;

    private int[] indices;
    private IndexBuffer ib;

    private static readonly Vector3 FRONT_TOP_LEFT     = new Vector3(-0.5f, 0.5f, 0.5f);
    private static readonly Vector3 FRONT_TOP_RIGHT    = new Vector3(0.5f, 0.5f, 0.5f);
    private static readonly Vector3 FRONT_BOTTOM_LEFT  = new Vector3(-0.5f, -0.5f, 0.5f);
    private static readonly Vector3 FRONT_BOTTOM_RIGHT = new Vector3(0.5f, -0.5f, 0.5f);
    private static readonly Vector3 BACK_TOP_LEFT      = new Vector3(-0.5f, 0.5f, -0.5f);
    private static readonly Vector3 BACK_TOP_RIGHT     = new Vector3(0.5f, 0.5f, -0.5f);
    private static readonly Vector3 BACK_BOTTOM_LEFT   = new Vector3(-0.5f, -0.5f, -0.5f);
    private static readonly Vector3 BACK_BOTTOM_RIGHT  = new Vector3(0.5f, -0.5f, -0.5f);

    private static readonly Texture2D TEXTURE_1      = Game1.Inst.Content.Load<Texture2D>("Textures/skybox1"); 
    private static readonly Texture2D TEXTURE_2      = Game1.Inst.Content.Load<Texture2D>("Textures/skybox2"); 
    private static readonly Texture2D TEXTURE_3      = Game1.Inst.Content.Load<Texture2D>("Textures/skybox3"); 
    private static readonly Texture2D TEXTURE_4      = Game1.Inst.Content.Load<Texture2D>("Textures/skybox4"); 
    private static readonly Texture2D TEXTURE_TOP    = Game1.Inst.Content.Load<Texture2D>("Textures/skyboxtop"); 
    private static readonly Texture2D TEXTURE_BOTTOM = Game1.Inst.Content.Load<Texture2D>("Textures/skyboxbottom");

    // Normals
    private static readonly Vector3 RIGHT    = new Vector3( 1,  0,  0); // +X
    private static readonly Vector3 LEFT     = new Vector3(-1,  0,  0); // -X
    private static readonly Vector3 UP       = new Vector3( 0,  1,  0); // +Y
    private static readonly Vector3 DOWN     = new Vector3( 0, -1,  0); // -Y
    private static readonly Vector3 FORWARD  = new Vector3( 0,  0,  1); // +Z
    private static readonly Vector3 BACKWARD = new Vector3( 0,  0, -1); // -Z
    /*--------------------------------------
     * PUBLIC METHODS
     *------------------------------------*/

    public SkyBox(Vector3 scale, Vector3 position){
        
        this.scale = scale;
        this.position = position;

        SetUpVertices();
        SetUpVertexBuffer();

        SetUpIndices();
        SetUpIndexBuffer();
                
    }

    private void SetUpVertices(){
        List<VertexPositionNormalTexture> vertexList = new List<VertexPositionNormalTexture>(36);

        // Front face
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_LEFT,     BACKWARD, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT,  BACKWARD, new Vector2(1, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_RIGHT, BACKWARD, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_LEFT,     BACKWARD, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_RIGHT, BACKWARD, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT,    BACKWARD, new Vector2(0, 0)));

        // Top face
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT,      DOWN, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_LEFT,     DOWN, new Vector2(1, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT,    DOWN, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT,      DOWN, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT,    DOWN, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_RIGHT,     DOWN, new Vector2(0, 0)));

        // Right face
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT,    LEFT, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_RIGHT, LEFT, new Vector2(1, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT,  LEFT, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_RIGHT,    LEFT, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT,  LEFT, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_RIGHT,     LEFT, new Vector2(0, 0)));

        // Bottom face
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT,  UP, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_LEFT,   UP, new Vector2(1, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT,  UP, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT,  UP, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT,  UP, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_RIGHT, UP, new Vector2(0, 0)));

        // Left face
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT,      RIGHT, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_LEFT,   RIGHT, new Vector2(1, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT,  RIGHT, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT,      RIGHT, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_BOTTOM_LEFT,  RIGHT, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(FRONT_TOP_LEFT,     RIGHT, new Vector2(0, 0)));

        // Back face
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_RIGHT,     FORWARD, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_RIGHT,  FORWARD, new Vector2(1, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_LEFT,   FORWARD, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_RIGHT,     FORWARD, new Vector2(1, 0)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_BOTTOM_LEFT,   FORWARD, new Vector2(0, 1)));
        vertexList.Add(new VertexPositionNormalTexture(BACK_TOP_LEFT,      FORWARD, new Vector2(0, 0)));

        vertices = vertexList.ToArray(); 
    }

    private void SetUpVertexBuffer(){
        vb = new VertexBuffer(Game1.Inst.GraphicsDevice, typeof(VertexPositionNormalTexture), 
            vertices.Length, BufferUsage.None);
        vb.SetData(vertices);        
    }
    
    private void SetUpIndices(){
        List<int> indexList = new List<int>(36);
        
        for(int i = 0; i < 36; ++i){
            indexList.Add(i);
        }

        indices = indexList.ToArray();
    }

    private void SetUpIndexBuffer(){
        ib = new IndexBuffer(Game1.Inst.GraphicsDevice, typeof(int), indices.Length,
            BufferUsage.None);
        ib.SetData(indices);
    }
    
    public void Draw(float t, float dt, BasicEffect bEffect){
        // Stäng av z-axeln för en bättre effekt
        Game1.Inst.GraphicsDevice.DepthStencilState = new DepthStencilState { DepthBufferEnable = false };
        
        Game1.Inst.GraphicsDevice.SetVertexBuffer(vb);
        Game1.Inst.GraphicsDevice.Indices = ib;

        bEffect.World = bEffect.World * Matrix.CreateScale(scale) * Matrix.CreateTranslation(position);

        bEffect.TextureEnabled = true;
        bEffect.EnableDefaultLighting();
        bEffect.VertexColorEnabled = false;
        bEffect.LightingEnabled = true;

        bEffect.Texture = TEXTURE_1;
        foreach(EffectPass ep in bEffect.CurrentTechnique.Passes){
            ep.Apply();
            Game1.Inst.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                0, 0, 2);
        }

        bEffect.Texture = TEXTURE_TOP;
        foreach(EffectPass ep in bEffect.CurrentTechnique.Passes){
            ep.Apply();
            Game1.Inst.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                0, 6, 2);
        }

        bEffect.Texture = TEXTURE_4;
        foreach(EffectPass ep in bEffect.CurrentTechnique.Passes){
            ep.Apply();
            Game1.Inst.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                0, 12, 2);
        }

        bEffect.Texture = TEXTURE_BOTTOM;
        foreach(EffectPass ep in bEffect.CurrentTechnique.Passes){
            ep.Apply();
            Game1.Inst.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                0, 18, 2);
        }

        bEffect.Texture = TEXTURE_2;
        foreach(EffectPass ep in bEffect.CurrentTechnique.Passes){
            ep.Apply();
            Game1.Inst.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                0, 24, 2);
        }

        bEffect.Texture = TEXTURE_3;
        foreach(EffectPass ep in bEffect.CurrentTechnique.Passes){
            ep.Apply();
            Game1.Inst.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                0, 30, 2);
        }
        
        Game1.Inst.GraphicsDevice.DepthStencilState = new DepthStencilState { DepthBufferEnable = true };        
        
    }
}

}
