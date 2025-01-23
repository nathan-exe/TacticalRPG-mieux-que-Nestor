using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectExtensions
{
    public static float DistanceToPoint(this Rect r, Vector2 point)
    {
        point -= r.center;
        Vector2 d = point.AbsoluteValue() - r.size;
        return (VectorExtensions.Max(d, Vector2.zero)).magnitude + Mathf.Min(Mathf.Max(d.x, d.y), 0f);

    }
}
