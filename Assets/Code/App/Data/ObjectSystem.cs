using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Solar System/Mesh System")]
public class ObjectSystem : ScriptableObject
{
    public Mesh mesh;
    public Material material;

    public string tag;
}
