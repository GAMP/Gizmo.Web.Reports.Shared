using System.Reflection;

namespace Gizmo.Web.Reports
{
    /// <summary>
    /// Host environment detection service.
    /// </summary>
    public sealed class HostEnvironmentService
    {
        public HostEnvironmentService()
        {
            if (_entryAssembly == null || _entryAssembly.FullName == null)
            {
                _hostAppType = HostAppType.Unknown;
            }
            else
            {
                var names = _entryAssembly.FullName.Split(',');
                if (names.Length < 1 || string.IsNullOrWhiteSpace(names[0]))
                {
                    _hostAppType = HostAppType.Unknown;
                    return;
                }

                if (string.Compare(names[0], "Gizmo.Web.Manager.UI.Host.WPF", true) == 0)
                {
                    _hostAppType = HostAppType.DesktopWebManager;
                }
                else if (string.Compare(names[0], "Gizmo.Web.Manager.UI.Host.Web", true) == 0)
                {
                    _hostAppType = HostAppType.WebManager;
                }
                else
                {
                    _hostAppType = HostAppType.Server;
                }
            }
        }

        private readonly Assembly? _entryAssembly = Assembly.GetEntryAssembly();
        private readonly HostAppType _hostAppType;

        /// <summary>
        /// Gets host app type.
        /// </summary>
        public HostAppType HostAppType => _hostAppType;
    }
}
