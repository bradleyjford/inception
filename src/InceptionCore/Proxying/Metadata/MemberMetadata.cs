using System;
using System.Reflection;

namespace InceptionCore.Proxying.Metadata
{
    abstract class MemberMetadata
    {
        readonly MemberInfo _memberInfo;

        protected MemberMetadata(MemberInfo memberInfo)
        {
            _memberInfo = memberInfo;

            Name = memberInfo.Name;
        }

        protected MemberMetadata(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public bool IsExplicitInterfaceImplementation { get; set; }

        public virtual void UseExplicitInterfaceImplementation()
        {
            if (_memberInfo == null)
            {
                throw new InvalidOperationException();
            }

            Name = string.Format("{0}.{1}", _memberInfo.DeclaringType.Name, _memberInfo.Name);

            IsExplicitInterfaceImplementation = true;
        }
    }
}