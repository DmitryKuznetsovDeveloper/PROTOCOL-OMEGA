namespace App
{
    public sealed class LookSettings
    {
        public float Sensitivity { get; }
        public bool InvertY { get; }

        public LookSettings(float sensitivity, bool invertY)
        {
            Sensitivity = sensitivity;
            InvertY = invertY;
        }
    }
}