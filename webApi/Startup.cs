using System.Reflection;
using application.Orders;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using webApi.Filters;

namespace webApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
	            options.Filters.Add(new RequestValidationFilter());
            });

		}

	    public void ConfigureContainer(ContainerBuilder builder)
	    {
		    builder.RegisterModule(new AutofacModule());
	    }

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }

	public class AutofacModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces()
				.InstancePerLifetimeScope();

			builder.RegisterAssemblyTypes(typeof(ApproveOrderRequest).GetTypeInfo().Assembly).AsImplementedInterfaces()
				.InstancePerLifetimeScope();

			var mediatrOpenTypes = new[]
			{
				typeof(IRequestHandler<,>),
				typeof(INotificationHandler<>),
			};

			foreach (var mediatrOpenType in mediatrOpenTypes)
			{
				builder
					.RegisterAssemblyTypes(typeof(ApproveOrderRequest).GetTypeInfo().Assembly)
					.AsClosedTypesOf(mediatrOpenType)
					.AsImplementedInterfaces();
			}

			builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).AsImplementedInterfaces();
			builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).AsImplementedInterfaces();

			builder.Register<ServiceFactory>(ctx =>
			{
				var c = ctx.Resolve<IComponentContext>();
				return t => c.Resolve(t);
			});

			//builder.RegisterType<DomainEventDispatcher>().As<IDomainEventDispatcher>();
		}
	}
}
