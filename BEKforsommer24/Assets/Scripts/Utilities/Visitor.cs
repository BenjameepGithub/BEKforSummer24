using UnityEngine;

namespace Utilities {
    // NB! This visitor executes a dynamic method. The unity "Api Compatibility Level" in "Project Settings" needs to be set to ".NET Framework".
    public interface IVisitor {
    }
    
    /// <summary>
    /// Defines a class as being able to visit IVisitable objects. Add additional "Visit" methods to inherited classes
    /// that are IVisitable by the Visitor
    /// </summary>
    /// <typeparam name="TVisitable">Type of the IVisitable interface</typeparam>
    public abstract class Visitor<TVisitable> : IVisitor where TVisitable : IVisitable {
        public void Visit(TVisitable interactiveObject) {
            Visit(interactiveObject as dynamic);
        }
        
        private static void Visit(dynamic visitable) {
            Debug.LogWarning($"Couldn't visit type: {visitable.GetType().Name}");
        }
    }
    
    public interface IVisitable {
    }
    
    /// <summary>
    /// Defines an interface that a Visitor can to visit.
    /// </summary>
    /// <typeparam name="TVisitable">Should be the interface that implements this interface</typeparam>
    /// <typeparam name="TVisitor">Visitor Type</typeparam>
    public interface IVisitable<TVisitable, in TVisitor> : IVisitable where TVisitor : Visitor<TVisitable> where TVisitable : IVisitable {
        public void Accept(TVisitor visitor) {
            visitor.Visit(this as dynamic);
        }
    }
    
    /// <summary>
    /// Represents an object that can interact with other interactive objects.
    /// </summary>
    /// <Notes>
    /// Implementing classes must provide a constructor that initializes the Visitor property.
    /// </Notes>
    public interface IVisitorOf<out T> where T : IVisitor {
        T Visitor { get; }
    }
}