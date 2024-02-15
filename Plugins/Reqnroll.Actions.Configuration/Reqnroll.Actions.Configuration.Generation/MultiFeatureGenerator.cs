using System.CodeDom;
using System.Collections.Generic;
using Reqnroll.Generator.UnitTestConverter;
using Reqnroll.Parser;

namespace Reqnroll.Actions.Configuration.Generation
{
    internal class MultiFeatureGenerator : IFeatureGenerator
    {
        private readonly List<IFeatureGenerator> _featureGenerators;

        public MultiFeatureGenerator(List<IFeatureGenerator> featureGenerators)
        {
            _featureGenerators = featureGenerators;
        }

        public CodeNamespace GenerateUnitTestFixture(ReqnrollDocument reqnrollDocument, string testClassName, string targetNamespace)
        {
            CodeNamespace? result = null;

            foreach (var featureGenerator in _featureGenerators)
            {
                var featureGeneratorResult = featureGenerator.GenerateUnitTestFixture(reqnrollDocument, testClassName, targetNamespace);

                if (result == null)
                {
                    result = featureGeneratorResult;
                }
                else
                {
                    foreach (CodeTypeDeclaration type in featureGeneratorResult.Types)
                    {
                        result.Types.Add(type);
                    }
                }
            }

            if (result == null)
            {
                result = new CodeNamespace(targetNamespace);
            }

            return result;
        }
    }
}