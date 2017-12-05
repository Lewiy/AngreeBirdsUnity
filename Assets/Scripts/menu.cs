using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

    private SaverObject state;


    void Start()
    {
        state = SaverProgress.DeXml();

    }

    public void startGame()
    {
        Debug.Log("Press logs start");
        SceneManager.LoadScene(2);
    }

    public void exit()
    {
        Application.Quit();
    }

    public void continueF()
    {
        
        Debug.Log("Loading lavel (Menu)- "+ state.Number);

        LevelManeger.Instance.continueGameFlag = 1;

        SceneManager.LoadScene(state.Number);
    }

    void OnGUI()
    {
        GUI.contentColor = Color.red;
                GUI.Label(new Rect(800, 380, 400, 200), "Current progress - " + (state.Number - 1)  + " level");
            
    }
}
