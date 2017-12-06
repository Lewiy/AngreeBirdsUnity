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

        FacebookManager.Instance.InitFB();
        DealWithFBMenus(FB.IsLoggedIn);
        Debug.Log(FacebookManager.Instance.IsLoggedIn);
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
                FacebookManager.Instance.IsLoggedIn = true;
                FacebookManager.Instance.GetProfile();
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

             if(FacebookManager.Instance.ProfileName != null)
                {

                Text UserName = DialogUserName.GetComponent<Text>();
                UserName.text = "" + FacebookManager.Instance.ProfileName;
            }else
            {
                StartCoroutine("WaitForProfileName");
            }


            if (FacebookManager.Instance.ProfilePic != null)
            {

                Image ProfilePic = DialogProfilePic.GetComponent<Image>();
                ProfilePic.sprite = FacebookManager.Instance.ProfilePic;
            }
            else
            {
                StartCoroutine("WaitForProfilePic");
            }




        }
        else
        {
            DialogLoggedIn.SetActive(false);
            DialogLoggedOut.SetActive(true);


        }

    }

   

    IEnumerator WaitForProfileName()
    {
        while(FacebookManager.Instance.ProfileName == null)
        {
            yield return null;
        }

        DealWithFBMenus(FB.IsLoggedIn);
    }

    IEnumerator WaitForProfilePic()
    {
        while (FacebookManager.Instance.ProfilePic == null)
        {
            yield return null;
        }

        DealWithFBMenus(FB.IsLoggedIn);
    }
    /*void Update()
    {
        if(FacebookManager.Instance.ProfileName == null)
        {

        }
    }*/

    public void Share()
    {
        FacebookManager.Instance.Share();
    }
}
