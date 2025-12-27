using System;
using Zenject;

namespace App
{
    public sealed class InputDebugService : IInitializable, IDisposable
    {
        private readonly IPlayerInputContext _context;
        private readonly IInputDeviceService _device;

        private readonly IMovementInput _movementInput;
        private readonly ILookInput _lookInput;
        private readonly IWeaponInput _weaponInput;
        private readonly IUiInput _uiInput;

        public InputDebugService(
            IPlayerInputContext context,
            IInputDeviceService device,
            IMovementInput movementInput,
            ILookInput lookInput,
            IWeaponInput weaponInput,
            IUiInput uiInput)
        {
            _context = context;
            _device = device;
            _movementInput = movementInput;
            _lookInput = lookInput;
            _weaponInput = weaponInput;
            _uiInput = uiInput;
        }

        public void Initialize()
        {
            _device.DeviceTypeChanged += OnDeviceChanged;

            _movementInput.JumpPressed += OnJump;

            _weaponInput.FireStarted += OnFireStarted;
            _weaponInput.FireCanceled += OnFireCanceled;
            _weaponInput.ReloadPressed += OnReload;
            _weaponInput.MeleePressed += OnMelee;
            _weaponInput.SwitchWeaponPressed += OnSwitchWeapon;

            _uiInput.SubmitPressed += OnUiSubmit;
            _uiInput.CancelPressed += OnUiCancel;
            _uiInput.PausePressed += OnUiPause;

            UnityEngine.Debug.Log($"[InputDebug] Initialized. Mode: {_context.CurrentMode}, Device: {_device.CurrentDeviceType}");
        }

        public void Dispose()
        {
            _device.DeviceTypeChanged -= OnDeviceChanged;

            _movementInput.JumpPressed -= OnJump;

            _weaponInput.FireStarted -= OnFireStarted;
            _weaponInput.FireCanceled -= OnFireCanceled;
            _weaponInput.ReloadPressed -= OnReload;
            _weaponInput.MeleePressed -= OnMelee;
            _weaponInput.SwitchWeaponPressed -= OnSwitchWeapon;

            _uiInput.SubmitPressed -= OnUiSubmit;
            _uiInput.CancelPressed -= OnUiCancel;
            _uiInput.PausePressed -= OnUiPause;
        }

        private void OnDeviceChanged(InputDeviceType type)
        {
            UnityEngine.Debug.Log($"[InputDebug] Device changed: {type}");
        }

        private void OnJump()
        {
            UnityEngine.Debug.Log($"[InputDebug] JumpPressed (Mode: {_context.CurrentMode})");
        }

        private void OnFireStarted()
        {
            UnityEngine.Debug.Log($"[InputDebug] FireStarted (Mode: {_context.CurrentMode})");
        }

        private void OnFireCanceled()
        {
            UnityEngine.Debug.Log($"[InputDebug] FireCanceled (Mode: {_context.CurrentMode})");
        }

        private void OnReload()
        {
            UnityEngine.Debug.Log($"[InputDebug] ReloadPressed (Mode: {_context.CurrentMode})");
        }

        private void OnMelee()
        {
            UnityEngine.Debug.Log($"[InputDebug] MeleePressed (Mode: {_context.CurrentMode})");
        }

        private void OnSwitchWeapon()
        {
            UnityEngine.Debug.Log($"[InputDebug] SwitchWeaponPressed (Mode: {_context.CurrentMode})");
        }

        private void OnUiSubmit()
        {
            UnityEngine.Debug.Log($"[InputDebug] UI SubmitPressed (Mode: {_context.CurrentMode})");
        }

        private void OnUiCancel()
        {
            UnityEngine.Debug.Log($"[InputDebug] UI CancelPressed (Mode: {_context.CurrentMode})");
        }

        private void OnUiPause()
        {
            UnityEngine.Debug.Log($"[InputDebug] UI PausePressed (Mode: {_context.CurrentMode})");
        }
    }
}
