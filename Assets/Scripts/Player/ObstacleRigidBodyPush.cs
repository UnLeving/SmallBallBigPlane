using System;
using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    [RequireComponent(typeof(AudioSource))]
    public class ObstacleRigidBodyPush : MonoBehaviour
    {
        [SerializeField] private LayerMask pushLayers;
        [SerializeField] private bool canPush;
        [Range(0.5f, 5f)] [SerializeField] private float strength = 1.1f;
        [SerializeField] private AudioClipSO audioClip;
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            
            audioSource.clip = audioClip.audioClip;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (canPush == false) return;

            PushRigidBodies(collision);
        }

        private void PushRigidBodies(Collision collision)
        {
            Rigidbody body = collision.rigidbody;

            if (body == null || body.isKinematic) return;

            var bodyLayerMask = 1 << body.gameObject.layer;
            if ((bodyLayerMask & pushLayers.value) == 0) return;

            ContactPoint contact = collision.GetContact(0);

            if (contact.normal.y > 0.3f) return;

            Vector3 pushDir = new Vector3(-contact.normal.x, 0.0f, -contact.normal.z);

            body.AddForce(pushDir * strength, ForceMode.Impulse);
            
            audioSource.Play();
        }
    }
}