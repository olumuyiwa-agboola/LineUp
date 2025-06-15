global using System;
global using Microsoft.VisualStudio.Shell;
global using Community.VisualStudio.Toolkit;
global using Task = System.Threading.Tasks.Task;

using System.Threading;
using System.Runtime.InteropServices;

namespace LineUp
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.LineUpString)]
    public sealed class LineUpPackage : ToolkitPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.RegisterCommandsAsync();
        }
    }
}