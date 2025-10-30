using UnityEngine;

namespace SmallBallBigPlane
{
    [CreateAssetMenu(fileName = "New Audio Clip", menuName = "AudioClip")]
    public class AudioClipSO : ScriptableObject
    {
        public AudioClip audioClip;
    }
}