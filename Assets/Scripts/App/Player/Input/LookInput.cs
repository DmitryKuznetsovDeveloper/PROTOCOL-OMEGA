using UnityEngine;
using Zenject;

namespace App
{
    public interface ILookInput
    {
        Vector2 LookDelta { get; }
    }
    
    public sealed class LookInput : ILookInput, ITickable
    {
        public Vector2 LookDelta => _lookDelta;

        private readonly IPlayerControls _playerControls;
        private readonly LookSettings _settings;

        private Vector2 _lookDelta;

        public LookInput(IPlayerControls playerControls, LookSettings settings)
        {
            _playerControls = playerControls;
            _settings = settings;
        }

        public void Tick()
        {
            var raw = _playerControls.Look.ReadValue<Vector2>();
            var y = _settings.InvertY ? -raw.y : raw.y;
            _lookDelta = new Vector2(raw.x, y) * _settings.Sensitivity;
        }
    }
}