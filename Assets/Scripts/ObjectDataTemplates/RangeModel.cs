using UnityEngine;
using System.Xml.Serialization;
using System.Collections.Generic;

public class RangeModel : MeleeModel
{
    [XmlElement]
    public List<Vector2> gunsPosition;
    [XmlElement]
    public List<Quaternion> gunsRotation;
    [XmlAttribute]
    public int gunCount;
    [XmlAttribute]
    public float shootingDelay;

    public RangeModel()
    {

    }

    public RangeModel(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, List<Vector2> gunsPosition, List<Quaternion> gunsRotation, int gunCount, float shootingDelay, float distanceToMove, float distanceOfVision) : 
        base(position, rotation, transparency, invisibility, invulnerable, health, maxHealth, distanceToMove, distanceOfVision)
    {
        this.gunsPosition = gunsPosition;
        this.gunsRotation = gunsRotation;
        this.gunCount = gunCount;
        this.shootingDelay = shootingDelay;
    }
}
