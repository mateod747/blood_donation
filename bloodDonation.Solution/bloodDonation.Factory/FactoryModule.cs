using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.Factory
{
    public class FactoryModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DonorFactory>().As<IDonorFactory>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
