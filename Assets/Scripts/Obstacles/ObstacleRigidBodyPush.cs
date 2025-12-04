using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    [RequireComponent(typeof(AudioSource))]
    public class ObstacleRigidBodyPush : MonoBehaviour
    {
        private AudioSource audioSource;
        private GameSettingsSO _gameSettings;
        
        private LayerMask PushLayers => _gameSettings.obstaclePushLayers;
        private bool CanPush => _gameSettings.canObstaclePush;
        private float Strength => _gameSettings.obstaclePushStrength;
        private AudioClipSO AudioClip => _gameSettings.obstacleKickAudioClip;
        
        [Inject]
        private void Construct(GameSettingsSO gameSettings)
        {
            this._gameSettings = gameSettings;
        }
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            
            audioSource.clip = AudioClip.audioClip;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (CanPush == false) return;

            PushRigidBodies(collision);
        }

        private void PushRigidBodies(Collision collision)
        {
            Rigidbody body = collision.rigidbody;

            if (body == null || body.isKinematic) return;

            var bodyLayerMask = 1 << body.gameObject.layer;
            if ((bodyLayerMask & PushLayers.value) == 0) return;

            ContactPoint contact = collision.GetContact(0);

            if (contact.normal.y > 0.3f) return;

            Vector3 pushDir = new Vector3(-contact.normal.x, 0.0f, -contact.normal.z);

            body.AddForce(pushDir * Strength, ForceMode.Impulse);
            
            audioSource.Play();
        }
    }
}