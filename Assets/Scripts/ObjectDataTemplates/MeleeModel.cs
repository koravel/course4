using UnityEngine;
using System.Xml.Serialization;

public class MeleeModel : ObjectModel
{
    [XmlAttribute]
    public bool transparency;
    [XmlAttribute]
    public bool invisibility;
    [XmlAttribute]
    public bool invulnerable;
    [XmlAttribute]
    public float health;
    [XmlAttribute]
    public float maxHealth;
    [XmlAttribute]
    public float distanceToMove;
    [XmlAttribute]
    public float distanceOfVision;

    public MeleeModel() : base()
    {

    }

    public MeleeModel(Vector3 position, Quaternion rotation, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, float distanceToMove, float distanceOfVision) : base(position, rotation)
    {
        this.transparency = transparency;
        this.invisibility = invisibility;
        this.invulnerable = invulnerable;
        this.health = health;
        this.maxHealth = maxHealth;
        this.distanceToMove = distanceToMove;
        this.distanceOfVision = distanceOfVision;
    }
}
