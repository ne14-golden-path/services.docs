// <copyright file="PdfConversionStartupExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace ne14.services.docs.app.Features.Pdf;

using FluentErrors.Extensions;
using Gotenberg.Sharp.API.Client;
using Gotenberg.Sharp.API.Client.Domain.Settings;
using Gotenberg.Sharp.API.Client.Extensions;
using ne14.library.gotenberg;
using ne14.services.docs.business.Features.Pdf;

/// <summary>
/// Startup extensions for pdf conversion.
/// </summary>
public static class PdfConversionStartupExtensions
{
    /// <summary>
    /// Adds the pdf conversion feature.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The services, for chainable calls.</returns>
    public static IServiceCollection AddPdfConversionFeature(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        configuration.MustExist();

        var section = configuration.GetSection(nameof(GotenbergSharpClient));
        services.AddOptions<GotenbergSharpClientOptions>().Bind(section);
        services.AddGotenbergSharpClient();

        return services.AddScoped<IPdfConverter, GotenbergService>();
    }
}
