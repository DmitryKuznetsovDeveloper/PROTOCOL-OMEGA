using System.Threading;
using Cysharp.Threading.Tasks;

namespace App
{
    public sealed class SceneNavigator : ISceneNavigator
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ISceneTransitionRequest _request;

        public SceneNavigator(ISceneLoader sceneLoader, ISceneTransitionRequest request)
        {
            _sceneLoader = sceneLoader;
            _request = request;
        }

        public async UniTask GoToAsync(string sceneName, CancellationToken token)
        {
            _request.TargetSceneName = sceneName;
            await _sceneLoader.LoadSingleAsync(SceneIds.Loading, token);
        }
    }
}