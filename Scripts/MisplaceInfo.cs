using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MisplaceInfo : MonoBehaviour
{
    private LevelManager levelManager;
    // Start is called before the first frame update
    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
      //  if(levelManager==null)Debug.Log("nie znalazł levelManagera");
    }
    
    private void OnEnable() 
    {
        levelManager.misplacedObjectsCount--; 
     //   Debug.Log("was enabled");   
    }

    private void OnDisable() 
    {
        levelManager.misplacedObjectsCount++;  
     //   Debug.Log("was disabled");
    }
    
}
