using System;
using UnityEngine.InputSystem;

namespace App
{
    public interface IPlayerControls : IDisposable
    {
        InputActionMap GameplayMap { get; }
        InputActionMap UiMap { get; }

        // Gameplay
        InputAction Move { get; }
        InputAction Look { get; }
        InputAction Jump { get; }
        InputAction Sprint { get; }

        InputAction Fire { get; }
        InputAction Reload { get; }
        InputAction Melee { get; }
        InputAction SwitchWeapon { get; }

        // UI
        InputAction UiNavigate { get; }
        InputAction UiSubmit { get; }
        InputAction UiCancel { get; }
        InputAction UiPause { get; }

        void EnableGameplayControls();
        void EnableUiControls();
        void DisableAllControls();
    }
}