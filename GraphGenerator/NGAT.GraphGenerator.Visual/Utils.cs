using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NGAT.GraphGenerator.Visual
{
    public class Utils
    {
        
        public static IEnumerable<T> GetSubClasses<T>(params object[] ctorArgs)
        {
            Type baseType = typeof(T);
            foreach (var dll in Directory.EnumerateFiles(@"..\net6.0-windows").Where(f => f.EndsWith("dll") && !f.EndsWith("Excel.dll") && !f.EndsWith("OsmSharp.dll")))
            {
                Assembly assembly = Assembly.LoadFile(Path.GetFullPath(dll));
                foreach (var module in assembly.GetModules())
                {
                    // Types inherinting from T
                    IEnumerable<Type> types = module.GetTypes().Where(t => (t.GetInterfaces().Contains(baseType) || t.IsSubclassOf(baseType)) && !(t.IsInterface || t.IsAbstract));

                    foreach (var type in types)
                    {
                        Type[] argsTypes = new Type[ctorArgs.Length];
                        for (int i = 0; i < ctorArgs.Length; i++)
                            argsTypes[i] = ctorArgs[i].GetType();

                        //Type[] argsTypes = ctorArgs.Select(arg => arg.GetType()).ToArray(); // another way, but slowest

                        var ctor = type.GetConstructor(argsTypes);
                        if (ctor != null)
                            yield return (T)ctor.Invoke(ctorArgs);
                    }
                }
            }
        }
    }
}
