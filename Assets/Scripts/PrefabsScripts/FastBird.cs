using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class FastBird : Bird {


    public string typeBird;
    public string Type { get { return typeBird; } }

    public float throwSpeedFastBird;

    public override float ThrowSpeed
    {
        get
        {
            return throwSpeedFastBird;
        }
    }
}
