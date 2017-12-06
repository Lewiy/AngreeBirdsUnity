using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using System.Collections.Generic;

public class FacebookManager : MonoBehaviour
{

    private static FacebookManager _instance;
    public static FacebookManager Instance
    {
        get{
            if (_instance == null)
            {
                GameObject fbm = new GameObject("FBManager");
                fbm.AddComponent<FacebookManager>();
            }

            return _instance;
        }


    }

    public bool IsLoggedIn { get; set; }
    public string ProfileName { get; set; }
    public Sprite ProfilePic { get; set; }
    public string AppLinkURL { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _instance = this;
        IsLoggedIn = true;
    }

    public void InitFB()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(SetInit, OnHideUnity);
        }
        else
        {
            //Debug.Log(FacebookManager.Instance.IsLoggedIn);
            IsLoggedIn = FB.IsLoggedIn;
        }
    }

    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("FB is log in");
            GetProfile();
        }
        else
        {
            Debug.Log("FB is not log in");
        }

        IsLoggedIn = FB.IsLoggedIn;
    }


    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    }

    public void GetProfile()
    {
        FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
        FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
        FB.GetAppLink(DealWithAppLink);
    }

    void DisplayUsername(IResult result)
    {
        

        if (result.Error == null)
        {
            ProfileName = "Hi, there, " + result.ResultDictionary["first_name"];
            Debug.Log("Hi, there, " + result.ResultDictionary["first_name"]);
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

    void DisplayProfilePic(IGraphResult results)
    {
        if (results.Texture != null)
        {
        

            ProfilePic = Sprite.Create(results.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
    }

    void DealWithAppLink(IAppLinkResult result)
    {
        if (!String.IsNullOrEmpty(result.Url))
        {
            AppLinkURL = result.Url;
        }
    }

    public void Share()
    {
        FB.FeedShare(
            string.Empty,
            new Uri(AppLinkURL),
            "Hello this is the title",
            "This is the caption",
            "Chek out this game",
            new Uri("https://blog.addthiscdn.com/wp-content/uploads/2015/11/logo-facebook.png"),
            string.Empty,
            ShareCallback
            );
    }

    void ShareCallback(IResult result)
    {
        if (result.Cancelled)
        {
            Debug.Log("Share cancelled");
        }else
        {
            if (!string.IsNullOrEmpty(result.Error))
            {
                Debug.Log("Error on share");
            }else if (!string.IsNullOrEmpty(result.RawResult))
            {
                Debug.Log("Success on share");
            }
        }
    }

    public void Invite()
    {
        FB.Mobile.AppInvite(
            new Uri(AppLinkURL),
             new Uri("https://blog.addthiscdn.com/wp-content/uploads/2015/11/logo-facebook.png"),
             InviteCallback);
    }

    void InviteCallback(IResult result)
    {
        if (result.Cancelled)
        {
            Debug.Log("Invite cancelled");
        }
        else
        {
            if (!string.IsNullOrEmpty(result.Error))
            {
                Debug.Log("Error on invite");
            }
            else if (!string.IsNullOrEmpty(result.RawResult))
            {
                Debug.Log("Success on invite");
            }
        }
    }

}