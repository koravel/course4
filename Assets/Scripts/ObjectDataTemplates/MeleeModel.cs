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
    [XmlAttribute]
    public float hurtDelay;
    [XmlAttribute]
    public int scorePoints;
    
    public MeleeModel() : base()
    {

    }

    public MeleeModel(Vector3 position, Quaternion rotation,Vector3 localScale, bool transparency, bool invisibility, bool invulnerable, float health, float maxHealth, float distanceToMove, float distanceOfVision, float hurtDelay, int scorePoints) : base(position, rotation,localScale)
    {
        this.transparency = transparency;
        this.invisibility = invisibility;
        this.invulnerable = invulnerable;
        this.health = health;
        this.maxHealth = maxHealth;
        this.distanceToMove = distanceToMove;
        this.distanceOfVision = distanceOfVision;
        this.hurtDelay = hurtDelay;
        this.scorePoints = scorePoints; 
    }
}
