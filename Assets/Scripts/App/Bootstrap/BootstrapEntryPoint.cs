using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

namespace App
{
    public sealed class BootstrapEntryPoint : IInitializable, IDisposable
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ISceneNavigator _sceneNavigator;

        private CancellationTokenSource _cts;
        private bool _started;

        public BootstrapEntryPoint(ISceneLoader sceneLoader, ISceneNavigator sceneNavigator)
        {
            _sceneLoader = sceneLoader;
            _sceneNavigator = sceneNavigator;
        }

        public void Initialize()
        {
            if (_started)
            {
                return;
            }
            
            _started = true;

            _cts = new CancellationTokenSource();
            RunAsync().Forget();
        }

        private async UniTaskVoid RunAsync()
        {
            _sceneLoader.SetCurrentSceneForTracking(SceneIds.Bootstrap);
            await _sceneNavigator.GoToAsync(SceneIds.Meta, _cts.Token);
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}