namespace CG_A2.Core {

/*--------------------------------------
 * USINGS
 *------------------------------------*/

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Core;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Represents a game instance. This class is a singleton
///          type.</summary>
public class Game1: Game {
    /*--------------------------------------
     * PRIVATE FIELDS
     *------------------------------------*/

    private Scene m_InitialScene;

    /// <summary>The scene that the game is currently displaying.</summary>
    private Scene m_Scene;

    /*--------------------------------------
     * PUBLIC PROPERTIES
     *------------------------------------*/

    /// <summary>Gets the <see cref="GraphicsDeviceManager"/> associated with
    ///          the game instance.</summary>
    public GraphicsDeviceManager Graphics { get; }

    /// <summary>Gets the <see cref="Game1"/> singleton instance.</summary>
    public static Game1 Inst { get; private set; }

    /*--------------------------------------
     * CONSTRUCTORS
     *------------------------------------*/

    /// <summary>Initializes a new instance of the <see cref="Game1"/>
    //           class.</summary>
    public Game1() {
        // Technically, we have a race condition here, but that's ok. ;-)
        if (Inst != null) {
            var s = $"Only a single {nameof (Game1)} instance is allowed.";
            throw new System.InvalidOperationException(s);
        }

        Inst     = this;
        Graphics = new GraphicsDeviceManager(this);

        Graphics.PreparingDeviceSettings += (sender, e) => {
            e.GraphicsDeviceInformation.GraphicsProfile = GraphicsProfile.HiDef;
        };
    }

    /*--------------------------------------
     * PUBLIC METHODS
     *------------------------------------*/

    /// <summary>Enters the specified scene.</summary>
    /// <param name="scene">The scene to enter.<param>
    public void EnterScene(Scene scene) {
        scene.ParentScene = m_Scene;
        m_Scene = scene;
        m_Scene.Init();
    }

    /// <summary>Leaves the current scene.</summary>
    public void LeaveScene() {
        if (m_Scene != null) {
            var scene = m_Scene;
            m_Scene = m_Scene.ParentScene;
            scene.Cleanup();
        }
    }

    /// <summary>Runs the game using the specified scene.</summary>
    /// <param name="scene">The scene to display.<param>
    public void Run(Scene scene) {
        m_InitialScene = scene;

        Run();
    }

    /*--------------------------------------
     * PROTECTED METHODS
     *------------------------------------*/

    /// <summary>Performs draw logic by telling the current scene to perform
    ///          any draw related operations.</summary>
    /// <param name="gameTime">The game time.</param>
    protected override void Draw(GameTime gameTime) {
        if (m_Scene != null) {
            var t  = (float)gameTime.TotalGameTime  .TotalSeconds;
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            m_Scene.Draw(t, dt);
        }

        base.Draw(gameTime);
    }

    /// <summary>Performs initialization logic.</summary>
    protected override void Initialize() {
        base.Initialize();

        Content.RootDirectory = "Content";
        IsMouseVisible        = true;
        Window.Title          = "Computer Graphics - Assignment 1";

        Graphics.PreferredBackBufferWidth  = 1920;
        Graphics.PreferredBackBufferHeight = 1080;
        Graphics.ApplyChanges();
    }

    /// <summary>Performs update logic by telling the current scene to perform
    ///          any update related operations.</summary>
    /// <param name="gameTime">The game time.</param>
    protected override void Update(GameTime gameTime) {
        // TODO: Fix this ugly crap.
        if (m_InitialScene != null) {
            EnterScene(m_InitialScene);
            m_InitialScene = null;
        }

        if (m_Scene != null) {
            var t  = (float)gameTime.TotalGameTime  .TotalSeconds;
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            m_Scene.Update(t, dt);
        }

        base.Update(gameTime);

        if(Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
    }
}

}
