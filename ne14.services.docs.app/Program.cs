// <copyright file="Program.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Builder;
using ne14.library.startup_extensions.Extensions;
using ne14.library.startup_extensions.Telemetry;
using ne14.services.docs.app.Features.Av;
using ne14.services.docs.app.Features.Pdf;
using ne14.services.docs.business.Features.Pdf;

[assembly: TraceThis]

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddEnterpriseHealthChecks();
builder.Services.AddEnterpriseTelemetry(config);

builder.Services.AddEnterpriseMq(config);
builder.Services.AddMqConsumer<PdfConversionRequiredConsumer>();
builder.Services.AddMqProducer<PdfConversionSucceededProducer>();
builder.Services.AddMqProducer<PdfConversionFailedProducer>();

builder.Services
    .AddPdfConversionFeature(config)
    .AddAntiVirusFeature(config);

var app = builder.Build();
app.UseEnterpriseHealthChecks();
app.Run();
