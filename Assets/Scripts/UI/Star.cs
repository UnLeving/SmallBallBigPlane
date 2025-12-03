using System;
using UnityEngine;

namespace SmallBallBigPlane.UI
{
    public sealed class Star : MonoBehaviour
    {
        [SerializeField] private GameObject activeObject;
        [SerializeField] private GameObject inactiveObject;
        [SerializeField] private ParticleSystem particles;

        private void Awake()
        {
            Deactivate();
        }

        public void Activate()
        {
            activeObject.SetActive(true);
            particles.Play();
        }

        public void Deactivate()
        {
            activeObject.SetActive(false);
        }
    }
}