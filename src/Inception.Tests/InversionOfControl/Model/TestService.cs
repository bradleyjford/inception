using System;

namespace Inception.Tests.InversionOfControl.Model
{
    public class TestService : ITestService
    {
        private ITestRepository _repository;

        public TestService(ITestRepository repository)
        {
            _repository = repository;
        }

        //[TimingAspect]
        public virtual int DoSomething(int i)
        {
            return Int32.MaxValue;
        }
    }
}
