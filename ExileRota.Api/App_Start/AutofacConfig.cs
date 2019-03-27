using System.Reflection;
using System.Runtime.Caching;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using ExileRota.Core.Repositories;
using ExileRota.Infrastructure.CommandHandlers;
using ExileRota.Infrastructure.Commands;
using ExileRota.Infrastructure.EntityFramework;
using ExileRota.Infrastructure.Services;

namespace ExileRota.Api
{
    public static class AutofacConfig
    {
        public static void Initialize()
        {
            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            // Register controllers

            var exileRotaApiAssembly = typeof(WebApiConfig)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterApiControllers(exileRotaApiAssembly);

            // Register types

            var exileRotaCoreAssembly = typeof(UserService)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(exileRotaCoreAssembly)
                .Where(x => x.IsAssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(exileRotaCoreAssembly)
                .Where(x => x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(exileRotaCoreAssembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();

            // Register single instances

            builder.RegisterInstance(AutomapperConfig.Initialize())
                .SingleInstance();

            builder.RegisterType<Encrypter>()
                .As<IEncrypter>()
                .SingleInstance();

            builder.RegisterType<JwtHandler>()
                .As<IJwtHandler>()
                .SingleInstance();

            // Register Entity Framework

            builder.RegisterType<PoeRotaContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            // Register memory chache

            builder.RegisterInstance(new MemoryCache("jwt"))
                .SingleInstance();

            // Resolve dependency

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}