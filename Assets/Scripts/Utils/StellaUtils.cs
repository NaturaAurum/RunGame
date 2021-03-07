using UnityEngine;

namespace Stella.Utils
{
    public static class StellaUtils
    {
        public static Type FindDeep<Type>(this GameObject self, string name, bool includeInactive = false)
            where Type : Component
        {
            var child = self.GetComponentsInChildren<Transform>(includeInactive);
            foreach (var transform in child)
            {
                if (transform.name.Equals(name))
                {
                    return transform.GetComponent<Type>();
                }
            }

            return null;
        }
    }
}