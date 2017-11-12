using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;


[XmlType("Pig")]
public class PigSaver : SavObject {

    [XmlElement("TypePig")]
    public string type { get; set; }

    public PigSaver() { }

    public PigSaver(string name, Vector3 position, Quaternion rotation,string typeOfPrefub, string type): base(name, position,rotation,typeOfPrefub) {
        this.type = type;
    }
}
