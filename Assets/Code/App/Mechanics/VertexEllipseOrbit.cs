using System;
using UnityEngine;

[Serializable]
public class VertexEllipseOrbit : Orbit
{
    public Vector3 vertex1;
    public Vector3 vertex2;

    public VertexEllipseOrbit(Vector3 vertex1, Vector3 vertex2, float eccentricity) : base(eccentricity)
    {
        this.vertex1 = vertex1;
        this.vertex2 = vertex2;
    }

    public override void Precompute()
    {
        center = (vertex1 + vertex2) * 0.5f;
        a_squared = (vertex2 - vertex1).sqrMagnitude;
        c_squared = a_squared * eccentricity * eccentricity;
        b_squared = a_squared - c_squared;
        period = a_squared * Mathf.Sqrt(a_squared.Value); // a^3 for yr^2 (sideyear) per AU (astronomical unit)
    }
}