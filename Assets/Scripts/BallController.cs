using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public Object leftPaddle;
    public Object rightPaddle;

    public AudioClip metalSound;
    public AudioClip wallSound;
    private AudioSource source;
    
    private Rigidbody rb;
    private int direction = 1;
    private int score1 = 0;
    private int score2 = 0;
    private float speed = 180;
    private float baseSpeed;

    public int scoreToWin = 11;
    
    public Text leftScore;
    public Text rightScore;
    public Text gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        
        
        baseSpeed = speed;
        rb = GetComponent<Rigidbody>();
        pushBall();
    }

    //If ball hits a paddle, reverse direction
    private void OnCollisionEnter(Collision other)
    {
        //If ball hits a paddle
        if (other.gameObject.tag == "Player")
        {
            //Increase ball speed when it hits a paddle
            speed = speed + 40f;
            
            //Change pitch of sound depending on speed
            source.clip = metalSound;
            source.pitch = (Math.Abs(rb.velocity.z) / 10);
            source.Play();
            
            float angle = transform.position.x - other.gameObject.transform.position.x;

            if (other.gameObject.Equals(leftPaddle))
            {
                direction = -1;
                pushBall(angle);  
            }

            if (other.gameObject.Equals(rightPaddle))
            {
                direction = 1;
                pushBall(angle);  
            }

        }

        //If ball hits a goal
        if (other.gameObject.tag == "Finish")
        {
            if (direction == 1)
            {
                score1 += 1;
                Debug.Log("Right Paddle Scored");
            } else if (direction == -1)
            {
                score2 += 1;
                Debug.Log("Left Paddle Scored");
            }
            Debug.Log(score2 + " - " + score1);
            if (score1 >= scoreToWin)
            {
                gameOver.color = Color.green; 
                gameOver.text = "Game over! Right paddle wins.";
                StartCoroutine(showGameOver());
                
                Debug.Log("Right Paddle Wins, GAME OVER!");
                score1 = 0;
                score2 = 0;
                Debug.Log("Score reset to 0 - 0.");
            }

            if (score2 >= scoreToWin)
            {
                Debug.Log("Left Paddle Wins, GAME OVER!");
                gameOver.color = Color.green; 
                gameOver.text = "Game over! Left paddle wins.";
                StartCoroutine(showGameOver());
                
                score1 = 0;
                score2 = 0;
                Debug.Log("Score reset to 0 - 0.");
            }
            
            updateScores();
            resetBall();
        }

        //If ball hits a wall
        if (other.gameObject.tag == "Wall")
        {
            //Change pitch of sound based on velocity
            source.clip = wallSound;
            source.pitch = (Math.Abs(rb.velocity.z) / 10);
            source.Play();
            
            rb.AddForce(new Vector3(rb.velocity.x*-2, rb.velocity.y,rb.velocity.z));
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
        Vector3 v3 = new Vector3(-angle,0,direction);
        rb.velocity = Vector3.zero;
        rb.AddForce(v3 * speed);
    }

    private void pushBall()
    {
        float angle = Random.Range(-1f, 1f) * 10f;
        
        Vector3 v3 = new Vector3(angle,0,direction * speed);
        rb.AddForce(v3);
    }

    private void updateScores()
    {
        leftScore.text = score1 + "";
        rightScore.text = score2 + "";
        //gameOver.text = "";

        if (score2 > 3 && score2 < 9)
        {
            rightScore.color = new Color(200 / 255f, 18 / 255f, 18 / 255f);
        } else if (score2 > 8 && score2 < 12)
        {
            rightScore.color = new Color(210 / 225f, 115 / 255f, 30 / 255f);
        }

        if (score1 > 3 && score1 < 9)
        {
            leftScore.color = new Color(200 / 255f, 18 / 255f, 18 / 255f);
        } else if (score1 > 8 && score1 < 12)
        {
            leftScore.color = new Color(210 / 225f, 115 / 255f, 30 / 255f);
        }

    }

    IEnumerator showGameOver()
    {
        gameOver.enabled = true;
        yield return new WaitForSeconds(3f);

        leftScore.color = new Color(23 / 255f, 117 / 255f, 154 / 255f);
        rightScore.color = new Color(23 / 255f, 117 / 255f, 154 / 255f);
        
        gameOver.enabled = false;
    }
}
