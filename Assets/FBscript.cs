using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using System.Collections.Generic;

public class FBscript : MonoBehaviour {

    public GameObject DialogLoggedIn;
    public GameObject DialogLoggedOut;
    public GameObject DialogUserName;
    public GameObject DialogProfilePic;
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

        DealWithFBMenus(FB.IsLoggedIn);
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
            DealWithFBMenus(FB.IsLoggedIn);
        }
    }

    void DealWithFBMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            DialogLoggedIn.SetActive(true);
            DialogLoggedOut.SetActive(false);

            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128",HttpMethod.GET,DisplayProfilePic);
        }else
        {
            DialogLoggedIn.SetActive(false);
            DialogLoggedOut.SetActive(true);
        }

    }

    void DisplayUsername(IResult result)
    {
        Text UserName = DialogUserName.GetComponent <Text>();

        if (result.Error == null)
        {
            UserName.text = "Hi, there, " + result.ResultDictionary["first_name"];
            Debug.Log("Hi, there, " + result.ResultDictionary["first_name"]);
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

    void DisplayProfilePic(IGraphResult results)
    {
        if(results.Texture != null)
        {
            Image ProfilePic = DialogProfilePic.GetComponent<Image>();

            ProfilePic.sprite = Sprite.Create(results.Texture, new Rect(0,0,128,128),new Vector2());
        }
    }

}
