using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

namespace App
{
    public sealed class LoadingViewModel : ViewModelBase, IInitializable
    {
        private const float Epsilon = 0.0001f;
        private readonly SceneTransitionFlow _flow;

        private CancellationTokenSource _cts;
        private bool _started;
        private float _progress;

        public event Action<float> ProgressChanged;
        public float Progress => _progress;

        public LoadingViewModel(SceneTransitionFlow flow)
        {
            _flow = flow;
        }
        
        public void Initialize()
        {
            if (_started)
            {
                return;
            }

            _started = true;

            RestartCts();
            SetProgress(0f);
            RunAsync(_cts.Token).Forget();
        }

        private void RestartCts()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
        }

        private async UniTaskVoid RunAsync(CancellationToken token)
        {
            try
            {
                var progress = new Progress<float>(SetProgress);
                await _flow.RunAsync(progress, token);
                SetProgress(1f);
            }
            catch (OperationCanceledException)
            {
                // Нормально: сцену выгрузили/переход отменили
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }

        private void SetProgress(float value)
        {
            value = Clamp01(value);

            if (Math.Abs(_progress - value) < Epsilon)
            {
                return;
            }

            _progress = value;
            ProgressChanged?.Invoke(_progress);
        }

        private static float Clamp01(float v)
        {
            return v switch
            {
                < 0f => 0f,
                > 1f => 1f,
                _ => v
            };
        }

        public override void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;

            base.Dispose();
        }
    }
}
