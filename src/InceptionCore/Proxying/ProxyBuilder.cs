using System;
using System.Reflection;
using System.Reflection.Emit;
using InceptionCore.Proxying.Generators;
using InceptionCore.Proxying.Metadata;

namespace InceptionCore.Proxying
{
    class ProxyBuilder
    {
        readonly AssemblyBuilder _assemblyBuilder;
        readonly string _assemblyFileName;
        readonly ModuleBuilder _moduleBuilder;
        readonly string _proxyNamespace;

        public ProxyBuilder(string proxyNamespace, string assemblyFileName)
        {
            _proxyNamespace = proxyNamespace;
            _assemblyFileName = assemblyFileName;

            _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName(proxyNamespace),
                AssemblyBuilderAccess.Run);

            _moduleBuilder = _assemblyBuilder.DefineDynamicModule(assemblyFileName);
        }

        public Type Build(ProxyDefinition proxyDefinition)
        {
            var typeMetadata = ProxyMetadataFactory.BuildTypeMetadata(proxyDefinition);

            var generator = new TypeGenerator(_moduleBuilder, _proxyNamespace, typeMetadata);

            return generator.Generate();
        }

        public void SaveAssembly()
        {
            throw new NotSupportedException();
        }
    }
}