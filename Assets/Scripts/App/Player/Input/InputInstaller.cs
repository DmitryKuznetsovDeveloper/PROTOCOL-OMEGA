using Zenject;

namespace App
{
    public sealed class InputInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerControls>().To<PlayerControls>().AsSingle().NonLazy();
            Container.Bind<IPlayerInputContext>().To<PlayerInputContext>().AsSingle().NonLazy();
            
            Container.BindInterfacesTo<InputDeviceService>().AsSingle().NonLazy();
            Container.Bind<LookSettings>()
                .FromInstance(new LookSettings(sensitivity: 0.1f, invertY: false))
                .AsSingle();
            
            Container.BindInterfacesTo<MovementInput>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LookInput>().AsSingle().NonLazy();
            Container.BindInterfacesTo<WeaponInput>().AsSingle().NonLazy();
            Container.BindInterfacesTo<UiInput>().AsSingle().NonLazy();
        }
    }
}