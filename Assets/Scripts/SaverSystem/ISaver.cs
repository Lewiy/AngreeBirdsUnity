using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaver {

   void Serialize(SaveScene state, string datapath);

    SaveScene Deserialize(string datapath);
}
