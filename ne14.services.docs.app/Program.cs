// <copyright file="Program.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

using EnterpriseStartup.Extensions;
using EnterpriseStartup.Telemetry;
using Microsoft.AspNetCore.Builder;
using ne14.services.docs.app.Features.Av;
using ne14.services.docs.app.Features.Pdf;
using ne14.services.docs.business.Features.Pdf;

[assembly: TraceThis]

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.AddEnterpriseHealthChecks();
builder.Services.AddEnterpriseTelemetry(config);
builder.Services.AddEnterpriseMq(config)
    .AddMqConsumer<PdfConversionRequiredConsumer>()
    .AddMqProducer<PdfConversionSucceededProducer>()
    .AddMqProducer<PdfConversionFailedProducer>();

builder.Services.AddEnterpriseUserBlobs(config);
builder.Services.AddPdfConversionFeature(config);
builder.Services.AddAntiVirusFeature(config);

var app = builder.Build();
app.UseEnterpriseHealthChecks();
await app.RunAsync();
