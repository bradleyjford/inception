using System;

namespace InceptionCore.Tests.Proxying.Model
{
    public interface IShape
    {
        event EventHandler ShapeChanged;
        string Name { get; }
        long CalculateArea();
    }
}
