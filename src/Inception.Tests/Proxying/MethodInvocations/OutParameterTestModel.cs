using System;

namespace Inception.Tests.Proxying.MethodInvocations
{
	public class OutParameterTestModel
	{
		public virtual void Int16OutParameter(out short i)
		{
			i = Int16.MaxValue;
		}

		public virtual void Int32OutParameter(out int i)
		{
			i = Int32.MaxValue;
		}

		public virtual void Int64OutParameter(out long i)
		{
			i = Int64.MaxValue;
		}

		public virtual void UInt16OutParameter(out ushort i)
		{
			i = UInt16.MaxValue;
		}

		public virtual void UInt32OutParameter(out uint i)
		{
			i = UInt32.MaxValue;
		}

		public virtual void UInt64OutParameter(out ulong i)
		{
			i = UInt64.MaxValue;
		}

		public virtual void SingleOutParameter(out float i)
		{
			i = Single.MaxValue;
		}

		public virtual void DoubleOutParameter(out double i)
		{
			i = Double.MaxValue;
		}

		public virtual void DecimalOutParameter(out decimal i)
		{
			i = Decimal.MaxValue;
		}

		public virtual void ClassOutParameter(out string i)
		{
			i = "Result";
		}

		public virtual void CharOutParameter(out char i)
		{
			i = Char.MaxValue;
		}

		public virtual void BooleanOutParameter(out bool i)
		{
			i = true;
		}

		public virtual void ByteOutParameter(out byte i)
		{
			i = Byte.MaxValue;
		}
	}
}