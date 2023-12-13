// <copyright file="Program.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

using ne14.library.startup_extensions;
using ne14.library.startup_extensions.Telemetry;
using ne14.services.docs.app;
using ne14.services.docs.app.Features.Av;
using ne14.services.docs.app.Features.Pdf;

[assembly: TraceThis]

var builder = Host.CreateApplicationBuilder(args);
var config = builder.Configuration;

builder.Services.AddHostedService<Worker>();
builder.Services.AddEnterpriseHealthChecks();
builder.Services.AddEnterpriseTelemetry(config);

builder.Services
    .AddPdfConversionFeature(config)
    .AddAntiVirusFeature(config);

var host = builder.Build();
host.Run();
