using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Inception.Tests.ScannerTest;
using NUnit.Framework;

namespace Inception.Tests.InversionOfControl
{
    [TestFixture]
    public class TypeScanningTests
    {
        [Test]
        public void Scan_ScanningATypesAssembly_ReturnsAllTypesInAssembly()
        {
            var scanner = new TypeScanner();

            scanner.IncludeAssembyComtainingType<IScannerTestService>();

            var results = scanner.Execute();

            Assert.AreEqual(2, results.Count());
        }

        [Test]
        public void Scan_Action_ExpectedResult()
        {
            //var convention = new DefaultConvention();

            //var services = 
            //    from t in typeof(IScannerTestService).Assembly.GetTypes()
            //    where t.IsAssignableFrom(typeof(IScannerTestService)) && convention.IsMatch(t)
            //    select new { Service = typeof(IScannerTestService), Type = t };

            
        }
    }

    public interface IContainerRegistrationConvention
    {
        bool IsMatch(Type serviceType, Type concreteType);
    }

    public class DefaultConvention : IContainerRegistrationConvention
    {
        public bool IsMatch(Type serviceType, Type concreteType)
        {
            var suffix = serviceType.Name;

            if (serviceType.IsInterface)
            {
                suffix = suffix.Substring(1);
            }

            return concreteType.Name.EndsWith(suffix);
        }
    }

    public class TypeScanner
    {
        private List<Assembly> _includedAssemblies = new List<Assembly>();

        public void IncludeAssembyComtainingType<T>()
        {
            _includedAssemblies.Add(typeof(T).Assembly);
        }

        public void ScanForTypesImplementing<T>()
        {
            // build up an expression
        }

        public IEnumerable<Type> Execute()
        {
            var types = new List<Type>();

            var allTypes = _includedAssemblies.SelectMany(a => a.GetTypes());
            
            foreach (var type in allTypes)
            {
                types.Add(type);
            }

            return types;
        }
    }

}
