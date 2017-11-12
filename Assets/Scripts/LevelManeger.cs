using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManeger
{

    public int indexLEvel;

    public int continueGameFlag = 0;

    public LevelManeger()
    {
        indexLEvel = 2;
    }

    public void plasLevel()
    {
       
        indexLEvel = indexLEvel + 1;
        Debug.Log("Plas level" + indexLEvel);
        if (indexLEvel == 5)
            indexLEvel = 1;
    }

    public static LevelManeger instance;

    public static LevelManeger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LevelManeger();
            }
            return instance;
        }
    }
}
