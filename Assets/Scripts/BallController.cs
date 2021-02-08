using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    public Object leftPaddle;
    public Object rightPaddle;
    
    private Rigidbody rb;
    private int direction = 1;
    private int score1 = 0;
    private int score2 = 0;
    private float speed = 180;
    private float baseSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
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
            speed = speed + 40f;
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
            
            //rb.AddForce(new Vector3(rb.velocity.x, rb.velocity.y,rb.velocity.z*-2));
 
            
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
            if (score1 > 10)
            {
                Debug.Log("Right Paddle Wins, GAME OVER!");
                score1 = 0;
                score2 = 0;
                Debug.Log("Score reset to 0 - 0.");
            }

            if (score2 > 10)
            {
                Debug.Log("Left Paddle Wins, GAME OVER!");
                score1 = 0;
                score2 = 0;
                Debug.Log("Score reset to 0 - 0.");
            }
            
            resetBall();
        }

        //If ball hits a wall
        if (other.gameObject.tag == "Wall")
        {
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
}
