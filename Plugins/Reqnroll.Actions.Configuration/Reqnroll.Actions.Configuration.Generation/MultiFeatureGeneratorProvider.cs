using BoDi;
using System.Collections.Generic;
using System.Linq;
using Reqnroll.Configuration;
using Reqnroll.Generator.CodeDom;
using Reqnroll.Generator.Generation;
using Reqnroll.Generator.UnitTestConverter;
using Reqnroll.Generator.UnitTestProvider;
using Reqnroll.Parser;

namespace Reqnroll.Actions.Configuration.Generation
{
    internal class MultiFeatureGeneratorProvider : IFeatureGeneratorProvider
    {
        private readonly ITargetIdentifier _targetIdentifier;
        private readonly MultiFeatureGenerator _multiFeatureGenerator;

        public MultiFeatureGeneratorProvider(IObjectContainer container, ITargetIdentifier targetIdentifier)
        {
            _targetIdentifier = targetIdentifier;
            var featureGenerators = new List<IFeatureGenerator>();


            var targets = _targetIdentifier.GetAllAvailableTargets();

            if (targets.Any())
            {
                foreach (var target in targets)
                {
                    var targetFeatureGenerator = new UnitTestTargetFeatureGenerator(container.Resolve<IUnitTestGeneratorProvider>(), container.Resolve<CodeDomHelper>(), container.Resolve<ReqnrollConfiguration>(), container.Resolve<IDecoratorRegistry>(), target);
                    featureGenerators.Add(targetFeatureGenerator);
                }
            }
            else
            {
                featureGenerators.Add(container.Resolve<UnitTestFeatureGenerator>());
            }

            _multiFeatureGenerator = new MultiFeatureGenerator(featureGenerators);
        }

        public bool CanGenerate(ReqnrollDocument reqnrollDocument)
        {
            return true;
        }

        public IFeatureGenerator CreateGenerator(ReqnrollDocument reqnrollDocument)
        {
            return _multiFeatureGenerator;
        }

        public int Priority => PriorityValues.Normal;
    }
}