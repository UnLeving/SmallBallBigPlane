using Cysharp.Threading.Tasks;

namespace SmallBallBigPlane
{
    public interface IInitializable
    {
        UniTask Initialize();
    }
}