// <copyright file="AntiVirusStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.app.Features.Av;

using nClam;
using ne14.library.clamav;
using ne14.services.docs.business.Features.Av;

/// <summary>
/// Startup extensions for the antivirus feature.
/// </summary>
public static class AntiVirusStartupExtensions
{
    /// <summary>
    /// Adds the antivirus feature.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The services, for chainable calls.</returns>
    public static IServiceCollection AddAntiVirusFeature(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var clamHost = configuration.GetValue<string>("ClamAv:Hostname")!;
        var clamPort = configuration.GetValue<int>("ClamAv:Port");
        var clamMaxSize = configuration.GetValue<long>("ClamAv:MaxStreamSize");

        services
            .AddScoped<IClamClient>(_ => new ClamClient(clamHost, clamPort) { MaxStreamSize = clamMaxSize })
            .AddScoped<IAntiVirusScanner, ClamAvScanner>()
            .AddHealthChecks()
            .AddTcpHealthCheck(o => o.AddHost(clamHost, clamPort), "clamav");

        return services;
    }
}
