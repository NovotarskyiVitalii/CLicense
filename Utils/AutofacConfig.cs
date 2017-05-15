using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using CLicense.DAL;
using CLicense.Infrastructure;

namespace CLicense.Utils
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // регистрируем споставление типов
            builder.RegisterType<Repository>().As<IRepository>();
                //.WithParameter(
                //new ResolvedParameter(
                //   (pi, ctx) => pi.ParameterType == typeof(int) && pi.Name == "userID",
                //   (pi, ctx) => 1));

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        }
    }
}