using System.Threading;
using Cysharp.Threading.Tasks;

namespace App
{
    public interface ISceneNavigator
    {
        UniTask GoToAsync(string sceneName, CancellationToken token);
    }
}