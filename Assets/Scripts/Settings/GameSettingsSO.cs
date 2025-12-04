using UnityEngine;

namespace SmallBallBigPlane
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings")]
    public class GameSettingsSO : ScriptableObject
    {
        [Header("Player Settings")]
        public float playerMoveSpeed = 2f;
        public float playerJumpPower = 4f;
        public float playerMoveSoundMaxVolume = 0.5f;
        public LayerMask playerGroundLayer;
        public float timeInAirBeforePlayerDie = 1f;
        public AudioClipSO playerMovingSound;
        public AnimationCurve volumeBySpeedCurve = AnimationCurve.Linear(0, 0, 1, 1);
        
        [Header("UI Settings")]
        public int restartDelayMS = 2500;

        [Header("WinWindow Settings")] 
        public int delayBetweenStarsMS = 500;
        
        [Header("UI sounds")]
        public AudioClipSO winSound;
        public AudioClipSO loseSound;
        
        [Header("Coin Settings")]
        public float coinRotationSpeed = 50f;
        
        [Header("Obstacle Settings")]
        public bool canObstaclePush;
        public LayerMask obstaclePushLayers;
        [Range(0.5f, 5f)] public float obstaclePushStrength = 4f;
        public AudioClipSO obstacleKickAudioClip;
    }
}