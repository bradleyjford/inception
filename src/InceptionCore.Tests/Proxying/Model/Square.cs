using System;

namespace InceptionCore.Tests.Proxying.Model
{
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
            _width = width;
        }

        public virtual string Name
        {
            get { return "Square"; }
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
