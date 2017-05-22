using UnityEngine;
using System.Collections;

public class ObjectModel
{
    public Vector3 position;
    public Quaternion rotation;

    public ObjectModel(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}
