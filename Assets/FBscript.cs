using System.Collections;
using UnityEngine;
using Facebook.Unity;
using System.Collections.Generic;

public class FBscript : MonoBehaviour {

	void Awake()
    {
        FB.Init(SetInit,OnHideUnity);////// bug
    }

    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("FB is log in");
        }else
        {
            Debug.Log("FB is not log in");
        }
    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }else
        {
            Time.timeScale = 1;
        }

    }

    public void FBLogin()
    {
        List<string> permisions = new List<string>();
        permisions.Add("public_profile");
        FB.LogInWithReadPermissions(permisions, AuthCallBack);
    }

    void AuthCallBack(IResult result)
    {
        if(result.Error != null)
        {
            Debug.Log(result.Error);
        }else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("FB is logged in");
            }else
            {
                Debug.Log("FB is not logged in");
            }
        }
    }
}
