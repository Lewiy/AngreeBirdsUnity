using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;
using System.IO;

public class Saver {
 
    static public void SaveXml(SaveScene state, string datapath)
    {
        Type[] extraTypes = { typeof(SavObject), typeof(BirdSaver), typeof(BrickSaver), typeof(PigSaver) };
        XmlSerializer serializer = new XmlSerializer(typeof(SaveScene), extraTypes);

        FileStream fs = new FileStream(datapath, FileMode.Create);
        serializer.Serialize(fs, state);
        fs.Close();

    }

    static public SaveScene DeXml(string datapath)
    {
//
        Type[] extraTypes = { typeof(SavObject), typeof(BirdSaver), typeof(BrickSaver), typeof(PigSaver) };
        XmlSerializer serializer = new XmlSerializer(typeof(SaveScene), extraTypes);

        FileStream fs = new FileStream(datapath, FileMode.Open);
        SaveScene state = (SaveScene)serializer.Deserialize(fs);
        fs.Close();

        return state;
    }
}
