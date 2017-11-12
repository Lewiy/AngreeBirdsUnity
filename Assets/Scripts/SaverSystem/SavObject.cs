using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlType("PositionData")]
[XmlInclude(typeof(BirdSaver))]

[XmlInclude(typeof(BrickSaver))]

[XmlInclude(typeof(PigSaver))]
public class SavObject {

    protected GameObject _inst; 
    public GameObject inst { set { _inst = value; } }

    [XmlElement("Type")]
    public string name { get; set; }  

    [XmlElement("Position")]
    public Vector3 position { get; set; }

    [XmlElement("Rotation")]
    public Quaternion rotation { get; set; }

    [XmlElement("TypeOfPrefub")]
    public string typeOfPrefub { get; set; }

    public SavObject() { }

    public SavObject(string name, Vector3 position,Quaternion rotation, string typeOfPrefub)
    {
        this.name = name;
        this.position = position;
        this.rotation = rotation;
        this.typeOfPrefub = typeOfPrefub;

    }

    public virtual void Estate() { }   

    public virtual void Update()
    {
        if(_inst !=null)
        position = _inst.transform.position;
        rotation = _inst.transform.rotation;
       
    }
}
