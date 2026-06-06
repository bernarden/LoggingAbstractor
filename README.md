# Logging Abstractor

[![Build status](https://ci.appveyor.com/api/projects/status/nw12mgqdk7usuh37/branch/master?svg=true)](https://ci.appveyor.com/project/bernarden/loggingabstractor/branch/master)

Provides an interface which standardizes server side logging across third party logging platforms using .NET standard. 
This makes it easy to swap out logging providers, log to multiple platforms at once and standardize your projects logging interface. 

Currently supports the following platforms: [Raygun](http://raygun.com/), [Application Insights](https://docs.microsoft.com/en-us/azure/azure-monitor/overview#application-insights) and [Sentry](https://sentry.io/welcome/)

## Getting started

1. In your project, add the `LoggingAbstractor.Core` and platform specific `LoggingAbstractor` nuget packages (e.g., `LoggingAbstractor.Raygun` if using Raygun).
2. In your `Program.cs`, register the provider's SDK and then register the `IAbstractLogger` for the platform(s) you use:

    **Application Insights**
    ```csharp
    builder.Services.AddApplicationInsightsTelemetry();
    builder.Services.AddTransient<IAbstractLogger>(x => 
        new AppInsightsAbstractLogger(x.GetRequiredService<TelemetryClient>(), minimalLoggingLevel)
    );
    ```

    **Raygun**
    ```csharp
    builder.Services.AddRaygun(builder.Configuration);
    builder.Services.AddTransient<IAbstractLogger>(x => 
        new RaygunAbstractLogger(x.GetRequiredService<RaygunClient>(), minimalLoggingLevel)
    );
    ```

    **Sentry**
    ```csharp
    builder.WebHost.UseSentry();
    builder.Services.AddTransient<IAbstractLogger>(x => 
        new SentryAbstractLogger(x.GetRequiredService<IHub>(), minimalLoggingLevel)
    );
    ```


## Multiple loggers

If you want to log to multiple platforms at once you can register the MultiAbstractLogger and pass in the loggers you want to use. This example shows how to register the Application Insights, Raygun, and Sentry loggers together.

```csharp
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddTransient<IAppInsightsAbstractLogger>(x =>
    new AppInsightsAbstractLogger(x.GetRequiredService(), minimalLoggingLevel));

builder.Services.AddRaygun(builder.Configuration);
builder.Services.AddTransient<IRaygunAbstractLogger>(x =>
    new RaygunAbstractLogger(x.GetRequiredService(), minimalLoggingLevel));

builder.WebHost.UseSentry();
builder.Services.AddTransient<ISentryAbstractLogger>(x =>
    new SentryAbstractLogger(x.GetRequiredService(), minimalLoggingLevel));

builder.Services.AddTransient<IAbstractLogger>(x =>
{
    var abstractLoggers = new List<IAbstractLogger>
    {
        x.GetRequiredService<IAppInsightsAbstractLogger>(),
        x.GetRequiredService<IRaygunAbstractLogger>(),
        x.GetRequiredService<ISentryAbstractLogger>()
    };
    return new MultiAbstractLogger(abstractLoggers);
})
```

## Logging level
Certain logging platforms store a logging level tag against each log, this allows for logs to be prioritized. Logging abstractor provides a generic enum which will map to the equivalent logging level of a specific platform. Below is how they map:

| LoggingAbstractor | ApplicationInsights | Raygun*     | Sentry   |
| ----------------- | ------------------- | ----------- | -------- |
| Verbose           | Verbose             | Verbose     | Debug    |
| Information       | Information         | Information | Info     |
| Warning           | Warning             | Warning     | Warning  |
| Error             | Error               | Error       | Error    |
| Critical          | Critical            | Critical    | Critical |
| None              | None                | None        | None     |

*\*Does not support logging levels natively. Logging level is added as a tag.*

## Contributing
PR's welcome, see contribution guide for details.
