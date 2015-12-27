using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace BusinessLogic
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<Model.Module>();

            builder
                .RegisterType<BusinessLogic.PostBusinessLayer>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<BusinessLogic.UserBusinessLayer>()
                .InstancePerLifetimeScope();
            builder
                .RegisterType<BusinessLogic.ThreadBusinessLayer>()
                .InstancePerLifetimeScope();

        }
    }
}