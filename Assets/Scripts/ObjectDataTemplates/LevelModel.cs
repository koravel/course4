using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;

public class LevelModel
{ 
    [XmlArray]
    [XmlArrayItem]
    public List<MeleeModel> meleeList = new List<MeleeModel>();
    [XmlArray]
    [XmlArrayItem]
    public List<RangeModel> rangeList = new List<RangeModel>();
    [XmlArray]
    [XmlArrayItem]
    public List<WallModel> wallList = new List<WallModel>();
    [XmlElement]
    public PlayerModel playerObj;
    [XmlAttribute]
    public string backgroundSpritePath;
    [XmlAttribute]
    public int levelScore;
    [XmlAttribute]
    public int levelTime;
}
