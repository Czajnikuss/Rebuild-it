using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 clickedPosition;
    private Vector3 newTransformPosition;
    private Rigidbody2D dragedObjectRigidbody2D;
    public bool isStationary =false;

    
    

    // Start is called before the first frame update

private void Start() 
{
    dragedObjectRigidbody2D = GetComponent<Rigidbody2D>();    
    isStationary = false;
}

private void Update() 
{
    if(dragedObjectRigidbody2D.velocity.magnitude<=0.1f)
        {
            newTransformPosition = new Vector3(Mathf.Round( transform.position.x) ,Mathf.Round(transform.position.y) , transform.position.z); 
            this.transform.SetPositionAndRotation(newTransformPosition, Quaternion.Euler(0,0,0));
            
            isStationary =true;
        } 
    


}

    private void OnMouseDrag() 
    {
        clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);       
        if(clickedPosition.x<=0) clickedPosition.x = 0;
        if(clickedPosition.y<=0) clickedPosition.y = 0;
        if(clickedPosition.x>=13) clickedPosition.x = 13;
        if(clickedPosition.y>=9) clickedPosition.y = 9;
        
       // Debug.Log(clickedPosition);
        
        dragedObjectRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        newTransformPosition = new Vector3(Mathf.Round( clickedPosition.x) ,Mathf.Round(clickedPosition.y) , transform.position.z); 
        this.transform.SetPositionAndRotation(newTransformPosition, Quaternion.Euler(0,0,0));
        if(Input.GetKeyDown(KeyCode.Delete)) Destroy(this.gameObject);
    
    } 
}
