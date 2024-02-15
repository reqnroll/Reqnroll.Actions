using System;

namespace Reqnroll.Actions.WindowsAppDriver
{
    public interface IAppDriverCli : IDisposable
    {
        void Start();
    }
}