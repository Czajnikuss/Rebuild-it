using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithMouse : MonoBehaviour
{
    Vector3 newCamPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        newCamPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Debug.Log(newCamPosition);
        transform.position = newCamPosition;
    }
}
