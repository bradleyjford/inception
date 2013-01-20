using System;
using System.Reflection;
using Inception.Reflection;
using NUnit.Framework;

namespace Inception.Tests.InversionOfControl
{
	[TestFixture]
	public class DynamicDelegateActivatorTests
	{
		public class Test
		{
			public static readonly ConstructorInfo DefaultConstructorInfo;
			public static readonly ConstructorInfo OtherConstructorInfo;

			static Test()
			{
				DefaultConstructorInfo = typeof(Test).GetConstructor(Type.EmptyTypes);
				OtherConstructorInfo = typeof(Test).GetConstructor(new[] { typeof(string), typeof(int) });
			}

			private readonly string _arg1;
			private readonly int _arg2;

			public Test()
			{
				
			}

			public Test(string arg1, int arg2)
			{
				_arg1 = arg1;
				_arg2 = arg2;
			}

			public int Arg2
			{
				get { return _arg2; }
			}

			public string Arg1
			{
				get { return _arg1; }
			}
		}

		[Test]
		public void CanInstantiateClassWithDefaultConstructor()
		{
			var activator = new DynamicDelegateActivator(typeof(Test), Test.DefaultConstructorInfo);

			var instance = activator.CreateInstance(null);

			Assert.IsNotNull(instance);
			Assert.IsInstanceOf<Test>(instance);
		}

		[Test]
		public void CanInstantiateNonDefaultConstructor()
		{
			var activator = new DynamicDelegateActivator(typeof(Test), Test.OtherConstructorInfo);

			var instance = activator.CreateInstance(new object[] { "name", Int32.MaxValue }) as Test;

			Assert.IsNotNull(instance);

			Assert.AreEqual("name", instance.Arg1);
			Assert.AreEqual(Int32.MaxValue, instance.Arg2);
		}
	}
}
