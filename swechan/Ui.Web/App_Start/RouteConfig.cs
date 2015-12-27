using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ui.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Posts",
                url: "Thread/{threadId}/Posts",
                defaults: new
                {
                    controller = "Posts",
                    action = "PostsByThread"
                }
            );

            routes.MapRoute(
                name: "CreatePost",
                url: "Thread/{threadId}/Posts/New",
                defaults: new
                {
                    controller = "Posts",
                    action = "Create"
                }
            );

            routes.MapRoute(
                name: "EditUser",
                url: "User/{userId}/Edit",
                defaults: new
                {
                    controller = "User",
                    action = "Edit"
                }
            );

            routes.MapRoute(
                name: "DeleteUser",
                url: "User/{userId}/Delete",
                defaults: new
                {
                    controller = "User",
                    action = "Delete"
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}