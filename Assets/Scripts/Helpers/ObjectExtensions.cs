using UnityEngine;

namespace HelpersAndExtensions
{
    public static class ObjectExtensions
    {
        public static void Activate(this GameObject gameObject)
        {
            if (gameObject) gameObject.SetActive(true);
        }

        public static void Deactivate(this GameObject gameObject)
        {
            if (gameObject) gameObject.SetActive(false);
        }

        public static void Activate(this Component component)
        {
            if (component) component.gameObject.SetActive(true);
        }

        public static void Deactivate(this Component component)
        {
            if (component) component.gameObject.SetActive(false);
        }
    }
}