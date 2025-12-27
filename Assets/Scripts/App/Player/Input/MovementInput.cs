using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace App
{
    public interface IMovementInput
    {
        Vector2 Move { get; }
        bool IsSprintHeld { get; }
        event Action JumpPressed;
    }

    public sealed class MovementInput : IMovementInput, ITickable, IDisposable
    {
        public Vector2 Move => _move;
        public bool IsSprintHeld => _isSprintHeld;
        public event Action JumpPressed;

        private readonly IPlayerControls _playerControls;

        private Vector2 _move;
        private bool _isSprintHeld;

        public MovementInput(IPlayerControls playerControls)
        {
            _playerControls = playerControls;
            _playerControls.Jump.performed += OnJumpPerformed;
        }

        public void Tick()
        {
            _move = _playerControls.Move.ReadValue<Vector2>();
            _isSprintHeld = _playerControls.Sprint.IsPressed();
        }

        public void Dispose()
        {
            _playerControls.Jump.performed -= OnJumpPerformed;
        }

        private void OnJumpPerformed(InputAction.CallbackContext _)
        {
            JumpPressed?.Invoke();
        }
    }
}