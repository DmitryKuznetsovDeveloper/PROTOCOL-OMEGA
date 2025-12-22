using App;
using UnityEngine;
using UnityEngine.UI;

public sealed class LoadingView : ViewBase<LoadingViewModel>
{
    [SerializeField] private Slider _slider;

    protected override void OnBind(LoadingViewModel viewModel)
    {
        viewModel.ProgressChanged += OnProgressChanged;
        OnProgressChanged(viewModel.Progress);
    }

    protected override void OnUnbind(LoadingViewModel viewModel)
    {
        viewModel.ProgressChanged -= OnProgressChanged;
    }

    private void OnProgressChanged(float value)
    {
        if (_slider != null)
        {
            _slider.value = value;
        }
    }
}