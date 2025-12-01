using Cysharp.Threading.Tasks;

namespace SmallBallBigPlane.Interactables
{
    public interface IInteractable
    {
        UniTask Interact();
    }
}