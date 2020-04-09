using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallManager : MonoBehaviour
{
    public float lifeTime = 15;
    
    private Rigidbody2D myRigidbody;
    public float forceMagnitude = 1000;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody2D>();
        myRigidbody.AddForce(new Vector2(forceMagnitude, 0));
        Destroy(this, lifeTime );
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }
}
