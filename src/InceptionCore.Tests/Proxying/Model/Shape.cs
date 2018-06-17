using System;

namespace InceptionCore.Tests.Proxying.Model
{
    public abstract class Shape : IShape
    {
        public event EventHandler ShapeChanged;

        public abstract string Name { get; }

        public abstract long CalculateArea();
    }
}
