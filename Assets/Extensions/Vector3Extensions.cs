using UnityEngine;

/// <summary>
/// <para>
/// A collection of extension methods (or one method) for Vector3s. 
/// This is just here to show how C# Extension Methods work. They're cool!
/// </para><para>
/// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
/// </para>
/// </summary>
public static class Vector3Extensions
{
    /// <summary>
    /// <para>
    /// Divides the individual components of v0 by those of v1.
    /// </para><para>
    /// e.g. v0.ComponentDivide(v1) returns [ v0.x/v1.x, v0.y/v1.y, v0.z/v1.z ]
    /// </para><para>
    /// If any of the components of v1 are 0, then that component of v0 will
    /// remain unchanged to avoid divide by zero errors.
    /// </para>
    /// </summary>
    /// <returns>The Vector3 result of the ComponentDivide.</returns>
    /// <param name="v0">The numerator Vector3</param>
    /// <param name="v1">The denominator Vector3</param>
    static public Vector3 ComponentDivide(this Vector3 v0, Vector3 v1)
    {
        Vector3 vRes = v0;

        // Avoid divide by zero errors
        if (v1.x != 0)  
        {
            vRes.x = v0.x / v1.x;
        }
        if (v1.y != 0)
        {
            vRes.y = v0.y / v1.y;
        }
        if (v1.z != 0)
        {
            vRes.z = v0.z / v1.z;
        }

        return vRes;
    }

    /// <summary>
    /// Gets a random x , y point from a radius
    /// </summary>
    /// <param name="center">Center of the point</param>
    /// <param name="radius">Distance from the center</param>
    /// <returns></returns>
    static public Vector3 RandomCircle(this Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

}


