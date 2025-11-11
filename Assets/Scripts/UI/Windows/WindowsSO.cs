using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SmallBallBigPlane
{
    [CreateAssetMenu(fileName = "WindowsContainer", menuName = "ScriptableObjects/WindowsContainer", order = 0)]
    public class WindowsSO : ScriptableObject
    {
        public AssetReferenceGameObject[] windowsReferences;
    }
}