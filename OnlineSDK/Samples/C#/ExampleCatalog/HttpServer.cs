using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Hosting.Self;
using Nancy.TinyIoc;
using System.Net.NetworkInformation;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Json;

namespace ExampleCatalog
{

    public class HttpServer
    {
        private NancyHost host;

        public HttpServer()
        {
            HostConfiguration hostConfigs = new HostConfiguration();
            hostConfigs.UrlReservations.CreateAutomatically = true;

            host = new NancyHost(new CustomNancyBootstrapper(this), hostConfigs, new Uri($"http://localhost/{HttpServer.Root}/"));
            JsonSettings.RetainCasing = true;
            host.Start();
            Console.WriteLine($"Started web app: http://localhost/" + Root);

            var url = "\t\thttp://{0}/" + Root;

            Console.WriteLine("\r\nActive IP Addresses: \r\n");
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.OperationalStatus == OperationalStatus.Up)
                {
                    var type = netInterface.NetworkInterfaceType;
                    if (type == NetworkInterfaceType.Ethernet || type == NetworkInterfaceType.GigabitEthernet || type == NetworkInterfaceType.Wireless80211)
                    {
                        Console.WriteLine("\t{0} - {1}", netInterface.Name, netInterface.Description);
                        Console.WriteLine();
                        IPInterfaceProperties ipProps = netInterface.GetIPProperties();
                        foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
                        {
                            if (addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                Console.WriteLine(url, addr.Address.ToString());
                            }
                        }
                        Console.WriteLine("");
                    }
                }
            }
        }

        public static string Root => "NancyExampleCatalog";
    }

    public class CustomNancyBootstrapper : DefaultNancyBootstrapper
    {
        private HttpServer server;

        public CustomNancyBootstrapper(HttpServer server)
        {
            this.server = server;
        }

	    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
	    {
			this.Conventions.ViewLocationConventions.Add((viewName, model, context) =>
			{
				return string.Concat("Views/", viewName);
			});
		}

	    protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Clear();

            var responseBuilder = StaticContentConventionBuilderAddOn.AddDirectoryWithExpiresHeader("Content", TimeSpan.FromMinutes(1));
            conventions.StaticContentsConventions.Add(responseBuilder);
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            container.Register(server);
        }
    }

    public static class StaticContentConventionBuilderAddOn
    {
        public static Func<NancyContext, string, Response> AddDirectoryWithExpiresHeader(
            string requestedPath,
            TimeSpan expiresTimeSpan,
            string contentPath = null,
            params string[] allowedExtensions)
        {
            var responseBuilder = StaticContentConventionBuilder
                .AddDirectory(requestedPath, contentPath, allowedExtensions);
            return (context, root) =>
            {
                var response = responseBuilder(context, root);
                if (response != null)
                {
                    response.Headers.Add("Expires", DateTime.Now.Add(expiresTimeSpan).ToString("R"));
                }
                return response;
            };
        }
    }
}
