using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coolBird : Bird {
    public string Type { get { return "CoolBird"; } }
    public override float ThrowSpeed
    {
        get
        {
            return 6;
        }
    }

    public override Vector2 Gravity
    {
        get
        {
            return new Vector2(0,-9);
        }
    }
   

}
