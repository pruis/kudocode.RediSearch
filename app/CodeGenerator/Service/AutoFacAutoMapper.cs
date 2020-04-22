using System;
using System.Collections.Generic;
using Autofac;
using AutoMapper;

namespace CodeGenerator.Service
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //https://github.com/RichardSlater/AutoMapperWithAutofac/blob/master/README.md
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AsClosedTypesOf(typeof(ITypeConverter<,>))
                .AsImplementedInterfaces();
            //            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            //builder.RegisterAssemblyTypes(typeof(CodeGenAutoMapper).Assembly);
            builder.RegisterAssemblyTypes(typeof(CodeGenAutoMapper).Assembly)
                .AssignableTo<Profile>().As<Profile>();


            builder.Register(context =>
            {
                var profiles = context.Resolve<IEnumerable<Profile>>();

                var config = new MapperConfiguration(x =>
                {
                    foreach (var profile in profiles)
                        x.AddProfile(profile);
                    //x.AddProfile(typeof(CodeGenAutoMapper));
                });
                return config;
            }) //.SingleInstance()
                .InstancePerMatchingLifetimeScope("ExecutionPipeline")
                .AsSelf(); // Bind it to its own type

            // HACK: IComponentContext needs to be resolved again as 'tempContext' is only temporary. See http://stackoverflow.com/a/5386634/718053 
            builder.Register(tempContext =>
            {
                var ctx = tempContext.Resolve<IComponentContext>();
                var config = ctx.Resolve<MapperConfiguration>();


                // Create our mapper using our configuration above
                return config.CreateMapper();
            }).As<IMapper>(); // Bind it to the IMapper interface

            base.Load(builder);
        }
    }
}