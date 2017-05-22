using UnityEngine;
using System.Xml.Serialization;
using System.Collections.Generic;

public class RangeModel : MeleeModel
{
    [XmlElement]
    public List<Vector2> gunsPosition;
    [XmlElement]
    public List<float> gunsRotation;
    [XmlElement]
    public List<float> gunsScale;
    [XmlAttribute]
    public int gunCount;
    [XmlAttribute]
    public float shootingDelay;
    [XmlAttribute]
    public string bulletPrefab;

    public RangeModel()
    {

    }

    public RangeModel(Vector3 position, Quaternion rotation, Vector3 localScale, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, List<Vector2> gunsPosition, List<float> gunsRotation,List<float> gunsScale, int gunCount, float shootingDelay, float distanceToMove, float distanceOfVision, float hurtDelay, int scorePoints, string bulletPrefab) : 
        base(position, rotation, localScale, transparency, invisibility, invulnerable, health, maxHealth, distanceToMove, distanceOfVision, hurtDelay, scorePoints)
    {
        this.gunsPosition = gunsPosition;
        this.gunsRotation = gunsRotation;
        this.gunsScale = gunsScale;
        this.gunCount = gunCount;
        this.shootingDelay = shootingDelay;
        this.bulletPrefab = bulletPrefab;
    }
}
