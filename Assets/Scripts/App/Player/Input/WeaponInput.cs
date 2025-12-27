using System;
using UnityEngine.InputSystem;
using Zenject;

namespace App
{
    public interface IWeaponInput
    {
        bool IsFireHeld { get; }

        event Action FireStarted;
        event Action FireCanceled;

        event Action ReloadPressed;
        event Action MeleePressed;
        event Action SwitchWeaponPressed;
    }
    
    public sealed class WeaponInput : IWeaponInput, ITickable, IDisposable
    {
        public bool IsFireHeld => _isFireHeld;

        public event Action FireStarted;
        public event Action FireCanceled;
        public event Action ReloadPressed;
        public event Action MeleePressed;
        public event Action SwitchWeaponPressed;

        private readonly IPlayerControls _playerControls;
        private bool _isFireHeld;

        public WeaponInput(IPlayerControls playerControls)
        {
            _playerControls = playerControls;

            _playerControls.Fire.started += OnFireStarted;
            _playerControls.Fire.canceled += OnFireCanceled;

            _playerControls.Reload.performed += OnReloadPerformed;
            _playerControls.Melee.performed += OnMeleePerformed;
            _playerControls.SwitchWeapon.performed += OnSwitchWeaponPerformed;
        }

        public void Tick()
        {
            _isFireHeld = _playerControls.Fire.IsPressed();
        }

        public void Dispose()
        {
            _playerControls.Fire.started -= OnFireStarted;
            _playerControls.Fire.canceled -= OnFireCanceled;

            _playerControls.Reload.performed -= OnReloadPerformed;
            _playerControls.Melee.performed -= OnMeleePerformed;
            _playerControls.SwitchWeapon.performed -= OnSwitchWeaponPerformed;
        }

        private void OnFireStarted(InputAction.CallbackContext _)
        {
            _isFireHeld = true;
            FireStarted?.Invoke();
        }

        private void OnFireCanceled(InputAction.CallbackContext _)
        {
            _isFireHeld = false;
            FireCanceled?.Invoke();
        }

        private void OnReloadPerformed(InputAction.CallbackContext _) => ReloadPressed?.Invoke();
        private void OnMeleePerformed(InputAction.CallbackContext _) => MeleePressed?.Invoke();
        private void OnSwitchWeaponPerformed(InputAction.CallbackContext _) => SwitchWeaponPressed?.Invoke();
    }
}
