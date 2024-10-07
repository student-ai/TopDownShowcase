using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // allows the variable moveSpeed to be changed in unity (lines 8 and 9)
    [SerializeField]
    float moveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check for horizontal and vertical input
        //move the player on that input
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        //Velocity is a Vector2 variable, which stores
        //2 floats, x and y
        GetComponent<Rigidbody2D>().velocity = new Vector2 (xInput, yInput) * moveSpeed;
    }
}
