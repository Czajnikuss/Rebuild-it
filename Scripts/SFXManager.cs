using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public GameObject SFXPrefab;
    private Camera cameraMain;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cameraMain = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            GameObject tempSFX = Instantiate(SFXPrefab, new Vector3(cameraMain.ScreenToWorldPoint(Input.mousePosition).x,cameraMain.ScreenToWorldPoint(Input.mousePosition).y, -1 ) , Quaternion.Euler(0,0,0) );
            tempSFX.GetComponent<ParticleSystem>().Play();
        }
    }
}
