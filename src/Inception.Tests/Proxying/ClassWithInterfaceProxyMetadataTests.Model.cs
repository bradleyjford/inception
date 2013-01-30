using System;

namespace Inception.Tests.Proxying
{
    partial class ClassWithInterfaceProxyMetadataTests
    {
        private interface IShape
        {
            event EventHandler ShapeChanged;
            string Name { get; }
            long CalculatePerimeter();
        }

        private class Square 
        {
            public virtual event EventHandler WidthChanged;

            private int _width;

            public Square()
            {

            }

            public Square(int width)
            {
                _width = width;
            }

            public virtual int Width
            {
                get { return _width; }

                set
                {
                    if (_width != value)
                    {
                        _width = value;

                        OnWidthChanged();
                    }
                }
            }

            protected virtual void OnWidthChanged()
            {
                RaiseWidthChanged();
            }

            protected void RaiseWidthChanged()
            {
                var handler = WidthChanged;

                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }

            public virtual long CalculateArea()
            {
                return Width * Width;
            }
        }
    }
}
