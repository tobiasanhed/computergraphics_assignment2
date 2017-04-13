namespace CG_A1.Core {

/*--------------------------------------
 * USINGS
 *------------------------------------*/

using System.Threading;
using System.Collections.Generic;

using Subsystems;
using Core;

/*--------------------------------------
 * CLASSES
 *------------------------------------*/

/// <summary>Represents a single scene.</summary>
public abstract class Scene {
    /*--------------------------------------
     * PRIVATE FIELDS
     *------------------------------------*/

    // Optimally, the m_Entities field should be a look-up table. It's ok to use
    // a list for this assignment since we don't expect a large number of
    // entities.
    /// <summary>The entities that are currently in the scene.</summary>
    private List<Entity> m_Entities = new List<Entity>();

    /// <summary>The subsystems currently used in the scene.</summary>
    private List<Subsystem> m_Subsystems = new List<Subsystem>();

    /*--------------------------------------
     * PUBLIC PROPERTIES
     *------------------------------------*/

    /// <summary>Gets or sets the parent scene.</summary>
    public Scene ParentScene { get; set; }

    /*--------------------------------------
     * PUBLIC METHODS
     *------------------------------------*/

    /// <summary>Adds the specified entity to the scene.</summary>
    /// <param name="entity">The entity to add to the scene.</param>
    public void AddEntity(Entity entity) {
        m_Entities.Add(entity);
        entity.Scene = this;
    }

    /// <summary>Adds the specified subsystem to the scene.</summary>
    /// <param name="subsystem">The subsystem to add to the scene.</param>
    public void AddSubsystem(Subsystem subsystem) {
        m_Subsystems.Add(subsystem);
        subsystem.Scene = this;
    }

    public void AddSubsystems(params Subsystem[] subsystems) {
        foreach (var subsystem in subsystems) {
            AddSubsystem(subsystem);
        }
    }

    /// <summary>Performs cleanup logic.</summary>
    public virtual void Cleanup() {
        foreach(var subsystem in m_Subsystems){
            subsystem.Cleanup();
        }
    }

    /// <summary>Performs scene-specific draw logic.</summary>
    /// <param name="t">The total game time, in seconds.</param>
    /// <param name="dt">The elapsed time since last call, in seconds.</param>
    public virtual void Draw(float t, float dt) {
        foreach (var subsystem in m_Subsystems) {
            subsystem.Draw(t, dt);
        }
    }

    /// <summary>Retrives a list of all entities containing the specified type
    ///          of component.</summary>
    public IEnumerable<Entity> GetEntities<T>() where T: Component {
        // TODO: This method is super slow, but who cares?

        var entities = new List<Entity>();

        foreach (var entity in m_Entities) {
            if (entity.HasComponent<T>()) {
                entities.Add(entity);
            }
        }

        return entities;
    }

    /// <summary>Performs initialization logic.</summary>
    public virtual void Init() {
        foreach(var subsystem in m_Subsystems){
            subsystem.Init();
        }
    }

    /// <summary>Removes the specified entity from the scene.</summary>
    /// <param name="entity">The entity to remove from the scene.</param>
    public void RemoveEntity(Entity entity) {
        m_Entities.Remove(entity);
        entity.Scene = null;
    }

    /// <summary>Performs scene-specific update logic.</summary>
    /// <param name="t">The total game time, in seconds.</param>
    /// <param name="dt">The elapsed time since last call, in seconds.</param>
    public virtual void Update(float t, float dt) {
        foreach (var subsystem in m_Subsystems) {
            subsystem.Update(t, dt);
        }
    }
}

}
