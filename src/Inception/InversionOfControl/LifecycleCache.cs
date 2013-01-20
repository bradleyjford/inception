using System;
using System.Linq;

namespace Inception.InversionOfControl
{
	internal sealed class LifecycleCache : LazyCache<Type, IContainerLifecycle>
	{
		public IContainerLifecycle GetByName(string name)
		{
			var pair =
				this.First(item => String.Compare(name, item.Value.Name, StringComparison.OrdinalIgnoreCase) == 0);

			return pair.Value;
		}

		protected override IContainerLifecycle CreateItem(Type type)
		{
			return (IContainerLifecycle)Activator.CreateInstance(type);
		}
	}
}