using System;
using UnityEngine;

namespace SmallBallBigPlane.UI.AdWheel
{
    public sealed class AdWheelArrow : MonoBehaviour
    {
        [SerializeField] private float speed = 200f;
        [SerializeField] private float angle;
        [SerializeField] private bool _enabled;

        public event Action<float> OnValueChanged;
        
        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            _enabled = false;
        }

        private void Update()
        {
            if(!_enabled) return;
            
            angle += speed * Time.deltaTime;

            if (Mathf.Abs(angle) >= 90f)
            {
                speed *= -1;
                angle = Mathf.Sign(angle) * 90f;
            }
            
            OnValueChanged?.Invoke(angle);
            
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}