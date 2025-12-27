using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace App
{
    /// <summary>
    /// - F5: Gameplay map
    /// - F6: UI map
    /// - F7: Disable all
    /// - Пишет события (Jump/Fire/Reload/UI Submit и т.д.)
    /// - Раз в X секунд печатает состояние осей
    /// </summary>
    public sealed class InputDebugController : MonoBehaviour
    {
        private IPlayerInputContext _context;
        private IInputDeviceService _device;

        private IMovementInput _movement;
        private ILookInput _look;
        private IWeaponInput _weapon;
        private IUiInput _ui;

        private float _nextStateLogTime;

        [Inject]
        private void Construct(
            IPlayerInputContext context,
            IInputDeviceService device,
            IMovementInput movement,
            ILookInput look,
            IWeaponInput weapon,
            IUiInput ui)
        {
            _context = context;
            _device = device;
            _movement = movement;
            _look = look;
            _weapon = weapon;
            _ui = ui;
        }

        private void Awake()
        {
#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
            enabled = false;
            return;
#endif
        }

        private void OnEnable()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            _device.DeviceTypeChanged += OnDeviceChanged;

            _movement.JumpPressed += OnJump;

            _weapon.FireStarted += OnFireStarted;
            _weapon.FireCanceled += OnFireCanceled;
            _weapon.ReloadPressed += OnReload;
            _weapon.MeleePressed += OnMelee;
            _weapon.SwitchWeaponPressed += OnSwitchWeapon;

            _ui.SubmitPressed += OnUiSubmit;
            _ui.CancelPressed += OnUiCancel;
            _ui.PausePressed += OnUiPause;

            Debug.Log($"[InputDebug] Enabled. Mode={_context.CurrentMode}, Device={_device.CurrentDeviceType}");
            Debug.Log("[InputDebug] Hotkeys: F5=Gameplay, F6=UI, F7=Disable");
#endif
        }

        private void OnDisable()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            _device.DeviceTypeChanged -= OnDeviceChanged;

            _movement.JumpPressed -= OnJump;

            _weapon.FireStarted -= OnFireStarted;
            _weapon.FireCanceled -= OnFireCanceled;
            _weapon.ReloadPressed -= OnReload;
            _weapon.MeleePressed -= OnMelee;
            _weapon.SwitchWeaponPressed -= OnSwitchWeapon;

            _ui.SubmitPressed -= OnUiSubmit;
            _ui.CancelPressed -= OnUiCancel;
            _ui.PausePressed -= OnUiPause;
#endif
        }

        private void Update()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            var keyboard = Keyboard.current;
            if (keyboard != null)
            {
                if (keyboard.f5Key.wasPressedThisFrame)
                {
                    _context.ActivateGameplayControls();
                    Debug.Log($"[InputDebug] Switch -> Gameplay (Device={_device.CurrentDeviceType})");
                }

                if (keyboard.f6Key.wasPressedThisFrame)
                {
                    _context.ActivateUiControls();
                    Debug.Log($"[InputDebug] Switch -> UI (Device={_device.CurrentDeviceType})");
                }

                if (keyboard.f7Key.wasPressedThisFrame)
                {
                    _context.DeactivateAllControls();
                    Debug.Log($"[InputDebug] Switch -> Disabled (Device={_device.CurrentDeviceType})");
                }
            }

            // Раз в 0.5 сек печатаем состояния, чтобы не спамить
            if (Time.unscaledTime >= _nextStateLogTime)
            {
                _nextStateLogTime = Time.unscaledTime + 0.5f;

                Debug.Log(
                    $"[InputDebug] Mode={_context.CurrentMode} Device={_device.CurrentDeviceType} " +
                    $"Move={_movement.Move} Sprint={_movement.IsSprintHeld} Look={_look.LookDelta} FireHeld={_weapon.IsFireHeld}");
            }
#endif
        }

        private void OnDeviceChanged(InputDeviceType type)
        {
            Debug.Log($"[InputDebug] Device changed -> {type}");
        }

        private void OnJump() => Debug.Log($"[InputDebug] JumpPressed (Mode={_context.CurrentMode})");

        private void OnFireStarted() => Debug.Log($"[InputDebug] FireStarted (Mode={_context.CurrentMode})");
        private void OnFireCanceled() => Debug.Log($"[InputDebug] FireCanceled (Mode={_context.CurrentMode})");

        private void OnReload() => Debug.Log($"[InputDebug] ReloadPressed (Mode={_context.CurrentMode})");
        private void OnMelee() => Debug.Log($"[InputDebug] MeleePressed (Mode={_context.CurrentMode})");
        private void OnSwitchWeapon() => Debug.Log($"[InputDebug] SwitchWeaponPressed (Mode={_context.CurrentMode})");

        private void OnUiSubmit() => Debug.Log($"[InputDebug] UI SubmitPressed (Mode={_context.CurrentMode})");
        private void OnUiCancel() => Debug.Log($"[InputDebug] UI CancelPressed (Mode={_context.CurrentMode})");
        private void OnUiPause() => Debug.Log($"[InputDebug] UI PausePressed (Mode={_context.CurrentMode})");
    }
}
