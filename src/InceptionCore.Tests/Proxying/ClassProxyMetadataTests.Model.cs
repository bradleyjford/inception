using System;

namespace InceptionCore.Tests.Proxying
{
    partial class ClassProxyMetadataTests
    {
        private interface ISquare
        {
            event EventHandler WidthChanged;
            int Width { get; set; }
            long CalculateArea();
        }

        private class Square : ISquare
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
