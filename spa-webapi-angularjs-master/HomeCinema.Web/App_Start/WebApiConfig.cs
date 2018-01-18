using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Tracing;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression.Compressors;
using WebApiContrib.Tracing.Slab;
using HomeCinema.Web.MessageHandlers;

namespace HomeCinema.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.EnableCors();
            // Web API configuration and services
            config.MessageHandlers.Add(new HomeCinemaAuthHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));

            config.EnableSystemDiagnosticsTracing();
            config.Services.Replace(typeof(ITraceWriter), new SlabTraceWriter());

            config.Services.Add(typeof(IExceptionLogger), new SlabLoggingExceptionLogger());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
        }
    }
}
