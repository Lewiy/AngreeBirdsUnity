using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    public CameraFollow cameraFollow;
    int currentBirdIndex;
    public SlingShot slingshot;
    [HideInInspector]
    public static GameState CurrentGameState = GameState.Start;
    private List<GameObject> Bricks;
    private List<GameObject> Birds;
    private List<GameObject> Pigs;
    private List<GameObject> GameObjectForSaving;
   
    SaveScene stateSaving = new SaveScene();
    private SaveScene state = new SaveScene();
    private string dataPath;
    private string dataPathProgressSavior;

    private MyXmlSerializer myXmlSerializer = new MyXmlSerializer(); // Creating own storage system
    void Start()
    {

        dataPath = Application.dataPath + "/Saves/SavedData.xml";
        dataPathProgressSavior = Application.dataPath + "/Saves/SavedDataProgress.xml";


        Bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        Birds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bird"));
        Pigs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pig"));

        GameObjectForSaving = new List<GameObject>();

        GameObjectForSaving.AddRange(Bricks);
        GameObjectForSaving.AddRange(Birds);
        GameObjectForSaving.AddRange(Pigs);


        if (LevelManeger.Instance.continueGameFlag == 1)
            contioueGame();

        CurrentGameState = GameState.Start;
        slingshot.enabled = false;
        
        slingshot.BirdThrown -= Slingshot_BirdThrown; slingshot.BirdThrown += Slingshot_BirdThrown;

     
        
    }

    void contioueGame()
    {
        destroyBeforeLoading();

        Birds.Clear();
        Bricks.Clear();
        Pigs.Clear();

        if (File.Exists(dataPath))
        {
         
            state = Saver.DeXml(myXmlSerializer,dataPath); // Deserializating with own type of storage
        }
  

        GenerateFromXML();

    }

    void destroyBeforeLoading()
    {
        foreach (GameObject destroyable in  GameObjectForSaving) {

            DestroyImmediate(destroyable);
        }
    }
    void GenerateFromXML()
    {
        foreach (SavObject felt in state.objects)
        {  

            Debug.Log("Instanciate object - " + "Prefabs/" + felt.name);

            GameObject prefab = new GameObject();

             felt.inst = prefab = Instantiate(Resources.Load("Prefabs/" + felt.name), felt.position, felt.rotation) as GameObject;

            prefab.name = felt.name;

            prefab.transform.parent = transform;

            prefab.gameObject.tag = felt.typeOfPrefub;

            Debug.Log("Tags  - " + prefab.gameObject.tag);

          
            felt.Estate(); 
        }

        Birds.Clear();
        Bricks.Clear();
        Pigs.Clear();
        Debug.Log("Birds count  - " + Birds.Count);

        Bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        Birds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bird"));

        Pigs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pig"));


        GameObjectForSaving.AddRange(Bricks);
        GameObjectForSaving.AddRange(Birds);
        GameObjectForSaving.AddRange(Pigs);

        Debug.Log("Birds count  - " + Birds.Count);
    }

    void ReadInfForSerial()
    {
       
        foreach (GameObject gameObject in GameObjectForSaving)
   
        {
            if (gameObject != null)
            {

              
                  SavObject savObject;

                  string nameOfPrefab =
                 PrefabUtility.GetPrefabParent(gameObject)  + "";
                if (nameOfPrefab == "")
                    nameOfPrefab = gameObject.name;
                else 

                nameOfPrefab = nameOfPrefab.Substring(0, nameOfPrefab.IndexOf(" "));
                
                Debug.Log("Name of prefub - " + nameOfPrefab);
             
               
                switch (gameObject.tag)
                {
                    case "Bird":
                        savObject = new BirdSaver(nameOfPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), 
                           gameObject.transform.rotation, gameObject.tag, gameObject.tag);
                        break;
                    case "Pig":
                        savObject = new PigSaver(nameOfPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z),
                              gameObject.transform.rotation, gameObject.tag, gameObject.tag);
                        break;
                    case "Brick":
                        savObject = new BrickSaver(nameOfPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z),
                              gameObject.transform.rotation, gameObject.tag, gameObject.tag);
                        break;
                    default:
                        savObject = new BrickSaver(nameOfPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z),
                              gameObject.transform.rotation, gameObject.tag, gameObject.tag);
                        break;
                }
                
                savObject.inst = gameObject;


                Debug.Log("savObject - " + savObject);
                stateSaving.AddItem(savObject);


            }
              
        }

        Debug.Log("Count objects saving - " + stateSaving.objects.Count);
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Escape))
        {

            ReadInfForSerial();

            stateSaving.Update();

            Saver.SaveXml(myXmlSerializer, stateSaving, dataPath); // Saving with own type of storage

            SaverObject progressObj = new SaverObject();

            progressObj.Number = Application.loadedLevel;

            SaverProgress.SaveXml(progressObj, dataPathProgressSavior);

            SceneManager.LoadScene(1);
        }

        switch (CurrentGameState)
        {
            case GameState.Start:
               
                if (Input.GetMouseButtonUp(0))
                {
                    AnimateBirdToSlingshot();
                }
                break;
            case GameState.BirdMovingToSlingshot:
                
                break;
            case GameState.Playing:
               
                if (slingshot.slingshotState == SlingshotState.BirdFlying &&
                    (BricksBirdsPigsStoppedMoving() || Time.time - slingshot.TimeSinceThrown > 5f))
                {
                    slingshot.enabled = false;
                    AnimateCameraToStartPosition();
                    CurrentGameState = GameState.BirdMovingToSlingshot;
                }
                break;
           
            case GameState.Won:
                
                if (Input.GetMouseButtonUp(0))
                {
                    
                   LevelManeger.Instance.plasLevel();
                    LevelManeger.Instance.continueGameFlag = 0;
                    Debug.Log("Loaded level" + LevelManeger.Instance.indexLEvel);
                    SceneManager.LoadScene(LevelManeger.Instance.indexLEvel);
                }

                break;

            case GameState.Lost:
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("Game lost");
                    Application.LoadLevel(Application.loadedLevel);
                }
                break;
            default:
                break;
        }
    }

    private bool AllPigsDestroyed()
    {
        return Pigs.All(x => x == null);
    }

    private void AnimateCameraToStartPosition()
    {
        float duration = Vector2.Distance(Camera.main.transform.position, cameraFollow.StartingPosition) / 10f;
        if (duration == 0.0f) duration = 0.1f;

        Camera.main.transform.positionTo
            (duration,
            cameraFollow.StartingPosition). 
            setOnCompleteHandler((x) =>
                        {
                            cameraFollow.IsFollowing = false;
                            if (AllPigsDestroyed())
                            {
                                CurrentGameState = GameState.Won;
                            }
                        
                            else if (currentBirdIndex == Birds.Count - 1)
                            {
                               
                                CurrentGameState = GameState.Lost;
                            }
                            else
                            {
                                slingshot.slingshotState = SlingshotState.Idle;
                              
                                currentBirdIndex++;
                                AnimateBirdToSlingshot();
                            }
                        });
    }
     
   
    void AnimateBirdToSlingshot()
    {
        CurrentGameState = GameState.BirdMovingToSlingshot;
        Birds[currentBirdIndex].transform.positionTo
            (Vector2.Distance(Birds[currentBirdIndex].transform.position / 10,
            slingshot.BirdWaitPosition.transform.position) / 10, 
            slingshot.BirdWaitPosition.transform.position). 
                setOnCompleteHandler((x) =>
                        {
                            x.complete();
                            x.destroy(); 
                            CurrentGameState = GameState.Playing;
                            slingshot.enabled = true; 
                            slingshot.BirdToThrow = Birds[currentBirdIndex];
                        });
    }

    private void Slingshot_BirdThrown(object sender, System.EventArgs e)
    {
        cameraFollow.BirdToFollow = Birds[currentBirdIndex].transform;
        cameraFollow.IsFollowing = true;
    }

 
    bool BricksBirdsPigsStoppedMoving()
    {
        foreach (var item in Bricks.Union(Birds).Union(Pigs))
        {
            if (item != null && item.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > Constants.MinVelocity)
            {
                return false;
            }
        }

        return true;
    }

   
    public static void AutoResize(int screenWidth, int screenHeight)
    {
        Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
    }

    void OnGUI()
    {
        GUI.contentColor = Color.red;
        
        AutoResize(800, 480);
        switch (CurrentGameState)
        {
            case GameState.Start:
                GUI.Label(new Rect(300, 250, 400, 200), "Tap the screen to start");
                break;
            case GameState.Won:
                GUI.Label(new Rect(300, 250, 400, 200), "You won! Tap the screen to restart");
                break;
            case GameState.Lost:
                GUI.Label(new Rect(300, 250, 400, 200), "You lost! Tap the screen to restart");
                break;
            default:
                break;
        }
    }


}
