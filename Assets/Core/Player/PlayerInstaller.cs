using Zenject;

namespace Core
{
    //sub container
    
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IInputHandler>().To<InputHandler>().AsTransient();
            Container.Bind<ICameraHandler>().To<CameraHandler>().AsTransient();
            Container.Bind<IMoveOnFoot>().To<MoveOnFoot>().AsTransient();
            Container.Bind<IAnimationHandler>().To<AnimationHandler>().AsTransient();
        }
    }
}

