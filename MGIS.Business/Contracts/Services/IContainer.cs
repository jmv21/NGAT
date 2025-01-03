using System;

namespace NGAT.Business.Contracts.Services
{
    /// <summary>
    /// Represents an interface for implementation of Container Pattern.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Records an especific implementation for a given type.
        /// </summary>
        /// <typeparam name="T">The type to be implemented. Must be an Interface or an Abstract class.</typeparam>
        /// <param name="implementation">The especific implementation of type T.</param>
        void Register<T>(Type implementation);

        /// <summary>
        /// Resolves an implementation for type T.
        /// </summary>
        /// <typeparam name="T">The type to be resolved.</typeparam>
        /// <returns>An object of type T.</returns>
        T Resolve<T>();

    }
}
