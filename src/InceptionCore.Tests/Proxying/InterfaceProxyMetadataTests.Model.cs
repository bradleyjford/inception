using System;

namespace InceptionCore.Tests.Proxying
{
    partial class InterfaceProxyMetadataTests
    {
        private interface IShape
        {
            event EventHandler ShapeChanged;

            string Name { get; }

            long CalculateArea();
        }
    }
}
