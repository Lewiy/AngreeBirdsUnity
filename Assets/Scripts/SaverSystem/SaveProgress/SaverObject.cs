using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlType("Lamp")]
public class SaverObject {

    [XmlElement("Level")]
    public int Number { get; set; }

    // Use this for initialization

}
