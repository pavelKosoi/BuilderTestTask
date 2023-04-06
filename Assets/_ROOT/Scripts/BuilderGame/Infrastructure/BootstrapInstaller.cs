using BuilderGame.Infrastructure.Input;
using Zenject;

namespace BuilderGame
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputProvider>().To<InputProvider>().AsSingle();
        }
    }
}