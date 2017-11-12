using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class FastBird : Bird {
    public string Type { get { return "FastBird"; } }
    public override float ThrowSpeed
    {
        get
        {
            return 20;
        }
    }
}
