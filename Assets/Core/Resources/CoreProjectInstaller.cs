
using Zenject;

namespace Core
{
    public class CoreProjectInstaller : MonoInstaller<CoreProjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<DiContainer>().AsSingle().WhenInjectedInto<PlayerSpawner>();
        }
    }

}