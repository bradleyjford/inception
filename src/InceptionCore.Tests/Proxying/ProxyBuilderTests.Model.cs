using System;

namespace InceptionCore.Tests.Proxying
{
    partial class ProxyBuilderTests
    {
        public interface IShape
        {
            event EventHandler ShapeChanged;
            string Name { get; }
            long CalculateArea();
        }

        public interface ISquare
        {
            event EventHandler WidthChanged;
            int Width { get; set; }
            long CalculateArea();
        }

        public class Square : ISquare
        {
            public virtual event EventHandler WidthChanged;
            public virtual event EventHandler ShapeChanged;

            private int _width;

            public Square()
            {

            }

            public Square(int width)
            {
            }

            public virtual string Name
            {
                get { return "Square"; }
            }

            public virtual int Width { get; set; }

            protected virtual void OnWidthChanged()
            {
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
                return _width * _width;
            }
        }
    }
}
