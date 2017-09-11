namespace TraficLight.DashboardApp
{
    using System.Linq;
    using System.Net;
    using Windows.Networking.Connectivity;
    using Griffin.Networking.Web;
    using Microsoft.Iot.Web.Api;
    using System.Reflection;
    using System.Web.Http;
    using Microsoft.Iot.Web;

    using Windows.UI.Xaml.Controls;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var container = new UnityContainer();

           container.RegisterType<ITrafficLightService, TrafficLightService>(new HierarchicalLifetimeManager());


            var assembly = this.GetType().GetTypeInfo().Assembly;

            var settings = new HttpConfiguration
            {
                //DefaultPath = DefaultPage,
                DependencyResolver = new UnityResolver(container)
            };

            settings.Listeners.Add(new WebApiListener(assembly)); // use attribute routing            

            // Http
            var server = new WebService(settings);
            server.Start(IPAddress.Parse(GetLocalIp()), 60055);
        }

        private static string GetLocalIp()
        {
            var icp = NetworkInformation.GetInternetConnectionProfile();

            if (icp?.NetworkAdapter == null) return null;
            var hostname =
                NetworkInformation.GetHostNames()
                    .SingleOrDefault(
                        hn =>
                            hn.IPInformation?.NetworkAdapter != null && hn.IPInformation.NetworkAdapter.NetworkAdapterId
                            == icp.NetworkAdapter.NetworkAdapterId);

            // the ip address
            return hostname?.CanonicalName;
        }
    }
}
