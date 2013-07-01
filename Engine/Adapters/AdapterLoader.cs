using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HomeIntegrationPlatform.Engine.Adapters
{
    class AdapterLoader
    {
        private Dictionary<String, Type> adapters = new Dictionary<string, Type>();

        public void LoadAdapters(String path)
        {
            Assembly assembly = Assembly.LoadFrom(path);

            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsAbstract && type.IsSubclassOf(typeof(AdapterBase)))
                {
                    adapters.Add(type.FullName, type);
                    Console.WriteLine(type.FullName);
                }
            }
        }

        public AdapterBase GetAdapter(String fullName, Settings settings)
        {
            if (!adapters.ContainsKey(fullName))
            {
                throw new ArgumentException(fullName + " not loaded", "fullname");
            }

            Type t = adapters[fullName];
            ConstructorInfo[] constructors = t.GetConstructors();

            foreach (ConstructorInfo info in constructors)
            {
                if (info.GetParameters().Length == 1 && info.GetParameters()[0].ParameterType == typeof(Settings))
                {
                    return (AdapterBase) info.Invoke(new object[] { settings });
                }
            }
            foreach (ConstructorInfo info in constructors)
            {
                if (info.GetParameters().Length == 0)
                {
                    return (AdapterBase)info.Invoke(new object[0]);
                }
            }

            throw new InvalidOperationException(fullName + " doesn't have an acceptable constructor");
        }
    }
}
