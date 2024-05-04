using System;
using UnityEngine;

[Serializable]
public abstract class Orbit
{
    public float eccentricity;

    public virtual Vector3 GetPoint(float theta)
    {
        float tan_theta = Mathf.Tan(theta);
        float x = Mathf.Sqrt(A_squared * B_squared / (B_squared + A_squared * (tan_theta * tan_theta)));
        if (theta <= Mathf.PI * 1.5f && theta >= Mathf.PI * 0.5f)
            x = -x;
        float y = x * tan_theta;

        return new Vector3(x, 0, y);
    }

    public Orbit(float eccentricity)
    {
        this.eccentricity = eccentricity;
    }


    public Vector3 Center
    {
        get
        {
            if (center == null)
                Debug.LogError("Orbit not initialized; call Precompute() on this orbit to init or update it");
            return center.Value;
        }
    }
    protected Vector3? center = null;
    public float C_squared
    {
        get
        {
            if (c_squared == null)
                Debug.LogError("Orbit not initialized; call Precompute() on this orbit to init or update it");
            return c_squared.Value;
        }
    }
    protected float? c_squared = null;

    public float A_squared
    {
        get
        {
            if (a_squared == null)
                Debug.LogError("Orbit not initialized; call Precompute() on this orbit to init or update it");
            return a_squared.Value;
        }
    }
    protected float? a_squared = null;

    public float B_squared
    {
        get
        {
            if (b_squared == null)
                Debug.LogError("Orbit not initialized; call Precompute() on this orbit to init or update it");
            return b_squared.Value;
        }
    }
    protected float? b_squared = null;

    public float Period
    {
        get
        {
            if (period == null)
                Debug.LogError("Orbit not initialized; call Precompute() on this orbit to init or update it");
            return period.Value;
        }
    }
    protected float? period = null;

    public abstract void Precompute();
}