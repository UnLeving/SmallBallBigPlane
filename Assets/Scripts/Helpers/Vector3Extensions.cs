using UnityEngine;

namespace Helpers.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }

        public static Vector3 Add(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(original.x + (x ?? 0), original.y + (y ?? 0), original.z + (z ?? 0));
        }

        public static bool IsBehind(this Vector3 queried, Vector3 forward)
        {
            return Vector3.Dot(queried, forward) < 0;
        }
    }
}