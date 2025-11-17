using Cysharp.Threading.Tasks;

namespace SmallBallBigPlane.UI.Windows
{
    public class LooseWindow : WindowBase
    {
        public override async UniTask Show()
        {
            if (isOpened) return;
        
            isOpened = true;
        
            await ShowPanel();
        }
        
        public override async UniTask Hide()
        {
            if (!isOpened) return;
        
            isOpened = false;
        
            await HidePanel();
        }
    }
}