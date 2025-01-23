using System.Runtime.CompilerServices;
using UnityEngine;

public static class VectorExtensions
{

    //rounding
    public static Vector3Int Round(this Vector3 v)
    {
        return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
    }

    public static Vector2Int Round(this Vector2 v)
    {
        return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
    }

    public static Vector2Int RoundToV2Int(this Vector3 v)
    {
        return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
    }

    public static Vector2Int Ceil(this Vector2 v)
    {
        return new Vector2Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
    }

    public static Vector2Int Floor(this Vector2 v)
    {
        return new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
    }

    //Distance
    public static float SqrDistanceTo(this Vector3 u,Vector3 v)
    {
        return (u - v).sqrMagnitude;
    }

    public static float SqrDistanceTo(this Vector2 u, Vector2 v)
    {
        return (u - v).sqrMagnitude;
    }


    //Swizzle
    public static Vector2 XZ(this Vector3 v)
    {
        return new Vector2(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.z));
    }

    public static Vector3 X0Y(this Vector2 v)
    {
        return new Vector3(v.x,0,v.y);
    }

    public static Vector3Int X0Y(this Vector2Int v)
    {
        return new Vector3Int(v.x, 0, v.y);
    }

    //additional math
    public static Vector2 AbsoluteValue(this Vector2 v)
    {
        return new Vector2(Mathf.Abs(v.x),Mathf.Abs(v.y));
    }

    //comparaison
    public static Vector2 Max(Vector2 a,Vector2 b)
    {
        return new Vector2(Mathf.Max(a.x,b.x),Mathf.Max(a.y,b.y));
    }

    public static Vector2 Min(Vector2 a, Vector2 b)
    {
        return new Vector2(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y));
    }
}
