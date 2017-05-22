using UnityEngine;
using System.Xml.Serialization;

public class WallModel : ObjectModel
{
    [XmlElement]
    public Vector3 scale;

    public WallModel() : base()
    {

    }

    public WallModel(Vector3 position, Quaternion rotation, Vector3 scale) : base(position,rotation)
    {
        this.scale = scale;
    }
}
