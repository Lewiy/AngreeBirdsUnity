using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

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

    }
}
