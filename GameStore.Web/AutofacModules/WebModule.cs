using Autofac;
using GameStore.Interfaces.Web.Settings;
using GameStore.Web.Payment;
using GameStore.Web.Settings.API;

namespace GameStore.Web.AutofacModules
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BankPayment>().Keyed<IPayment>(PaymentType.Bank);
            builder.RegisterType<IBoxPayment>().Keyed<IPayment>(PaymentType.IBox);
            builder.RegisterType<CardPayment>().Keyed<IPayment>(PaymentType.Visa);
            builder.RegisterType<JwtTokenGenerator>().As<ITokenGenerator>();
        }
    }
}