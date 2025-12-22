using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace App
{
    public interface ISceneLoader
    {
        string CurrentSceneName { get; }

        void SetCurrentSceneForTracking(string sceneName);

        /// <summary>Мгновенно показать шторку/сцену (Single).</summary>
        UniTask LoadSingleAsync(string sceneName, CancellationToken token);

        /// <summary>Переключение: загрузить Additive, активировать, выгрузить предыдущую.</summary>
        UniTask SwitchToAsync(string sceneName, IProgress<float> progress, CancellationToken token);

        UniTask UnloadCurrentAsync(CancellationToken token);
    }
}