using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SmallBallBigPlane.UI.Windows
{
    public abstract class UIContainer : MonoBehaviour
    {
        public abstract UniTask Show();

        public abstract UniTask Hide();
    }
}