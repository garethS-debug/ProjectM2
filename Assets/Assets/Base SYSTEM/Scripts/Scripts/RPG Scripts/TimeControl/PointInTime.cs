
using UnityEngine;

public class PointInTime  
{

    public Vector3 position; //stores position
    public Quaternion rotation; // storing rotation

    public PointInTime (Vector3 _position, Quaternion _rotation)
    {
        position = _position;
            rotation = _rotation;
    }
}
