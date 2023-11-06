using Autofac;
using Spell.Forms.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spell.Windows.Forms.Main
{
    internal class MainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainModel>();
            builder.RegisterType<MainView>().As<IMainView>();
            builder.RegisterType<MainPresenter>();
        }
    }
}
