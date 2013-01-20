using System;
using System.Reflection;

namespace Inception.Proxying.Metadata
{
	internal abstract class MemberMetadata
	{
		private readonly MemberInfo _memberInfo;

		private string _name;
		private bool _isExplicitInterfaceImplementation;

		protected MemberMetadata(MemberInfo memberInfo)
		{
			_memberInfo = memberInfo;

			_name = memberInfo.Name;
		}

		protected MemberMetadata(string name)
		{
			_name = name;
		}

		public string Name
		{
			get { return _name; }
		}

		public bool IsExplicitInterfaceImplementation
		{
			get { return _isExplicitInterfaceImplementation; }
		}

		public virtual void UseExplicitInterfaceImplementation()
		{
			if (_memberInfo == null)
			{
				throw new InvalidOperationException();	
			}

			_name = String.Format("{0}.{1}", _memberInfo.DeclaringType.Name, _memberInfo.Name);

			_isExplicitInterfaceImplementation = true;
		}
	}
}
