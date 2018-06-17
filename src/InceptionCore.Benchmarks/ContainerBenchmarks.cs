using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using InceptionCore.InversionOfControl;
using InceptionCore.InversionOfControl.Configuration;
using InceptionCore.Tests.InversionOfControl.Model;

namespace InceptionCore.Benchmarks
{
    public class ContainerBenchmarks
    {
        Container _container;

        [GlobalSetup]
        public void Setup()
        {
            _container = new Container(c =>
            {
                c.For<ITestService>()
                    .WithConstructorArgument("name", "Hello World")
                    .Use<ConstructorParameterService>();

                c.For<ITestRepository>()
                    .Singleton()
                    .Use<TestRepository>();
            });
        }

        [Benchmark]
        public void ResolveInstance()
        {
            var service = _container.GetInstance<ITestService>();

            service.DoSomething(13);
        }

        [Benchmark]
        public void ContainerCreationAndResolveInstance()
        {
            var container = new Container(c =>
            {
                c.For<ITestService>()
                    .WithConstructorArgument("name", "Hello World")
                    .Use<ConstructorParameterService>();

                c.For<ITestRepository>()
                    .Singleton()
                    .Use<TestRepository>();
            });

            var service = container.GetInstance<ITestService>();

            service.DoSomething(13);

        }
    }
}
