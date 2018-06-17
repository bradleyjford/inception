using System;

namespace InceptionCore.Tests.Proxying.MethodInvocations
{
    public class RefParameterTestModel
    {
        public virtual void Int16RefParameter(ref short i)
        {
            if (i == Int16.MinValue)
            {
                i = Int16.MaxValue;
            }
        }

        public virtual void Int32RefParameter(ref int i)
        {
            if (i == Int32.MinValue)
            {
                i = Int32.MaxValue;
            }
        }

        public virtual void Int64RefParameter(ref long i)
        {
            if (i == Int64.MinValue)
            {
                i = Int64.MaxValue;
            }
        }

        public virtual void UInt16RefParameter(ref ushort i)
        {
            if (i == UInt16.MinValue)
            {
                i = UInt16.MaxValue;
            }
        }

        public virtual void UInt32RefParameter(ref uint i)
        {
            if (i == UInt32.MinValue)
            {
                i = UInt32.MaxValue;
            }
        }

        public virtual void UInt64RefParameter(ref ulong i)
        {
            if (i == UInt64.MinValue)
            {
                i = UInt64.MaxValue;
            }
        }

        public virtual void SingleRefParameter(ref float i)
        {
            if (i == Single.MinValue)
            {
                i = Single.MaxValue;
            }
        }

        public virtual void DoubleRefParameter(ref double i)
        {
            if (i == Double.MinValue)
            {
                i = Double.MaxValue;
            }
        }

        public virtual void DecimalRefParameter(ref decimal i)
        {
            if (i == Decimal.MinValue)
            {
                i = Decimal.MaxValue;
            }
        }
        
        public virtual void ClassRefParameter(ref string i)
        {
            if (i == "Test")
            {
                i = "Result";
            }
        }

        public virtual void CharRefParameter(ref char i)
        {
            if (i == Char.MinValue)
            {
                i = Char.MaxValue;
            }
        }

        public virtual void BooleanRefParameter(ref bool i)
        {
            if (!i)
            {
                i = true;
            }
        }

        public virtual void ByteRefParameter(ref byte i)
        {
            if (i == Byte.MinValue)
            {
                i = Byte.MaxValue;
            }
        }
    }
}