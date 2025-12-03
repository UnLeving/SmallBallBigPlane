using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SmallBallBigPlane.UI
{
    public sealed class StarsHandler : MonoBehaviour
    {
        [SerializeField] private Star[] stars;
        [SerializeField] private int delay = 500; // todo move to so config
        
        public async UniTask Show(int count)
        {
            for (int i = 0; i < count; i++)
            {
                stars[i].Activate();

                await UniTask.Delay(delay);
            }
        }

        public void Hide()
        {
            foreach (var star in stars)
            {
                star.Deactivate();
            }
        }
    }
}