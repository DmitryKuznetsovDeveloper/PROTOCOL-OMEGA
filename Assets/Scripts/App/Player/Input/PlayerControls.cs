using UnityEngine.InputSystem;

namespace App
{
    /// <summary>
    /// Единая схема управления игрока. Здесь создаём ActionMaps и бинды под PC + любой Gamepad (Xbox/PS).
    /// </summary>
    public sealed class PlayerControls : IPlayerControls
    {
        public InputActionMap GameplayMap => _gameplayMap;
        public InputActionMap UiMap => _uiMap;

        public InputAction Move => _move;
        public InputAction Look => _look;
        public InputAction Jump => _jump;
        public InputAction Sprint => _sprint;

        public InputAction Fire => _fire;
        public InputAction Reload => _reload;
        public InputAction Melee => _melee;
        public InputAction SwitchWeapon => _switchWeapon;

        public InputAction UiNavigate => _uiNavigate;
        public InputAction UiSubmit => _uiSubmit;
        public InputAction UiCancel => _uiCancel;
        public InputAction UiPause => _uiPause;

        private readonly InputActionMap _gameplayMap;
        private readonly InputActionMap _uiMap;

        private readonly InputAction _move;
        private readonly InputAction _look;
        private readonly InputAction _jump;
        private readonly InputAction _sprint;

        private readonly InputAction _fire;
        private readonly InputAction _reload;
        private readonly InputAction _melee;
        private readonly InputAction _switchWeapon;

        private readonly InputAction _uiNavigate;
        private readonly InputAction _uiSubmit;
        private readonly InputAction _uiCancel;
        private readonly InputAction _uiPause;

        public PlayerControls()
        {
            _gameplayMap = new InputActionMap("Gameplay");
            _uiMap = new InputActionMap("UI");

            // ================= GAMEPLAY =================

            // Move: WASD + LeftStick
            _move = _gameplayMap.AddAction("Move", InputActionType.Value, "Vector2");
            _move.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
            _move.AddBinding("<Gamepad>/leftStick");

            // Look: Mouse delta + RightStick
            _look = _gameplayMap.AddAction("Look", InputActionType.Value, "Vector2");
            _look.AddBinding("<Mouse>/delta");
            _look.AddBinding("<Gamepad>/rightStick");

            // Jump: Space + buttonSouth (Xbox A / PS ✕)
            _jump = _gameplayMap.AddAction("Jump", InputActionType.Button);
            _jump.AddBinding("<Keyboard>/space");
            _jump.AddBinding("<Gamepad>/buttonSouth");

            // Sprint: Shift + leftStickPress
            _sprint = _gameplayMap.AddAction("Sprint", InputActionType.Button);
            _sprint.AddBinding("<Keyboard>/leftShift");
            _sprint.AddBinding("<Gamepad>/leftStickPress");

            // Fire: LMB + rightTrigger
            _fire = _gameplayMap.AddAction("Fire", InputActionType.Button);
            _fire.AddBinding("<Mouse>/leftButton");
            _fire.AddBinding("<Gamepad>/rightTrigger");

            // Reload: R + buttonWest (Xbox X / PS □) - пример
            _reload = _gameplayMap.AddAction("Reload", InputActionType.Button);
            _reload.AddBinding("<Keyboard>/r");
            _reload.AddBinding("<Gamepad>/buttonWest");

            // Melee: Mouse4 + rightStickPress - пример
            _melee = _gameplayMap.AddAction("Melee", InputActionType.Button);
            _melee.AddBinding("<Mouse>/backButton");
            _melee.AddBinding("<Gamepad>/rightStickPress");

            // SwitchWeapon: Q + buttonNorth (Xbox Y / PS △)
            _switchWeapon = _gameplayMap.AddAction("SwitchWeapon", InputActionType.Button);
            _switchWeapon.AddBinding("<Keyboard>/q");
            _switchWeapon.AddBinding("<Gamepad>/buttonNorth");

            // ================= UI =================

            _uiNavigate = _uiMap.AddAction("Navigate", InputActionType.Value, "Vector2");
            _uiNavigate.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/upArrow")
                .With("Down", "<Keyboard>/downArrow")
                .With("Left", "<Keyboard>/leftArrow")
                .With("Right", "<Keyboard>/rightArrow");
            _uiNavigate.AddBinding("<Gamepad>/dpad");
            _uiNavigate.AddBinding("<Gamepad>/leftStick");

            _uiSubmit = _uiMap.AddAction("Submit", InputActionType.Button);
            _uiSubmit.AddBinding("<Keyboard>/enter");
            _uiSubmit.AddBinding("<Gamepad>/buttonSouth");

            _uiCancel = _uiMap.AddAction("Cancel", InputActionType.Button);
            _uiCancel.AddBinding("<Keyboard>/escape");
            _uiCancel.AddBinding("<Gamepad>/buttonEast");

            _uiPause = _uiMap.AddAction("Pause", InputActionType.Button);
            _uiPause.AddBinding("<Keyboard>/escape");
            _uiPause.AddBinding("<Gamepad>/start");

            DisableAllControls();
        }

        public void EnableGameplayControls()
        {
            _uiMap.Disable();
            _gameplayMap.Enable();
        }

        public void EnableUiControls()
        {
            _gameplayMap.Disable();
            _uiMap.Enable();
        }

        public void DisableAllControls()
        {
            _gameplayMap.Disable();
            _uiMap.Disable();
        }

        public void Dispose()
        {
            DisableAllControls();
            _gameplayMap.Dispose();
            _uiMap.Dispose();
        }
    }
}
