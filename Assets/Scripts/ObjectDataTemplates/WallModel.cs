using UnityEngine;
using System.Collections;

public class WallModel : ObjectModel
{
    public Vector3 scale;
    public WallModel(Vector3 position, Quaternion rotation, Vector3 scale) : base(position,rotation)
    {
        this.scale = scale;
    }
}
