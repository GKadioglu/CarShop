using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using CarShop.Business.Abstract;
using CarShop.Business.Concrete;
using CarShop.Data.Abstract;
using CarShop.Data.Concrete.EfCore;
using Castle.DynamicProxy;
using Core.Utilities;

namespace CarShop.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CarManager>().As<ICarService>();
            builder.RegisterType<EfCoreCarRepository>().As<ICarRepository>();

            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCoreCategoryRepository>().As<ICategoryRepository>();

            builder.RegisterType<ModelManager>().As<IModelService>();
            builder.RegisterType<EfCoreModelRepository>().As<IModelRepository>();

            builder.RegisterType<FavoriteCarManager>().As<IFavoriteCarService>();
            builder.RegisterType<EfFavoriteCarRepository>().As<IFavoriteCarRepository>();

            builder.RegisterType<Model3DManager>().As<IModel3DService>();
            builder.RegisterType<EfCoreModel3DRepository>().As<IModel3DRepository>();

            builder.RegisterType<AdminMessageManager>().As<IAdminMessageService>();
            builder.RegisterType<EfCoreAdminMessageRepository>().As<IAdminMessageRepository>();

            builder.RegisterType<AnonimMessageManager>().As<IAnonimMessageService>();
            builder.RegisterType<EfCoreAnonimMessageRepository>().As<IAnonimMessageRepository>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); 
            
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).InstancePerDependency();

        }
    }
}