using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Model;
using Autofac;
using Autofac.Integration.Mvc;
using Ui.Web.Controllers;
using System.Reflection;

namespace Ui.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<BusinessLogic.Module>();
            
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            


            using (var db = new MyContext())
            {
                db.Posts.RemoveRange(db.Posts);
                db.Users.RemoveRange(db.Users);
                db.Threads.RemoveRange(db.Threads);
                db.SaveChanges();
                
                db.Users.Add(new User() { FirstName = "Timo", LastName = "Obereder", Email="bla@bla.bla", Password = "sicheresPw", RegistrationKey = string.Empty});

                db.Threads.Add(new Thread() { CreatedAt = DateTime.Now - new TimeSpan(1, 0, 0, 0), Title = "MVVM ist noch viel cooler" });
                db.Threads.Add(new Thread() { CreatedAt = DateTime.Now - new TimeSpan(2, 0, 0, 0), Title = "MVC ist cool" });
                db.SaveChanges();

                db.Posts.Add(new Post()
                {
                    CeatedAt = DateTime.Now - new TimeSpan(1, 0, 0, 0),
                    ImagePath = string.Empty,
                    Text = "lalal",
                    UserId = db.Users.First().UserId,
                    ThreadId = db.Threads.ToArray()[0].ThreadId
                });
                db.Posts.Add(new Post()
                {
                    CeatedAt = DateTime.Now - new TimeSpan(2, 0, 0, 0),
                    ImagePath = string.Empty,
                    Text = "lalaafvasfasfl",
                    UserId = db.Users.First().UserId,
                    ThreadId = db.Threads.ToArray()[0].ThreadId
                });
                db.Posts.Add(new Post()
                {
                    CeatedAt = DateTime.Now - new TimeSpan(3, 0, 0, 0),
                    ImagePath = string.Empty,
                    Text = "lalasgyx<<fs<vs<al",
                    UserId = db.Users.First().UserId,
                    ThreadId = db.Threads.ToArray()[0].ThreadId
                });
                db.Posts.Add(new Post()
                {
                    CeatedAt = DateTime.Now - new TimeSpan(2, 0, 0, 0),
                    ImagePath = string.Empty,
                    Text = "la1414124lal",
                    UserId = db.Users.First().UserId,
                    ThreadId = db.Threads.ToArray()[1].ThreadId
                });
                db.Posts.Add(new Post()
                {
                    CeatedAt = DateTime.Now - new TimeSpan(3, 0, 0, 0),
                    ImagePath = string.Empty,
                    Text = "lasfalal",
                    UserId = db.Users.First().UserId,
                    ThreadId = db.Threads.ToArray()[1].ThreadId
                });
                db.SaveChanges();
            }

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}