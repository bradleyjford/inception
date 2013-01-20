using System;

namespace Inception.InversionOfControl
{
	internal class ArrayCastMethodCache : LazyCache<Type, Func<object[], object>>
	{
		protected override Func<object[], object> CreateItem(Type key)
		{
			return ArrayCastDynamicMethodGenerator.Generate(key);
		}
	}
}
