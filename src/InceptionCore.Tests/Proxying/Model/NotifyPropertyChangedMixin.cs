using System;
using System.ComponentModel;
using InceptionCore.Proxying;

namespace InceptionCore.Tests.Proxying.Model
{
    public sealed class NotifyPropertyChangedMixin : IInterceptor, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void IInterceptor.Intercept(IInvocation invocation)
        {
            if (!IsPropertySetInvocation(invocation))
            {
                invocation.Proceed();
                return;
            }

            invocation.Proceed();

            var propertyName = GetPropertyName(invocation);

            RaisePropertyChanged(invocation.Target, propertyName);
        }

        private bool IsPropertySetInvocation(IInvocation invocation)
        {
            return invocation.Method.Name.StartsWith("set_");
        }

        private string GetPropertyName(IInvocation invocation)
        {
            return invocation.Method.Name.Substring(4);
        }

        private void RaisePropertyChanged(object target, string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(target, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
