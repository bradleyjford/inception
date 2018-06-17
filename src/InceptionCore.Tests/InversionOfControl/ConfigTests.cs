using System;
using System.Linq;
using InceptionCore.InversionOfControl;
using InceptionCore.InversionOfControl.Configuration;
using InceptionCore.Tests.InversionOfControl.Model;
using Xunit;

namespace InceptionCore.Tests.InversionOfControl
{
    public class ConfigTests
    {
        [Fact]
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

            Assert.NotNull(service);
            Assert.Equal("Hello World", ((ConstructorParameterService)service).ConstructorArgumentValue);
        }

        [Fact]
        public void GettingSingletonReturnsSameInstance()
        {
            var container = new Container(r =>
            {
                r.For<ITestRepository>().Singleton().Use<TestRepository>();                                                               
            });

            var repository1 = container.GetInstance<ITestRepository>();
            var repository2 = container.GetInstance<ITestRepository>();

            Assert.Same(repository1, repository2);
        }

        [Fact]
        public void GettingTransientReturnsUniqueInstances()
        {
            var container = new Container(r =>
            {
                r.For<ITestRepository>().Use<TestRepository>();
            });

            var repository1 = container.GetInstance<ITestRepository>();
            var repository2 = container.GetInstance<ITestRepository>();
            
            Assert.NotSame(repository1, repository2);
        }

        [Fact]
        public void CanInstantiateUnregisteredType()
        {
            var container = new Container(r =>
            {
                r.For<ITestRepository>().Use<TestRepository>();
            });

            Assert.Throws<NullReferenceException>(() =>
            {
                container.GetInstance<TestService>();
            });
        }

        [Fact]
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

            Assert.NotNull(service);
        }

        //[Fact]
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

        //    Assert.NotNull(service1);
        //    Assert.Equal(Int32.MaxValue, service1.DoSomething(1));
        //}

        [Fact]
        public void CanRegisterAndRetrieveNamedInstance()
        {
            var container = new Container(r =>
            {
                r.For<ITestRepository>()
                    .Named("Test")
                    .Use<TestRepository>();
            });

            var repository = container.GetInstance<ITestRepository>("Test");

            Assert.NotNull(repository);
        }

        [Fact]
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

        [Fact]
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

            Assert.Equal(2, instances.Count());
            Assert.NotSame(instances.ElementAt(0), instances.ElementAt(1));
        }

        [Fact]
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

                r.For<ArrayInjectionService>()
                    .Singleton()
                    .Use<ArrayInjectionService>();
            });

            var arrayService = container.GetInstance<ArrayInjectionService>();

            Assert.Equal(2, arrayService.Repositories.Count());
        }
    }
}
