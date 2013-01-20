﻿using System;
using System.Reflection;

namespace Inception.Proxying.Metadata
{
	internal class ClassProxyMetadataBuilder : ProxyMetadataBuilder
	{
		private readonly ClassProxyDefinition _proxyDefinition;

		public ClassProxyMetadataBuilder(ClassProxyDefinition proxyDefinition) 
			: base(proxyDefinition, proxyDefinition.Type)
		{
			_proxyDefinition = proxyDefinition;
		}

		protected override void InitializeConstructors()
		{
			var constructors =
				_proxyDefinition.Type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

			for (var i = 0; i < constructors.Length; i++)
			{
				var constructor = constructors[i];

				var parameters = InitializeConstructorParameters(constructor);

				Constructors.Add(new ConstructorMetadata(constructor, parameters));
			}
		}

	}
}
