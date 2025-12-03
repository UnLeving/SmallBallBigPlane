using System;
using UnityEngine;

namespace SmallBallBigPlane
{
    public sealed class UICamera : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera  { get; private set; }

        private void Awake()
        {
            if (!Camera)
            {
                Debug.LogError($"UICamera {name} is missing");
            }
        }
    }
}