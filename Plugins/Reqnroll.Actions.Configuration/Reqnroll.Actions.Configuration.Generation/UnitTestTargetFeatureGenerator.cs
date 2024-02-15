using System.CodeDom;
using Reqnroll;
using Reqnroll.Configuration;
using Reqnroll.Generator;
using Reqnroll.Generator.CodeDom;
using Reqnroll.Generator.UnitTestConverter;
using Reqnroll.Generator.UnitTestProvider;

namespace Reqnroll.Actions.Configuration.Generation
{
    internal class UnitTestTargetFeatureGenerator : UnitTestFeatureGeneratorBase
    {
        private readonly string _target;

        public UnitTestTargetFeatureGenerator(IUnitTestGeneratorProvider testGeneratorProvider,
            CodeDomHelper codeDomHelper, ReqnrollConfiguration reqnrollConfiguration,
            IDecoratorRegistry decoratorRegistry, string target)
            : base(testGeneratorProvider, codeDomHelper, reqnrollConfiguration, decoratorRegistry)
        {
            _target = target;
            //base.TestClassNameFormat += $"_{_seleniumReqnrollJsonPart.Browser}";
            TestClassNameFormat += $"_{target.Replace(".", "_")}";
        }

        internal override void SetupScenarioInitializeMethod(TestClassGenerationContext generationContext)
        {
            var scenarioInitializeMethod = generationContext.ScenarioInitializeMethod;
            scenarioInitializeMethod.Attributes = MemberAttributes.Public;
            scenarioInitializeMethod.Name = "ScenarioInitialize";
            scenarioInitializeMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(ScenarioInfo), "scenarioInfo"));
            var testRunnerExpression = _scenarioPartHelper.GetTestRunnerExpression();
            scenarioInitializeMethod.Statements.Add(new CodeMethodInvokeExpression(testRunnerExpression, "OnScenarioInitialize", new CodeVariableReferenceExpression("scenarioInfo")));
            scenarioInitializeMethod.Statements.Add(new CodeSnippetStatement($"\t\t\ttestRunner.ScenarioContext[\"__ReqnrollActionsConfigurationTarget\"] = \"{_target}\";"));
        }
    }
}