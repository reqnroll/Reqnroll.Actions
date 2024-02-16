using System.CodeDom;
using Reqnroll.Configuration;
using Reqnroll.Generator.CodeDom;
using Reqnroll.Generator.Generation;
using Reqnroll.Generator.UnitTestConverter;
using Reqnroll.Generator.UnitTestProvider;
using Reqnroll.Parser;
using System.Linq;

namespace Reqnroll.Actions.Configuration.Generation;

internal class UnitTestTargetFeatureGenerator : UnitTestFeatureGenerator, IFeatureGenerator
{
    private readonly string _target;

    public UnitTestTargetFeatureGenerator(IUnitTestGeneratorProvider testGeneratorProvider,
        CodeDomHelper codeDomHelper, ReqnrollConfiguration reqnrollConfiguration,
        IDecoratorRegistry decoratorRegistry, string target)
        : base(testGeneratorProvider, codeDomHelper, reqnrollConfiguration, decoratorRegistry)
    {
        _target = target;
        TestClassNameFormat += $"_{target.Replace(".", "_")}";
    }

    public new CodeNamespace GenerateUnitTestFixture(ReqnrollDocument document, string testClassName, string targetNamespace)
    {
        var codeNamespace = base.GenerateUnitTestFixture(document, testClassName, targetNamespace);

        var testType = codeNamespace.Types.OfType<CodeTypeDeclaration>().FirstOrDefault();
        if (testType != null)
        {
            var scenarioInitializeMethod = testType.Members.OfType<CodeMemberMethod>()
                .FirstOrDefault(m => m.Name == GeneratorConstants.SCENARIO_INITIALIZE_NAME);
            if (scenarioInitializeMethod != null)
            {
                scenarioInitializeMethod.Statements.Add(new CodeSnippetStatement($"\t\t\ttestRunner.ScenarioContext[\"__ReqnrollActionsConfigurationTarget\"] = \"{_target}\";"));
            }
        }


        return codeNamespace;
    }
}