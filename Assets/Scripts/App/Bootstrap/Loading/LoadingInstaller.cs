using Zenject;

namespace App
{
    public sealed class LoadingInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LoadingView>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();;
            
            Container.BindInterfacesAndSelfTo<LoadingViewModel>()
                .AsSingle()
                .NonLazy();
        }
    }
}