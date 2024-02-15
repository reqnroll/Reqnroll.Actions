using Reqnroll.Actions.Configuration;
using Reqnroll.Actions.Selenium.Configuration;

namespace Reqnroll.Actions.LambdaTest;

public class LambdaTestConfiguration : SeleniumConfiguration
{
    private readonly IReqnrollActionsConfiguration _reqnrollActionsConfiguration;

    public LambdaTestConfiguration(IReqnrollActionsConfiguration reqnrollActionsConfiguration) : base(
        reqnrollActionsConfiguration)
    {
        _reqnrollActionsConfiguration = reqnrollActionsConfiguration;
    }

    public string Url =>
        _reqnrollActionsConfiguration.Get("selenium:lambdatest:url",
            "http://hub.lambdatest.com/wd/hub/");
}