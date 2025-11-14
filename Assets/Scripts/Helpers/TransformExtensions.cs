using System.Collections.Generic;
using UnityEngine;

namespace Helpers.Extensions
{
    public static class TransformExtensions
    {
        public static Vector3 DirectionTo(this Transform transform, Transform target)
        {
            return (target.position - transform.position).normalized;
        }

        public static IEnumerable<Transform> Children(this Transform parent)
        {
            foreach (Transform child in parent)
            {
                yield return child;
            }
        }
    }
}