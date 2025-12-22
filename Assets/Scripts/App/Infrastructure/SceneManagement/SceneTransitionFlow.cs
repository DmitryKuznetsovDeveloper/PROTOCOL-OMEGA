using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace App
{
    public sealed class SceneTransitionFlow
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ISceneTransitionRequest _request;

        public SceneTransitionFlow(ISceneLoader sceneLoader, ISceneTransitionRequest request)
        {
            _sceneLoader = sceneLoader;
            _request = request;
        }

        public async UniTask RunAsync(IProgress<float> progress, CancellationToken token)
        {
            _sceneLoader.SetCurrentSceneForTracking(SceneIds.Loading);

            var target = string.IsNullOrEmpty(_request.TargetSceneName)
                ? SceneIds.Meta
                : _request.TargetSceneName;

            try
            {
                await _sceneLoader.SwitchToAsync(target, progress, token);
            }
            finally
            {
                _request.Clear();
            }
        }
    }
}