using UnityEngine;
using System.Xml.Serialization;

public class ObjectModel
{
    [XmlElement]
    public Vector3 position;
    [XmlElement]
    public Quaternion rotation;
    [XmlElement]
    public Vector3 localScale;

    public ObjectModel()
    {

    }

    public ObjectModel(Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        this.position = position;
        this.rotation = rotation;
        this.localScale = localScale;
    }
}
