using System;
using System.Collections.Generic;

namespace Inception.InversionOfControl
{
    public interface IContainer : IDisposable
    {
        IContainer ParentContainer { get; }

        T GetInstance<T>() where T : class;
        T GetInstance<T>(string name) where T : class;

        //T TryGetInstance<T>() where T : class;
        //object TryGetInstance(Type type);
        object GetInstance(Type type);
        object GetInstance(Type type, string name);

        IEnumerable<T> GetAllInstances<T>();
        IEnumerable<object> GetAllInstances(Type type);
    }
}
