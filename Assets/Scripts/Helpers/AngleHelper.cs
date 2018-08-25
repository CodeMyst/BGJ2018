using UnityEngine;

namespace BGJ2018.Helpers
{
    public static class AngleHelper
    {
        public static float ClampAngle (float angle, float from, float to)
        {
            if (angle < 0f) angle = 360 + angle;
            if (angle > 180f) return Mathf.Max (angle, 360 + from);
            return Mathf.Min (angle, to);
        }
    }
}