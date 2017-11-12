using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("SaveScene")]
[XmlInclude(typeof(SavObject))]
public class SaveScene{

    [XmlArray("Furniture")]
    [XmlArrayItem("FurnitureObject")]
    public List<SavObject> objects = new List<SavObject>();


    public SaveScene()
    {

    }

    public void AddItem(SavObject item)
    {  
       objects.Add(item);          
    }


    public void Update()
    {   
        foreach (SavObject felt in objects) 
            felt.Update();
    }
}
