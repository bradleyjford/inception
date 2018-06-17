using System;

namespace InceptionCore.Tests.InversionOfControl.Model
{
    public class ArrayInjectionService
    {
        private readonly ITestRepository[] _repositories;

        public ArrayInjectionService(ITestRepository[] repositories)
        {
            _repositories = repositories;
        }

        public ITestRepository[] Repositories
        {
            get { return _repositories; }
        }
    }
}
