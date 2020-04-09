using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject gameOverObject;
    public GameObject winnerObject;
    
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        gameOverObject = transform.Find("GameOver").gameObject;
        winnerObject = transform.Find("Winner").gameObject;
        

        
        gameOverObject.SetActive(false);
        winnerObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOverSummon()
    {
        //Instantiate(gameOverObject, transform.position, transform.rotation);
        gameOverObject.SetActive(true);
        levelManager.Disaster();
      //  levelManager.ClearRecording();
    }
    public void WinnerSummon()
    {
        winnerObject.SetActive(true);
    }


}
