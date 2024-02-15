using Reqnroll.Actions.Configuration.Generation;
using Reqnroll.Generator.Plugins;
using Reqnroll.Generator.UnitTestConverter;
using Reqnroll.Infrastructure;
using Reqnroll.UnitTestProvider;

[assembly: GeneratorPlugin(typeof(TargetsGeneratorPlugin))]
namespace Reqnroll.Actions.Configuration.Generation
{
    internal class TargetsGeneratorPlugin : IGeneratorPlugin
    {
        public void Initialize(GeneratorPluginEvents generatorPluginEvents, GeneratorPluginParameters generatorPluginParameters, UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            generatorPluginEvents.CustomizeDependencies += GeneratorPluginEvents_CustomizeDependencies!;
        }

        private void GeneratorPluginEvents_CustomizeDependencies(object sender, CustomizeDependenciesEventArgs e)
        {
            e.ObjectContainer.RegisterTypeAs<ReqnrollActionJsonLoader, IReqnrollActionJsonLoader>();
            e.ObjectContainer.RegisterTypeAs<ReqnrollActionJsonLocator, IReqnrollActionJsonLocator>();
            e.ObjectContainer.RegisterTypeAs<TargetIdentifier, ITargetIdentifier>();
            //TODO: the MultiFeatureGeneratorProvider is disabled temporarily to be able to make the code build
            //e.ObjectContainer.RegisterTypeAs<MultiFeatureGeneratorProvider, IFeatureGeneratorProvider>();
        }
    }
}