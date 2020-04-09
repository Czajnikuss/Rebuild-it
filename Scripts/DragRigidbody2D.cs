
using UnityEngine;
using System.Collections;
 
public class DragRigidbody2D : MonoBehaviour
//attach it to camera
{
    private Ray ray;
    private RaycastHit2D hitInfo;
    public GameObject clickedObject;
    Rigidbody2D dragedObjectRigitbody;
    Vector3 clickedPosition;

  
   void Update () 
   {
 
        if(Input.GetMouseButton(0))
        {
            clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(clickedPosition);
            
        
        
        }
 
        
        
    }

 
}
 