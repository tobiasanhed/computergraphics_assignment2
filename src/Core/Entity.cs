namespace CG_A2.Core {

/*--------------------------------------
 * USINGS
 *------------------------------------*/

using System;
using System.Collections.Generic;
using System.Threading;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Represents a single entity.</summary>
public sealed class Entity {
    /*--------------------------------------
     * PRIVATE FIELDS
     *------------------------------------*/

    /// <summary>The look-up table for components attached to the
    ///          entity.</summary>
    private readonly Dictionary<Type, Component> m_Components =
        new Dictionary<Type, Component>();

    /// <summary>Static ID counter for entities.</summary>
    private static int s_NextID = 1;

    /*--------------------------------------
     * PUBLIC PROPERTIES
     *------------------------------------*/

    /// <summary>Gets the entity ID.</summary>
    public int ID { get; }

    /// <summary>Gets or sets the scene that the entity is in.</summary>
    public Scene Scene { get; set; }

    /*--------------------------------------
     * CONSTRUCTOR
     *------------------------------------*/

    /// <summary>Creates a new <see cref="Entity"/> instance.</summary>
    public Entity() {
        // Well, what do you know! No race condition here! :-P
        ID = Interlocked.Increment(ref s_NextID);
    }

    /*--------------------------------------
     * PUBLIC METHODS
     *------------------------------------*/

    /// <summary>Adds the specified component to the entity.</summary>
    /// <param name="component">The component to add to the entity.</param>
    public void AddComponent<T>(T component) where T : Component {
        m_Components.Add(typeof (T), component);
    }

    /// <summary>Adds all specified components to the entity.</summary>
    /// <param name="components">The components to add to the entity.</param>
    public void AddComponents(params Component[] components) {
        foreach (var component in components) {
            m_Components.Add(component.GetType(), component);
        }
    }

    /// <summary>Destroys the entity by removing it from the scene.</summary>
    public void Destroy() {
        if (Scene != null) {
            Scene.RemoveEntity(this);
        }
    }

    /// <summary>Gets the entity component of the specified type.</summary>
    public T GetComponent<T>() where T : Component {
        try {
            return (T)m_Components[typeof (T)];
        }catch{
            return null;
        }
    }

    /// <summary>Checks whether the entity has a component of the specified
    ///          type.</summary>
    /// <returns><see langword="true"/> if the entity has a component of the
    ///          specified type.</returns>
    public bool HasComponent<T>() where T : Component {
        return m_Components.ContainsKey(typeof (T));
    }
}

}
