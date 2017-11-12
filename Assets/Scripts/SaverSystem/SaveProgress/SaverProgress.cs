using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System;

public class SaverProgress  {

    static public void SaveXml(SaverObject state, string datapath)
    {

        //Type[] extraTypes = { typeof(SaverObject)};
        XmlSerializer serializer = new XmlSerializer(typeof(SaverObject));

        FileStream fs = new FileStream(datapath, FileMode.Create);
        serializer.Serialize(fs, state);
        fs.Close();

    }

    static public SaverObject DeXml()
    {
        string datapath = Application.dataPath + "/Saves/SavedDataProgress.xml";
        SaverObject state = null;

        if (File.Exists(datapath))
        {
           
            try
            {
              
                XmlSerializer serializer = new XmlSerializer(typeof(SaverObject));

                FileStream fs = new FileStream(datapath, FileMode.Open);
                state = (SaverObject)serializer.Deserialize(fs);
                fs.Close();
            }
            catch (Exception ex)
            {
                //Log exception here
                Debug.Log(ex.Message);
            }
        }

           
        return state;
    }
}
