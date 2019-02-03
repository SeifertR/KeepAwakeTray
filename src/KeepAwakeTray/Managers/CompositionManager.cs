using CommonServiceLocator;
using KeepAwakeTray.Interfaces;
using Unity;
using Unity.ServiceLocation;

namespace KeepAwakeTray.Managers
{
    public class CompositionManager
    {
        private readonly UnityContainer container;

        public CompositionManager()
        {
            container = new UnityContainer();
            ConfigureContainer();
            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        private void ConfigureContainer()
        {
            container.RegisterSingleton<IAppManager, AppManager>();
            container.RegisterSingleton<IActivityMonitor, ActivityMonitor>();
            container.RegisterSingleton<ISettingsManager, SettingsManager>();
        }

        public T Resolve<T>() => container.Resolve<T>();
    }
}
