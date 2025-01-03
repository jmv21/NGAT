using System;
using System.Collections.Generic;
using System.Linq;

namespace NGAT.Business.Contracts.Services
{
    /// <summary>
    /// Represents a generic implementation of the Container Pattern for dependency inyection.
    /// </summary>
    public class Container : IContainer
    {
        protected Dictionary<Type, Type> Dictionary { get; set; }

        public Container()
        {
            Dictionary = new Dictionary<Type, Type>();
        }

        /// <summary>
        /// Records an especific implementation for a given type.
        /// </summary>
        /// <typeparam name="T">The type to be implemented. Must be an Interface or an Abstract class.</typeparam>
        /// <param name="implementation">The especific implementation of type T.</param>
        public void Register<T>(Type implementation)
        {
            Type key = typeof(T);

            // some validations
            if (!key.IsAbstract && !key.IsInterface)
                throw new ArgumentException("The generic type cannot be an especific implementation.");
            else if (key.IsAbstract && !implementation.IsSubclassOf(key))
                throw new ArgumentException("The given implementation is not subclass of generic type.");
            else if (key.IsInterface && !implementation.GetInterfaces().Contains(key))
                throw new ArgumentException("The given implementation does not implement the specified interface.");

            // the facts
            if (!Dictionary.ContainsKey(key))
                Dictionary.Add(key, implementation);
            else
                Dictionary[key] = implementation;
        }

        /// <summary>
        /// Resolves an implementation for type T.
        /// </summary>
        /// <typeparam name="T">The type to be resolved.</typeparam>
        /// <returns>An object of type T.</returns>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        private object Resolve(Type key)
        {
            if (!Dictionary.ContainsKey(key))
                throw new InvalidOperationException("The specified type has not been registered yet.");

            foreach (var constructor in Dictionary[key].GetConstructors())
            {
                var parametersInfo = constructor.GetParameters();
                if (parametersInfo.Length == 0)
                    return constructor.Invoke(new object[] { });
                // esle
                object[] parameters = new object[parametersInfo.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    Type t = parametersInfo[i].ParameterType;
                    if (Dictionary.ContainsKey(t))
                        parameters[i] = Resolve(t);
                    else goto END;
                }
                return constructor.Invoke(parameters);
                END:; // keep looking for a constructor
            }
            throw new InvalidOperationException("It was imposible to resolve this implementation due to unknown dependencies.");
        }
    }

}
