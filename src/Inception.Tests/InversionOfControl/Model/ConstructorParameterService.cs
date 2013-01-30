using System;

namespace Inception.Tests.InversionOfControl.Model
{
    public class ConstructorParameterService : ITestService
    {
        private ITestRepository _repository;
        private string _name;

        public ConstructorParameterService(ITestRepository repository, string name)
        {
            _repository = repository;
            _name = name;
        }

        public string ConstructorArgumentValue
        {
            get { return _name; }
        }

        public int DoSomething(int i)
        {
            return 0;
        }
    }
}
