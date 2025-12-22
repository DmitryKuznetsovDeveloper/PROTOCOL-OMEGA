using Zenject;

namespace App
{
    public sealed class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BootstrapEntryPoint>().AsSingle();
        }
    }
}