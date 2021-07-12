using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloodDonation.DAL
{
    public class DALModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DonorDAL>().As<IDonorDAL>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
