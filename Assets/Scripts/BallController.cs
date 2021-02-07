using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private int direction = 1;
    private float speed = 100;
    private float baseSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = speed;
        rb = GetComponent<Rigidbody>();
        pushBall();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = (Vector3.forward * direction * speed);

    }
    
    //If ball hits a paddle, reverse direction
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            direction = direction * -1;
            speed = speed + 25f;
            float angle = transform.position.x - other.gameObject.transform.position.x;
         
            pushBall(angle);   
        }

        if (other.gameObject.tag == "Finish")
        {
            resetBall();
        }

        if (other.gameObject.tag == "Wall")
        {
            rb.AddForce(rb.velocity*-2);
        }
    }

    private void resetBall()
    {
        speed = baseSpeed;
        transform.position = new Vector3(0, 1, 0);
        rb.velocity = Vector3.zero;
        pushBall();
    }

    private void pushBall(float angle)
    {
        Vector3 v3 = new Vector3(angle,0,0);
        rb.AddForce(Vector3.forward * direction * speed + (v3*speed));
    }

    private void pushBall()
    {
        float angle = Random.Range(-1f, 1f);
        
        Vector3 v3 = new Vector3(angle,0,0);
        rb.AddForce(Vector3.forward * direction * speed + (v3*speed));
    }
}
