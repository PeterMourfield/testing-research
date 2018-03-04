using System;
using System.Collections.Generic;

namespace ContainerLibrary
{
    public class Container
    {
        public delegate object InstanceCreator(Container container);

        Dictionary<Type, InstanceCreator> registeredTypes = new Dictionary<Type, InstanceCreator>();

        public void Register(Type typeToRegister, InstanceCreator creatorDelegate)
        {
            registeredTypes.Add(typeToRegister, creatorDelegate);
        }

        public void Register<T>(InstanceCreator creatorDelegate)
        {
            Type registeredType = typeof(T);
            registeredTypes.Add(registeredType, creatorDelegate);
        }

        public object GetInstance(Type typeToGet)
        {
            return registeredTypes[typeToGet](this);
        }

        public T GetInstance<T>()
        {
            Type registeredType = typeof(T);
            return (T)registeredTypes[registeredType](this);
        }
    }
}
