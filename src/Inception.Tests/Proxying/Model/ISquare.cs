using System;

namespace Inception.Tests.Proxying.Model
{
    public interface ISquare
    {
        event EventHandler WidthChanged;
        int Width { get; set; }
        long CalculateArea();
    }
}