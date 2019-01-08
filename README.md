
# Logging Abstractor

[![Build status](https://ci.appveyor.com/api/projects/status/nw12mgqdk7usuh37/branch/master?svg=true)](https://ci.appveyor.com/project/bernarden/loggingabstractor/branch/master)

Provides an interface which standardizes server side logging across third party logging platforms using .NET standard. 
This makes it easy to swap out logging providers, log to multiple platforms at once and standardize your projects logging interface. 

Currently supports the following platforms: [Raygun](http://raygun.com/), [Application Insights](https://docs.microsoft.com/en-us/azure/azure-monitor/overview#application-insights) and [Sentry](https://sentry.io/welcome/)

## Getting started in a .NET core api
 1. In the WebApi project, add the LoggingAbstractor.Core and platform specific LoggingAbstractor nuget packages (.e. LoggingAbstractor.Raygun if using raygun).
2. In the startup.cs register the IAbstractLogger for the platform(s) you use:

**ApplicationInsights**


      services.AddApplicationInsightsTelemetry(Configuration);
            services.AddTransient<IAbstractLogger>(x => new AppInsightsAbstractLogger(x.GetService<TelemetryClient>()));

**Raygun**
   

     public void ConfigureServices(IServiceCollection services) {
            ...
                services.AddRaygun(Configuration);
                services.AddTransient<IAbstractLogger>(x => new RaygunAbstractLogger(new RaygunClient(raygunApiKey), minimalLoggingLevel));
        }


## Multiple loggers
If you want to log to multiple platforms at once you can register the MultiAbstractLogger and pass in the loggers you want to use. This example shows how to register the application insights and sentry loggers.
**Startup.cs** 
  

      public void ConfigureServices(IServiceCollection services) {
        ...
        var raygunApiKey = Configuration.GetSection("RaygunSettings")["ApiKey"];
        services.AddTransient<IAppInsightsAbstractLogger>(x => new AppInsightsAbstractLogger(x.GetService<TelemetryClient>(), minimalLoggingLevel));
        services.AddTransient<IRaygunAbstractLogger>(x => new RaygunAbstractLogger(new RaygunClient(raygunApiKey), minimalLoggingLevel));
        services.AddTransient<IAbstractLogger>(x =>
        {
            IEnumerable<IAbstractLogger> abstractLoggers = new List<IAbstractLogger>
                { x.GetService<IAppInsightsAbstractLogger>(), x.GetService<IRaygunAbstractLogger>() };
            return new MultiAbstractLogger(abstractLoggers);
        });   
    }

## Logging level
Certain logging platforms store a logging level tag against each log, this allows for logs to be prioritized. Logging abstractor provides a generic enum which will map to the equivalent logging level of a specific platform. Below is how they map

| LoggingAbstractor| ApplicationInsights| Raygun* | Sentry
| ------------- |-------------| ------------- |------------- |
| Verbose      | Verbose      |Verbose|Debug
| Information      |Information   |Information|Info
| Warning| Warning|Warning|Warning
| Error| Error|Error|Error
| Critical| Critical|Critical|Critical
| None| Verbose      |None|Debug

*Does not support logging levels. Logging level is added as a tag

## Contributing
PR's welcome, see contribution guide for details.
