using System;
using System.Linq;
using Inception.InversionOfControl;
using Inception.InversionOfControl.Configuration;
using Inception.Proxying;
using Inception.Tests.InversionOfControl.Model;
using NUnit.Framework;

namespace Inception.Tests.InversionOfControl
{
	[TestFixture]
	public class ConfigTests
	{
		[Test]
		public void Test()
		{
			var container = new Container(r =>
			{
				r.For<ITestService>()
					.WithConstructorArgument("name", "Hello World")
					.Use<ConstructorParameterService>();  	
	
				r.For<ITestRepository>()
					.Singleton()
					.Use<TestRepository>();
			});

			var service = container.GetInstance<ITestService>();

			Assert.IsNotNull(service);
			Assert.AreEqual("Hello World", ((ConstructorParameterService)service).ConstructorArgumentValue);
		}

		[Test]
		public void GettingSingletonReturnsSameInstance()
		{
			var container = new Container(r =>
			{
				r.For<ITestRepository>().Singleton().Use<TestRepository>();			                                           		
			});

			var repository1 = container.GetInstance<ITestRepository>();
			var repository2 = container.GetInstance<ITestRepository>();

			Assert.AreSame(repository1, repository2);
		}

		[Test]
		public void GettingTransientReturnsUniqueInstances()
		{
			var container = new Container(r =>
			{
				r.For<ITestRepository>().Use<TestRepository>();
			});

			var repository1 = container.GetInstance<ITestRepository>();
			var repository2 = container.GetInstance<ITestRepository>();
			
			Assert.AreNotSame(repository1, repository2);
		}

		[Test]
		public void CanConstructInstanceWithArray()
		{
			var container = new Container(r =>
			{
				r.For<ITestRepository>()
					.Named("Repository1")
					.Use<TestRepository>();

				r.For<ITestRepository>()
					.Named("Repository2")
					.Use<TestRepository>();  		
			});

			var service = container.GetInstance<ArrayInjectionService>();

			Assert.AreEqual(2, service.Repositories.Length);
		}

		[Test]
		public void CanInstantiateUnregisteredType()
		{
			var container = new Container(r =>
			{
				r.For<ITestRepository>().Use<TestRepository>();
			});

			var service = container.GetInstance<TestService>();

			Assert.IsNotNull(service);
		}

		[Test]
		public void ChildContainerCanResolveParentRegistration()
		{
			var container = new Container(r =>
			{
				r.For<ITestRepository>().Singleton().Use<TestRepository>();
			});

			var childContainer = container.CreateChildContainer(r =>
			{
				r.For<ITestService>()
					.WithConstructorArgument("name", "Hello World!")
					.Use<ConstructorParameterService>();
			});

			var service = childContainer.GetInstance<ITestService>();

			Assert.Throws<NullReferenceException>(() =>
			{
			    container.GetInstance<ITestService>();
			});

			Assert.IsNotNull(service);
		}

        //[Test]
        //public void CanInstantiateProxy()
        //{
        //    var container = new Container(r =>
        //    {
        //        r.For<ITestRepository>()
        //            .Singleton()
        //            .Use<TestRepository>();

        //        //var interceptor = new TestInterfaceInterceptor();

        //        r.For<ITestService>()
        //            .Singleton()
        //            .Proxy(interceptor: null)
        //            .Use<TestService>();
        //    });

        //    var service1 = container.GetInstance<ITestService>();

        //    Assert.IsNotNull(service1);
        //    Assert.AreEqual(Int32.MaxValue, service1.DoSomething(1));
        //}

		[Test]
		public void CanRegisterAndRetrieveNamedInstance()
		{
			var container = new Container(r =>
			{
				r.For<ITestRepository>()
					.Named("Test")
					.Use<TestRepository>();
			});

			var repository = container.GetInstance<ITestRepository>("Test");

			Assert.IsNotNull(repository);
		}

		[Test]
		public void RequestingUnnamedInstanceDoesNotReturnNamedInstance()
		{
			var container = new Container(r =>
			{
				r.For<ITestRepository>()
					.Named("Test")
					.Use<TestRepository>();
			});

			Assert.Throws<NullReferenceException>(() =>
				container.GetInstance<ITestRepository>()
			);
		}

		[Test]
		public void CanGetAllInstancesOfType()
		{
			var container = new Container(r =>
			{
				r.For<ITestRepository>()
					.Named("Repository1")
					.Singleton()
					.Use<TestRepository>();

				r.For<ITestRepository>()
					.Named("Repository2")
					.Singleton()
					.Use<TestRepository>();
			});

			var instances = container.GetAllInstances<ITestRepository>();

			Assert.AreEqual(2, instances.Count());
			Assert.AreNotSame(instances.ElementAt(0), instances.ElementAt(1));
		}

		[Test]
		public void CanInstantiateObjectWithConstructorArray()
		{
			var container = new Container(r =>
			{
				r.For<ITestRepository>()
					.Named("Repository1")
					.Singleton()
					.Use<TestRepository>();

				r.For<ITestRepository>()
					.Named("Repository2")
					.Singleton()
					.Use<TestRepository>();
			});

			var arrayService = container.GetInstance<ArrayInjectionService>();

			Assert.AreEqual(2, arrayService.Repositories.Count());
		}
	}
}
