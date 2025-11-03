using Cysharp.Threading.Tasks;

namespace SmallBallBigPlane
{
    public class LooseWindow : WindowBase
    {
        public override UniTask Hide()
        {
            gameObject.SetActive(false);
            
            isOpened = false;
            
            return UniTask.CompletedTask;
        }
    }
}