using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;


[XmlType("Brick")]
public class BrickSaver : SavObject {

    [XmlElement("TypeBrick")]
    public string type { get; set; }

    public BrickSaver() { }
    public BrickSaver(string name, Vector3 position,Quaternion rotation,string typeofPrefub, string type): base(name, position,rotation, typeofPrefub) {
        this.type = type;
    }
}
