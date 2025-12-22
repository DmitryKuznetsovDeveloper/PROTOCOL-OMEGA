namespace App
{
    public interface ISceneTransitionRequest
    {
        string TargetSceneName { get; set; }
        void Clear();
    }
}