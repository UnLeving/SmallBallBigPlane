using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SmallBallBigPlane
{
    [CreateAssetMenu(fileName = "LevelsContainer", menuName = "LevelsContainer", order = 0)]
    public class LevelsSO : ScriptableObject
    {
        public AssetReferenceGameObject[] levelsAssetReferences;
    }
}