using System;

namespace Inception.Tests.Proxying.Model
{
	public interface IShape
	{
		event EventHandler ShapeChanged;
		string Name { get; }
		long CalculateArea();
	}
}
