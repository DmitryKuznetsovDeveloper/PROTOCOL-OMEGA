namespace App
{
    public sealed class PlayerInputContext : IPlayerInputContext
    {
        public PlayerInputMode CurrentMode { get; private set; }

        private readonly IPlayerControls _playerControls;

        public PlayerInputContext(IPlayerControls playerControls)
        {
            _playerControls = playerControls;
            DeactivateAllControls();
        }

        public void ActivateGameplayControls()
        {
            CurrentMode = PlayerInputMode.Gameplay;
            _playerControls.EnableGameplayControls();
        }

        public void ActivateUiControls()
        {
            CurrentMode = PlayerInputMode.UI;
            _playerControls.EnableUiControls();
        }

        public void DeactivateAllControls()
        {
            CurrentMode = PlayerInputMode.Disabled;
            _playerControls.DisableAllControls();
        }
    }
}