using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerController : MonoBehaviour
{
    public GameObject pickupEffect;
    private float smallSize = 0.5f;
    private float bigSize = 3f;
    public float duration = 5f;
    public float effectDuration = 1.6f;

    public enum Type {Small, Big};

    public Type power = Type.Big;

    private void Start()
    {
        power = (Type) Random.Range(0, 2 );
        
        if (power == Type.Small)
        {
            //gameObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
        } else if (power == Type.Big)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(90/255f, 168/255f, 50/255f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {

            if (power == Type.Small)
            {
                StartCoroutine(smallBall(other));
            } else if (power == Type.Big)
            {
                StartCoroutine(bigBall(other));
            }
        }

    }

    IEnumerator smallBall(Collider ball)
    {
        StartCoroutine(playEffect());

        //Make ball smaller when power up is picked up
        ball.transform.localScale *= smallSize;
        
        //Hide mesh renderer and collider
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        
        //Wait 5 seconds before removing the size increase
        yield return new WaitForSeconds(duration);

        //Put ball back to normal size
        ball.transform.localScale /= smallSize;
        
        //Destroy power up
        Destroy(gameObject);
    }

    IEnumerator bigBall(Collider ball)
    {
        StartCoroutine(playEffect());

        //Make ball smaller when power up is picked up
        ball.transform.localScale *= bigSize;
        
        //Hide mesh renderer and collider
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        
        //Wait 5 seconds before removing the size increase
        yield return new WaitForSeconds(duration);

        //Put ball back to normal size
        ball.transform.localScale /= bigSize;
        
        //Destroy power up
        Destroy(gameObject);
    }

    IEnumerator playEffect()
    {
        GameObject explosion = Instantiate(pickupEffect, transform.position, transform.rotation);
        yield return new WaitForSeconds(effectDuration);
        Destroy(explosion);
    }
}
