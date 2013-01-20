using System;
using System.Reflection;
using System.Reflection.Emit;
using Inception.Proxying.Generators;
using Inception.Proxying.Metadata;

namespace Inception.Proxying
{
	internal class ProxyBuilder
	{
		private readonly string _proxyNamespace;
		private readonly string _assemblyFileName;
		private readonly AssemblyBuilder _assemblyBuilder;
		private readonly ModuleBuilder _moduleBuilder;

		public ProxyBuilder(string proxyNamespace, string assemblyFileName)
		{
			_proxyNamespace = proxyNamespace;
			_assemblyFileName = assemblyFileName;

			_assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
				new AssemblyName(proxyNamespace),
				AssemblyBuilderAccess.RunAndSave);

			_moduleBuilder =
				_assemblyBuilder.DefineDynamicModule(proxyNamespace, assemblyFileName);
		}

		public Type Build(ProxyDefinition proxyDefinition)
		{
			var typeMetadata = ProxyMetadataFactory.BuildTypeMetadata(proxyDefinition);

			var generator = new TypeGenerator(_moduleBuilder, _proxyNamespace, typeMetadata);

			return generator.Generate();
		}

		public void SaveAssembly()
		{
			_assemblyBuilder.Save(_assemblyFileName);
		}
	}
}
