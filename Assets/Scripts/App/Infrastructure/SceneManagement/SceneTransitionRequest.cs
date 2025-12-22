namespace App
{
    public sealed class SceneTransitionRequest : ISceneTransitionRequest
    {
        public string TargetSceneName { get; set; }

        public void Clear()
        {
            TargetSceneName = null;
        }
    }
}