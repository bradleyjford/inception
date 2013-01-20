using System;
using System.Collections.Generic;
using System.Threading;

namespace Inception.InversionOfControl
{
	public sealed class ThreadStaticContainerLifecycle : ManagedContainerLifecycle
	{
		private readonly Dictionary<Thread, SingletonContainerLifecycle> _threadStaticLifecycles =
			new Dictionary<Thread, SingletonContainerLifecycle>();

		private readonly ReaderWriterLockSlim _lock = 
			new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

		public override string Name
		{
			get { return "ThreadStatic"; }
		}

		public override object GetInstance(IContainer container, IRegistration registration)
		{
			if (IsDisposed) throw new ObjectDisposedException("ThreadStaticContainerLifecycle");

			var lifecycle = GetThreadStaticLifecycle();

			return lifecycle.GetInstance(container, registration);
		}

		private SingletonContainerLifecycle GetThreadStaticLifecycle()
		{
			_lock.ExitUpgradeableReadLock();

			try
			{
				var thread = Thread.CurrentThread;

				if (_threadStaticLifecycles.ContainsKey(thread))
				{
					return _threadStaticLifecycles[thread];
				}

				_lock.EnterWriteLock();

				try
				{
					var lifecycle = new SingletonContainerLifecycle();

					_threadStaticLifecycles.Add(thread, lifecycle);

					return lifecycle;
				}
				finally
				{
					_lock.ExitWriteLock();
				}
			}
			finally
			{
				_lock.ExitUpgradeableReadLock();
			}
		}

		protected override void Dispose(bool disposing)
		{
			foreach (var lifecycle in _threadStaticLifecycles.Values)
			{
				lifecycle.Dispose();
			}

			_threadStaticLifecycles.Clear();

			_lock.Dispose();
		}
	}
}
