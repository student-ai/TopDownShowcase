using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Anim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xvelo = GetComponent<Rigidbody2D>().velocity.x;
        float yvelo = GetComponent<Rigidbody2D>().velocity.y; 
        GetComponent<Animator>().SetFloat("x", xvelo);
        GetComponent<Animator>().SetFloat("y", yvelo);
    }
}
