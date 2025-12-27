namespace App
{
    public enum PlayerInputMode
    {
        Disabled = 0,
        Gameplay = 1,
        UI = 2
    }

    public interface IPlayerInputContext
    {
        PlayerInputMode CurrentMode { get; }

        void ActivateGameplayControls();
        void ActivateUiControls();
        void DeactivateAllControls();
    }
}