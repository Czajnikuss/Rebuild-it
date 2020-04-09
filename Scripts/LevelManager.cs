using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

 


public class LevelManager : MonoBehaviour
{
//Save composition system////
#region SaveCompositionSystem
private class CompositionSaveData
{
    public int savedWidth, savedHight;
    public string copositionInString;

}

#endregion
////
    [Header("Audio Elements")]
   private AudioSource audioSource;
   public AudioClip firstTrack;
   public AudioClip secondTrack;
   [Header("Level Compositoin")]
   
   public string levelName;
   public int[,] levelComposition;
   public int hight, width;
   public GameObject[] typesOfBlocks;
   public int misplacedObjectsCount;
   private Vector3 clickedPosition;
  
    public List<GameObject>[] listOfTypeOfListsOfBlocks;
    
    public float disasterMagnitude =  1000f;
    private GameObject timerObject;
    private Text goalCount;
    private bool isLevelConstructed = false;
    public float howMuchTimeForLevel = 45.0f;
    public float momentOfStart;
    public bool isTimeUp = false;
[Header("UIElements")]
    public UIManager uIManager;
    public bool isHiddenCamOn = false;
    public Camera HiddenCam;
    private bool forcedStart = false;


public void Start()
    {
        #region VariableInitialization;
            timerObject = GameObject.Find("Timer");
            levelComposition = new int[width, hight];
            uIManager = FindObjectOfType<UIManager>();
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            
            audioSource.clip = firstTrack;
            audioSource.Play();
            goalCount = GameObject.Find("GolaCountTextField").GetComponent<Text>();
            listOfTypeOfListsOfBlocks = new List<GameObject>[typesOfBlocks.Length];
            for (int i = 0; i < typesOfBlocks.Length; i++)
            {
                                
                listOfTypeOfListsOfBlocks[i] = new List<GameObject>();
            }
            misplacedObjectsCount = 0;
            isLevelConstructed =false;
        #endregion 
        
      
        
        LevelConstruction();


//bool że level gotowy do gry po zakóczeniu katasrofy
}
public void CearLevel()
{
    GameObject[] tempAllBlocks = GameObject.FindGameObjectsWithTag("Block");
    foreach (var item in tempAllBlocks)
    {
        Destroy(item);
    }
}


public void ClearRecording()
{
    levelComposition = new int[width, hight];
}
public void LevelRecording()
{
    GameObject[] allBlocks;
    allBlocks = GameObject.FindGameObjectsWithTag("Block");
    foreach (var block in allBlocks)
    {
        levelComposition[Mathf.RoundToInt(block.transform.position.x),Mathf.RoundToInt(block.transform.position.y)] = block.GetComponent<BlockManager>().typeOfBlock;
    //adding to goal list    
         
            listOfTypeOfListsOfBlocks[block.GetComponent<BlockManager>().typeOfBlock -1].Add(block);
    
    }
   
}
public void CreateOnButtonPressed (int typeOfBlock)
{
    GameObject tempBlock;
    clickedPosition =new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
    
        tempBlock = Instantiate(typesOfBlocks[typeOfBlock - 1],clickedPosition, Quaternion.Euler(0,0,0) );
  
}

public void StartLevelInEditor()
{
    misplacedObjectsCount =0;
    momentOfStart = Time.time;
    forcedStart = true;
    CearLevel();
    ConstructHidenLayerHints();
    LevelConstruction();
    Disaster();
    

}
public void ConstructHidenLayerHints()
{
   GameObject[] hintObjects = new GameObject[typesOfBlocks.Length];
   for (int i = 0; i < typesOfBlocks.Length; i++)
   {
       hintObjects[i] = new GameObject();
       hintObjects[i].AddComponent<SpriteRenderer>();
       hintObjects[i].GetComponent<SpriteRenderer>().sprite = typesOfBlocks[i].GetComponent<SpriteRenderer>().sprite;
       hintObjects[i].layer = 20;
   } 
     
    for (int y = 0; y < hight; y++)
    {
        for (int x = 0; x < width; x++)
        {   
            if(levelComposition[x,y]!=0)
            {
                
            Instantiate(hintObjects[levelComposition[x,y]-1],new Vector2(x,y), Quaternion.Euler(0,0,0));
            }
        }

    }

}
public void doExitGame() {
     Application.Quit();
 }
public void LevelConstruction()
{        
    misplacedObjectsCount = 0;
    
    if(SceneManager.GetActiveScene().name =="Bunker" || SceneManager.GetActiveScene().name == "House")
    {
        LoadLevelComposition(SceneManager.GetActiveScene().name);
    }
//ustanowiuenie przeźroczystych spritów jako podpoiwieerdzi, pobierane z klockó docelowych.

//ustanowienie klockó w mijscach docelowych, lista klocków przyzwanych
    ConstructHidenLayerHints();
    for (int y = 0; y < hight; y++)
    {
        for (int x = 0; x < width; x++)
        {   
            if(levelComposition[x,y]!=0)
            {
                
            listOfTypeOfListsOfBlocks[levelComposition[x,y]-1].Add(Instantiate(typesOfBlocks[levelComposition[x,y]-1],new Vector2(x,y), Quaternion.Euler(0,0,0)));
            }
        }

    }
    
    //Time.timeScale = 0;

 foreach (var item in listOfTypeOfListsOfBlocks)
    {
        misplacedObjectsCount += item.Count;
    }
    if(SceneManager.GetActiveScene().name != "Editor")
    {
    Disaster();
    momentOfStart = Time.time;
    }
isLevelConstructed = true;
}
//wywołanie katastrofy
public void Disaster()
{
    
    GameObject[] tempAllBlocks = GameObject.FindGameObjectsWithTag("Block");
    foreach (var item in tempAllBlocks)
    {
        item.GetComponent<BlockManager>().enabled = false;
        item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        item.GetComponent<Rigidbody2D>().AddForce(new Vector3( UnityEngine.Random.onUnitSphere.x * disasterMagnitude ,Mathf.Abs( UnityEngine.Random.onUnitSphere.y * disasterMagnitude) , 0 ));
        item.transform.GetChild(0).gameObject.SetActive(false);
        
    }
    Invoke("BackToNormal", 1.5f);
}

public void BackToNormal ()
{
    GameObject[] tempAllBlocks = GameObject.FindGameObjectsWithTag("Block");
    foreach (var item in tempAllBlocks)
    {
        item.GetComponent<BlockManager>().enabled = true;
    }
    

}

public void ShowTimer(float timeLeft)
{
    if(timeLeft<=0) 
    {
        timeLeft =0;
        isTimeUp = true;
    }
    timerObject.GetComponent<Text>().text = "Time left: "+ Mathf.RoundToInt(timeLeft) + " seconds.";
    if (timeLeft<= howMuchTimeForLevel/3 & audioSource.clip!=secondTrack)
    {
        audioSource.Stop();
        audioSource.clip = secondTrack;
        audioSource.Play();
    }
}
////////////
//Save system.....
public void CompositionFileSave(string nameOfComposition)
{
    CompositionSaveData compositionToSave = new CompositionSaveData();
    compositionToSave.savedWidth = width;
    compositionToSave.savedHight = hight;
//twodimentiona array have to be flatted , using string with separator to flaten, then knowing width we can put it back together.
    string joinedData =null;
    for (int y = 0; y < hight; y++)
    {
        for (int x = 0; x < width; x++)
        {
            joinedData += levelComposition[x,y].ToString()+" ";
        }
    }
    compositionToSave.copositionInString = joinedData;


    string json = JsonUtility.ToJson(compositionToSave);
    Debug.Log(json);
    File.WriteAllText(Application.dataPath + "/Compositions/" + nameOfComposition + ".json",json);
    
}
public void CompositionFileLoad(string nameOfComposition)
{
    char[] separatorText = " ".ToCharArray();
    CompositionSaveData loadetData = new CompositionSaveData();
    string tempCompositionRead = File.ReadAllText(Application.dataPath + "/Compositions/" + nameOfComposition + ".json");
    loadetData = JsonUtility.FromJson<CompositionSaveData>(tempCompositionRead);
    width = loadetData.savedWidth;
    hight = loadetData.savedHight;
    string[] tempArrayInString =  loadetData.copositionInString.Split(separatorText );
    int i = 0;
    for (int y = 0; y < hight; y++)
    {
        for (int x = 0; x < width; x++)
        {
            i = (width * y) +x;
            levelComposition[x,y] = int.Parse( tempArrayInString[i]);    
            Debug.Log(levelComposition[x,y]);
        }
    }

    if(SceneManager.GetActiveScene().name =="Editor")
   {
    CearLevel();
    misplacedObjectsCount =0;
    LevelConstruction();
   }
    
    
    
}


public void SaveLevelComposition (string levelName)
{       
   
   for (int y = 0; y <= hight-1 ; y++)
   {
       for (int x = 0; x <= width -1; x++)
       {
           PlayerPrefs.SetInt(levelName + x.ToString()+y.ToString(),levelComposition[x,y]);

       }
   } 


}
public void LoadLevelComposition (string levelName)
{       
   int tempValue;
   for (int y = 0; y <= hight-1 ; y++)
   {
       for (int x = 0; x <= width-1; x++)
       {
            tempValue = PlayerPrefs.GetInt(levelName+ x.ToString()+y.ToString(),0);
            levelComposition[x,y]=tempValue;
       }
   } 
   if(SceneManager.GetActiveScene().name =="Editor")
   {
    CearLevel();
    misplacedObjectsCount =0;
    LevelConstruction();
   }
}
////////////////////

public void SceneLoadFromName(string sceneToLoad)
{
    string callFrom = SceneManager.GetActiveScene().name;
    if(sceneToLoad == "Reload")
    {
    sceneToLoad= SceneManager.GetActiveScene().name;
    }
    SceneManager.LoadScene(sceneToLoad);
    
    SceneManager.SetActiveScene(SceneManager.GetSceneByName( sceneToLoad)) ;



}
private void Update() 
{
    if(Input.GetKeyDown(KeyCode.Q)) 
    {
        CompositionFileSave("thisComposition");
        Debug.Log("Saved");
    }
    if(Input.GetKeyDown(KeyCode.E)) 
    {
        CompositionFileLoad("thisComposition");
        Debug.Log("Saved");
    }
    if(SceneManager.GetActiveScene().name !="Editor" || forcedStart)
    {
//Spradzanie czy odworowano kompozycję levelu 
//na poziomie klocka, tutaj dojdzie ilość typów klockó ustawionych
//i porównywanie z iloścxiąklocków przyxzwanych
//klocek we włąściwym miejscu oznaczony jakoś,,,
//sprawdzanie czy czas na level dobiegł końca.
    goalCount.text = "Blocks left: " + misplacedObjectsCount.ToString();
    ShowTimer(momentOfStart+howMuchTimeForLevel-Time.time);
    if (isTimeUp & misplacedObjectsCount>0)
        {
            uIManager.GameOverSummon();
            // Disaster();
        }
    
   
  //  Debug.Log("misplaced Objects = " +misplacedObjectsCount);
   /* misplacedObjectsCount = 0;
    foreach (var item in listOfTypeOfListsOfBlocks)
    {
        misplacedObjectsCount += item.Count;
    }
    */
//win/lose.
    if(misplacedObjectsCount == 0 & isLevelConstructed)
        {
            uIManager.WinnerSummon();
        }
    
    }

}

//end of class
}