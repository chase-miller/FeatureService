using System;
using Autofac;
using Autofac.Core;
using FeatureService.Services;

namespace FeatureService.Configuration
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method in ConfigureServices.
            builder.Register(c => new InMemoryFeatureService())
                .As<IFeatureService>()
                .InstancePerLifetimeScope();
        }
    }
}