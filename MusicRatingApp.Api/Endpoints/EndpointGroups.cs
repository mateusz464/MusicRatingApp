using System.Reflection;

namespace MusicRatingApp.Api.Endpoints;

public static class EndpointGroups
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        var endpointGroup = app.MapGroup("/");
        var driverEndpoints = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(IEndpointGroup)))
            .Select(t => Activator.CreateInstance(t) as IEndpointGroup)
            .ToList();
        driverEndpoints.ForEach(e => e?.RouteGroup(endpointGroup));
    }
}