using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace App
{
    public interface IUiInput
    {
        Vector2 Navigate { get; }
        event Action SubmitPressed;
        event Action CancelPressed;
        event Action PausePressed;
    }

    public sealed class UiInput : IUiInput, ITickable, IDisposable
    {
        public Vector2 Navigate => _navigate;

        public event Action SubmitPressed;
        public event Action CancelPressed;
        public event Action PausePressed;

        private readonly IPlayerControls _playerControls;
        private Vector2 _navigate;

        public UiInput(IPlayerControls playerControls)
        {
            _playerControls = playerControls;

            _playerControls.UiSubmit.performed += OnSubmitPerformed;
            _playerControls.UiCancel.performed += OnCancelPerformed;
            _playerControls.UiPause.performed += OnPausePerformed;
        }

        public void Tick()
        {
            _navigate = _playerControls.UiNavigate.ReadValue<Vector2>();
        }

        public void Dispose()
        {
            _playerControls.UiSubmit.performed -= OnSubmitPerformed;
            _playerControls.UiCancel.performed -= OnCancelPerformed;
            _playerControls.UiPause.performed -= OnPausePerformed;
        }

        private void OnSubmitPerformed(InputAction.CallbackContext _) => SubmitPressed?.Invoke();
        private void OnCancelPerformed(InputAction.CallbackContext _) => CancelPressed?.Invoke();
        private void OnPausePerformed(InputAction.CallbackContext _) => PausePressed?.Invoke();
    }
}