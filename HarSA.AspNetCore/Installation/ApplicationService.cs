using System.Reflection;

namespace HarSA.AspNetCore.Installation
{
    public class ApplicationService : IApplicationService
    {
        public string GetEntryAssemblyVersion()
        {
            return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
        }
    }
}
