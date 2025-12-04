using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane.UI
{
    public sealed class StarsHandler : MonoBehaviour
    {
        [SerializeField] private Star[] stars;
        [Inject] private readonly GameSettingsSO gameSettingsSO;
        private int Delay => gameSettingsSO.delayBetweenStarsMS;
        
        public async UniTask Show(int count)
        {
            for (int i = 0; i < count; i++)
            {
                stars[i].Activate();

                await UniTask.Delay(Delay);
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