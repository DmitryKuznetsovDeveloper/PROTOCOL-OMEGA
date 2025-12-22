using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App
{
    public sealed class SceneLoader : ISceneLoader
    {
        private string _currentSceneName;
        private bool _isTransitionInProgress;
        public string CurrentSceneName => _currentSceneName;

        public void SetCurrentSceneForTracking(string sceneName)
        {
            _currentSceneName = sceneName;
        }

        public async UniTask LoadSingleAsync(string sceneName, CancellationToken token)
        {
            if (_isTransitionInProgress)
            {
                return;
            }

            _isTransitionInProgress = true;

            try
            {
                var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
                if (op == null)
                {
                    throw new Exception($"LoadSceneAsync returned null. Scene: {sceneName}. Check Build Settings.");
                }

                await op.ToUniTask(cancellationToken: token);

                _currentSceneName = sceneName;
            }
            finally
            {
                _isTransitionInProgress = false;
            }
        }

        public async UniTask SwitchToAsync(string sceneName, IProgress<float> progress, CancellationToken token)
        {
            if (_isTransitionInProgress)
            {
                return;
            }

            _isTransitionInProgress = true;

            try
            {
                var previousSceneName = _currentSceneName;

                var loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                if (loadOp == null)
                {
                    throw new Exception($"LoadSceneAsync returned null. Scene: {sceneName}. Check Build Settings.");
                }

                await AwaitWithProgress(loadOp, progress, token);

                var newScene = SceneManager.GetSceneByName(sceneName);
                if (!newScene.IsValid() || !newScene.isLoaded)
                {
                    throw new Exception($"Loaded scene is not valid: {sceneName}");
                }

                SceneManager.SetActiveScene(newScene);

                if (!string.IsNullOrEmpty(previousSceneName))
                {
                    var oldScene = SceneManager.GetSceneByName(previousSceneName);
                    if (oldScene.IsValid() && oldScene.isLoaded)
                    {
                        var unloadOp = SceneManager.UnloadSceneAsync(oldScene);
                        if (unloadOp != null)
                        {
                            await unloadOp.ToUniTask(cancellationToken: token);
                        }
                    }

                    await Resources.UnloadUnusedAssets().ToUniTask(cancellationToken: token);
                }

                _currentSceneName = sceneName;
                progress?.Report(1f);
            }
            finally
            {
                _isTransitionInProgress = false;
            }
        }


        public async UniTask UnloadCurrentAsync(CancellationToken token)
        {
            if (string.IsNullOrEmpty(_currentSceneName))
                return;

            var scene = SceneManager.GetSceneByName(_currentSceneName);
            _currentSceneName = null;

            if (scene.IsValid() && scene.isLoaded)
            {
                var unloadOp = SceneManager.UnloadSceneAsync(scene);
                if (unloadOp != null)
                    await unloadOp.ToUniTask(cancellationToken: token);
            }

            await Resources.UnloadUnusedAssets().ToUniTask(cancellationToken: token);
        }

        private static async UniTask AwaitWithProgress(AsyncOperation op, IProgress<float> progress, CancellationToken token)
        {
            while (!op.isDone)
            {
                token.ThrowIfCancellationRequested();

                var p = op.progress < 0.9f ? op.progress / 0.9f : 1f;
                progress?.Report(p);

                await UniTask.Yield(token);
            }

            progress?.Report(1f);
        }
    }
}
