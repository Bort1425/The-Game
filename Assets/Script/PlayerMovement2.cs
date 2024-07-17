using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    Vector2 move;
    public float screenBoundary = 4.2f;
    public float rotationSpeed;
    float rotationInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector2(0f, Input.GetAxis("P2_Vertical") * speed);
        rotationInput = Input.GetAxis("P2_Rotate");
        if(transform.position.y > screenBoundary){ //to keep the position of the player inside the box
            transform.position = new Vector2(transform.position.x,screenBoundary);
        }else if(transform.position.y <  -screenBoundary){
            transform.position = new Vector2(transform.position.x, -screenBoundary);
        }
        //Rotate around the Vector 3 * the speed in which i am rotating * my current framerate * the rotationInput that i give it
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * -rotationInput); 
    }

     void FixedUpdate()
    {
        rb.velocity = move;
    }

}
