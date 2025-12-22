using Zenject;

namespace App
{
    public sealed class AppInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SceneFlowBind();
        }

        private void SceneFlowBind()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<ISceneTransitionRequest>().To<SceneTransitionRequest>().AsSingle();
            Container.Bind<SceneTransitionFlow>().AsSingle();
            Container.Bind<ISceneNavigator>().To<SceneNavigator>().AsSingle();
        }
    }
}