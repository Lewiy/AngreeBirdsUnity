using System.Xml.Serialization;
using System.IO;
using System;

public class MyXmlSerializer : ISaver {

public void Serialize(SaveScene state, string datapath) {
        Type[] extraTypes = { typeof(SavObject), typeof(BirdSaver), typeof(BrickSaver), typeof(PigSaver) };
        XmlSerializer serializer = new XmlSerializer(typeof(SaveScene), extraTypes);

        FileStream fs = new FileStream(datapath, FileMode.Create);
        serializer.Serialize(fs, state);
        fs.Close();
    }

 public    SaveScene Deserialize(string datapath) {
        Type[] extraTypes = { typeof(SavObject), typeof(BirdSaver), typeof(BrickSaver), typeof(PigSaver) };
        XmlSerializer serializer = new XmlSerializer(typeof(SaveScene), extraTypes);

        FileStream fs = new FileStream(datapath, FileMode.Open);
        SaveScene state = (SaveScene)serializer.Deserialize(fs);
        fs.Close();

        return state;
    }
}
