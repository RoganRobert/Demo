
using Core;
using Photon.Realtime;
using Zenject;

public class SceneContext : MonoInstaller
{

    //public PlayerController playerControllerPref;
    public override void InstallBindings()
    {
        //Container.Bind<DiContainer>().AsSingle().WhenInjectedInto<PlayerSpawner>();
        // Container.Bind<ICameraHandler>().To<CameraHandler>().AsCached();
        // Container.Bind<IMoveOnFoot>().To<MoveOnFoot>().AsCached();
        // Container.Bind<IInputHandler>().To<InputHandler>().AsCached();
        // Container.Bind<IAnimationHandler>().To<AnimationHandler>().AsCached();
        // Container.Bind<PlayerSpawner>().AsSingle();

        // Container.BindFactory<PlayerController, PlayerController.Factory>().
        //     FromComponentInNewPrefab(playerControllerPref);

    }
}
