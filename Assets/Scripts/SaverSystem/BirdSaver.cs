using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;


[XmlType("Bird")]
public class BirdSaver : SavObject {

    [XmlElement("TypeBird")]
    public string type { get; set; }

    public BirdSaver() { }
    public BirdSaver(string name, Vector3 position, Quaternion rotation,string typeOfPrefub, string type): base(name, position, rotation,typeOfPrefub) {
        this.type = type;
    }

}
