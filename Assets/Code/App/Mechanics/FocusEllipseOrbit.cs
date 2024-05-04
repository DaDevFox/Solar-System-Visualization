using System;
using UnityEngine;

[Serializable]
public class FocusEllipseOrbit : Orbit
{
    public Vector3 focus1;
    public Vector3 focus2;

    public FocusEllipseOrbit(Vector3 focus1, Vector3 focus2, float eccentricity) : base(eccentricity)
    {
        this.focus1 = focus1;
        this.focus2 = focus2;
    }

    public override void Precompute()
    {
        center = (focus1 + focus2) * 0.5f;
        c_squared = (focus1 - center.Value).sqrMagnitude;
        a_squared = c_squared / (eccentricity * eccentricity);
        b_squared = a_squared - c_squared;
        period = a_squared * Mathf.Sqrt(a_squared.Value); // a^3 for yr^2 (sideyear) per AU (astronomical unit)
    }
}