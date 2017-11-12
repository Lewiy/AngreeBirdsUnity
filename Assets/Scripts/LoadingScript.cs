using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScript : MonoBehaviour {

    public KeyCode _keyCode = KeyCode.Space;
    public GameObject loadingInfo, loadingIcon;
    private AsyncOperation async;

    IEnumerator Start()
    {
        async = SceneManager.LoadSceneAsync(1);
       // loadingIcon.SetActive(true);
       // loadingInfo.SetActive(false);
        yield return true;
        async.allowSceneActivation = false;
      //  loadingIcon.SetActive(false);
      //  loadingInfo.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(_keyCode)) async.allowSceneActivation = true;
    }

    void OnGUI()
    {
        GUI.contentColor = Color.red;
        GUI.Label(new Rect(400, 120, 400, 200), "Press space for start game...");

    }

}
