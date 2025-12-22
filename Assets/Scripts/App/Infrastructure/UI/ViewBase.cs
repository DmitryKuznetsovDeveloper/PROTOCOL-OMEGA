using UnityEngine;
using Zenject;

namespace App
{
    public abstract class ViewBase<TViewModel> : MonoBehaviour
        where TViewModel : class
    {
        protected TViewModel ViewModel => _viewModel;

        private TViewModel _viewModel;
        private bool _isBound;
        
        [Inject]
        public void Construct(TViewModel viewModel)
        {
            _viewModel = viewModel;
            TryBind();
        }

        private void OnEnable()
        {
            TryBind();
        }

        private void OnDisable()
        {
            TryUnbind();
        }

        private void OnDestroy()
        {
            TryUnbind();
        }

        private void TryBind()
        {
            if (_isBound)
            {
                return;
            }

            if (_viewModel == null)
            {
                return;
            }

            _isBound = true;
            OnBind(_viewModel);
        }

        private void TryUnbind()
        {
            if (!_isBound)
            {
                return;
            }

            _isBound = false;

            if (_viewModel != null)
            {
                OnUnbind(_viewModel);
            }
        }
        
        protected abstract void OnBind(TViewModel viewModel);
        protected abstract void OnUnbind(TViewModel viewModel);
    }
}