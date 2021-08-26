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
            builder.RegisterType<BloodDonationFactory>().As<IBloodDonationFactory>().InstancePerLifetimeScope();
            builder.RegisterType<BloodTransactionFactory>().As<IBloodTransactionFactory>().InstancePerLifetimeScope();
            builder.RegisterType<MedicalPersonnelFactory>().As<IMedicalPersonnelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<RecipientFactory>().As<IRecipientFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DonationListFactory>().As<IDonationListFactory>().InstancePerLifetimeScope();
            builder.RegisterType<LoginFactory>().As<ILoginFactory>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
