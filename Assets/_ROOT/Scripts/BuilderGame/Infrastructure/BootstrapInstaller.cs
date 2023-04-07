using BuilderGame.Infrastructure.Ads;
using BuilderGame.Infrastructure.Ads.Fake;
using BuilderGame.Infrastructure.Input;
using Zenject;

namespace BuilderGame.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputProvider>().To<InputProvider>().AsSingle();

            Container.Bind<FakeAdsSettings>().FromResources(nameof(FakeAdsSettings)).AsSingle();
            Container.Bind<IAdvertiser>().To<FakeAdvertiser>().AsSingle();
        }
    }
}