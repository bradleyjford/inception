using System;

namespace Inception.Tests.Proxying
{
    partial class ProxyConstructorSelectorTests
    {
        public interface ISquare
        {
            
        }

        public class Square : ISquare
        {
            public Square()
            {
                
            }

            public Square(int width)
            {
                
            }
        }

        public abstract class Shape
        {
            
        }

        public class Circle : Shape
        {
            
        }

        public class ConstructorSelectorTestModel
        {
            public ConstructorSelectorTestModel()
            {

            }

            public ConstructorSelectorTestModel(ISquare square)
            {

            }

            public ConstructorSelectorTestModel(Shape shape)
            {

            }
        }
    }
}
