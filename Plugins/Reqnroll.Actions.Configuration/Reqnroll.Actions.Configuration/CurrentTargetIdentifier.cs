using Reqnroll.BoDi;
using System;
using Reqnroll;

namespace Reqnroll.Actions.Configuration
{
    public class CurrentTargetIdentifier
    {
        private readonly ObjectContainer _objectContainer;
        
        public CurrentTargetIdentifier(ObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            
        }

        private ObjectContainer? Container
        {
            get
            {
                try
                {
                    return _objectContainer.Resolve<ObjectContainer>();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }


        public string? Name
        {
            get
            {
                var scenarioContext = Container?.Resolve<ScenarioContext>();

                if (scenarioContext is not null && scenarioContext.ContainsKey("__ReqnrollActionsConfigurationTarget"))
                {
                    return (string)scenarioContext["__ReqnrollActionsConfigurationTarget"];
                }

                return null;
            }
        }
    }
}