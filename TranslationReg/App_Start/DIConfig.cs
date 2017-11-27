using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using SqlRepository;
using System.Web.Mvc;
using TranslationReg;
using TranslationRegistryModel;

public class AutofacConfig
{
    public static void ConfigureContainer()
    {
        // получаем экземпляр контейнера
        var builder = new ContainerBuilder();

        // регистрируем контроллер в текущей сборке
        builder.RegisterControllers(typeof(MvcApplication).Assembly);

        // Регистрируем споставление типов
        // Работа с готовой БД
        //var dbConnStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SqlRepository.SqlContext;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //builder.RegisterType<SqlRep>().As<IRepository>()
        //    .WithParameter("connStr",dbConnStr);

        // Для автоматического создания БД
        builder.RegisterType<SqlRep>().As<IRepository>();

        // создаем новый контейнер с теми зависимостями, которые определены выше
        var container = builder.Build();

        // установка сопоставителя зависимостей
        DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
    }
}