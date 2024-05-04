using System;
using UnityEngine;


[CreateAssetMenu(menuName = "Solar System/CelestialObject")]
public class CelestialObject : ScriptableObject
{
    public Color baseColor = Color.white;
    public float scale = 1f;

    [NonSerialized]
    public float current_theta = 0f;
    public VertexEllipseOrbit orbit;
    public ObjectSystem system;
}
