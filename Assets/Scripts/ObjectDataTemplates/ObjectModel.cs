using UnityEngine;
using System.Xml.Serialization;

public class ObjectModel
{
    [XmlElement]
    public Vector3 position;
    [XmlElement]
    public Quaternion rotation;

    public ObjectModel()
    {

    }

    public ObjectModel(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}
