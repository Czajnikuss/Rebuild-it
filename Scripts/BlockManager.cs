using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public int typeOfBlock;
   /*
      1 Wall,
      2 Roof, 
      3 Window

   */
    private LevelManager levelM;
    private Rigidbody2D blockBody;
    private bool wasCounted = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        levelM = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
        blockBody = this.GetComponent<Rigidbody2D>();
        for (int i = 0; i < levelM.typesOfBlocks.Length; i++)
        
        {
            if (levelM.typesOfBlocks[i].name + "(Clone)"== this.name)
            {
                typeOfBlock = i+1;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (levelM.levelComposition[Mathf.RoundToInt(this.transform.position.x),Mathf.RoundToInt(this.transform.position.y)] == typeOfBlock)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            blockBody.bodyType = RigidbodyType2D.Static;
          /*  if(!wasCounted)
            {   
                wasCounted = true;
                levelM.listOfTypeOfListsOfBlocks[typeOfBlock-1].Remove(this.gameObject);                
                
            }
          */  
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            blockBody.bodyType = RigidbodyType2D.Dynamic;
          /*  if(wasCounted)
            {
                wasCounted = false;
                levelM.listOfTypeOfListsOfBlocks[typeOfBlock-1].Add(this.gameObject);                
                
            }
          */  
        }
    }
}
