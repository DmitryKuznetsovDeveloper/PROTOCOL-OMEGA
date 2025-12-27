using System;

namespace App
{
    public enum InputDeviceType
    {
        Unknown = 0,
        KeyboardMouse = 1,
        XboxGamepad = 2,
        PlayStationGamepad = 3,
        GenericGamepad = 4
    }
    
    public interface IInputDeviceService
    {
        InputDeviceType CurrentDeviceType { get; }
        event Action<InputDeviceType> DeviceTypeChanged;
    }
}