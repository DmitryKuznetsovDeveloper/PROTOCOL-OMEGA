using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.XInput;
using Zenject;

namespace App
{
    public sealed class InputDeviceService : IInputDeviceService, IInitializable, IDisposable
    {
        public InputDeviceType CurrentDeviceType { get; private set; } = InputDeviceType.Unknown;

        public event Action<InputDeviceType> DeviceTypeChanged;

        private IDisposable _anyButtonSubscription;

        public void Initialize()
        {
            // Срабатывает на любое нажатие любой кнопки на любом устройстве.
            _anyButtonSubscription = InputSystem.onAnyButtonPress.Call(OnAnyButtonPress);
        }

        public void Dispose()
        {
            _anyButtonSubscription?.Dispose();
            _anyButtonSubscription = null;
        }

        private void OnAnyButtonPress(InputControl control)
        {
            var device = control?.device;
            if (device == null)
            {
                return;
            }

            var newType = ResolveDeviceType(device);
            if (newType == CurrentDeviceType)
            {
                return;
            }

            CurrentDeviceType = newType;
            DeviceTypeChanged?.Invoke(CurrentDeviceType);
        }

        private static InputDeviceType ResolveDeviceType(InputDevice device)
        {
            switch (device)
            {
                // Клава/мышь
                case Keyboard:
                case Mouse:
                    return InputDeviceType.KeyboardMouse;
                // Xbox (XInput)
                case XInputController:
                    return InputDeviceType.XboxGamepad;
                // PlayStation (DualShock). DualSense тоже часто попадает в “Sony HID”, но в новом пакете есть DualShock/DualSense типы.
                case DualShockGamepad:
                    return InputDeviceType.PlayStationGamepad;
                // Любой другой геймпад
                case Gamepad:
                    return InputDeviceType.GenericGamepad;
                default:
                    return InputDeviceType.Unknown;
            }
        }
    }
}
