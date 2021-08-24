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
            builder.RegisterType<BloodDonationDAL>().As<IBloodDonationDAL>().InstancePerLifetimeScope();
            builder.RegisterType<BloodTransactionDAL>().As<IBloodTransactionDAL>().InstancePerLifetimeScope();
            builder.RegisterType<MedicalPersonnelDAL>().As<IMedicalPersonnelDAL>().InstancePerLifetimeScope();
            builder.RegisterType<RecipientDAL>().As<IRecipientDAL>().InstancePerLifetimeScope();
            builder.RegisterType<BloodStockDAL>().As<IBloodStockDAL>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
