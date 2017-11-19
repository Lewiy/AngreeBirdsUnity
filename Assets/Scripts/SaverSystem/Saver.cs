using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Saver {

    static public void SaveXml(ISaver saver, SaveScene state, string datapath)
    {
        saver.Serialize( state, datapath);
    }

    static public SaveScene DeXml(ISaver saver, string datapath)
    {
        return saver.Deserialize(datapath);
    }
}
