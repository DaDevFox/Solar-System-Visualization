using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Graphics;
using System.Linq;
using System;
using UnityEngine.UIElements;

public class Planets : MonoBehaviour
{
    [Header("Scene")]
    public string planetsDirectory;
    [Tooltip("If planetsDirectory is not \"\", this is used")]
    public Planets[] planets;

    [Header("Settings")]
    [Tooltip("yrs (sideyears) per thousand seconds")]
    public float timescale = 1f;

    private MultiMeshSystem _graphicsSystem = new MultiMeshSystem();
    private List<Tuple<int, int>> _graphicsObjects = new List<Tuple<int, int>>();
    private ICollection<CelestialObject> _objects;

    void Start()
    {
        _objects = Resources.LoadAll<CelestialObject>(planetsDirectory);
        foreach (CelestialObject obj in _objects)
        {
            obj.orbit.Precompute();

            if (!_graphicsSystem.HasSystem(obj.system.tag))
                _graphicsSystem.AddSystem(obj.system.tag, obj.system.mesh, obj.system.material);

            InstanceMeshSystemV2.ItemData data = _graphicsSystem[obj.system.tag].Add(Vector3.zero, Quaternion.identity, new Vector3(obj.scale, obj.scale, obj.scale));
            _graphicsObjects.Add(new Tuple<int, int>(data.matrix, data.id));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            CelestialObject obj = _objects.ElementAt(i);
            Vector3 pos = obj.orbit.GetPoint(obj.current_theta);
            _graphicsSystem[obj.system.tag].Set(_graphicsObjects[i].Item1, _graphicsObjects[i].Item2, pos);

            float diffX = pos.x - obj.orbit.Center.x;
            float diffZ = pos.z - obj.orbit.Center.z;
            float radius_squared = diffX * diffX + diffZ * diffZ;
            float angular_velocity = 2f * Mathf.PI * Mathf.Sqrt(obj.orbit.A_squared * obj.orbit.B_squared) / (radius_squared * obj.orbit.Period);

            obj.current_theta += angular_velocity * Time.deltaTime * timescale;
            if (obj.current_theta > 2f * Mathf.PI)
                obj.current_theta %= 2f * Mathf.PI;
            if (obj.current_theta < 0.0F)
                obj.current_theta = -obj.current_theta;
        }
        _graphicsSystem.Update();
    }

    private static int debugObjIndex = -1;

    private void OnDrawGizmos()
    {
        if (debugObjIndex == -1)
            return;

        CelestialObject obj = _objects.ElementAt(debugObjIndex);
        //Debug.DrawRay(obj.orbit.focus1, new Vector3(0f, 3f, 0f), Color.green);
        //Debug.DrawRay(obj.orbit.focus2, new Vector3(0f, 3f, 0f), Color.green);
        Debug.DrawRay(obj.orbit.Center, new Vector3(0f, 3f, 0f), Color.yellow);

        Vector3 pos = obj.orbit.GetPoint(obj.current_theta);
        Vector3 xvec = new Vector3(pos.x - obj.orbit.Center.x, 0f, 0f);
        Vector3 yvec = new Vector3(0f, 0f, pos.z - obj.orbit.Center.z);
        Debug.DrawLine(xvec, xvec + new Vector3(0f, 1f, 0f), Color.red);
        Debug.DrawLine(yvec, yvec + new Vector3(0f, 1f, 0f), Color.red);

        float diffX = pos.x - obj.orbit.Center.x;
        float diffZ = pos.z - obj.orbit.Center.z;
        float radius_squared = diffX * diffX + diffZ * diffZ;
        Debug.DrawLine(Vector3.zero, new Vector3(0f, 0f, obj.current_theta / (2 * Mathf.PI)), Color.white);
        Debug.DrawLine(Vector3.one, new Vector3(1f, 1f, 1f + radius_squared) / 5f, Color.white);
    }
}
