using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coolBird : Bird {

    public string nameBird;
    public float throwSpeedCoolBird;
    public Vector2 gravity;
    public string Type { get { return nameBird; } }
    public override float ThrowSpeed
    {
        get
        {
            return throwSpeedCoolBird;
        }
    }

    public override Vector2 Gravity
    {
        get
        {
            return gravity;
        }
    }
   

}
