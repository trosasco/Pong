using UnityEngine;

public class PaddleController : MonoBehaviour
{
    //Reference input manager
    public string input = "left paddle";

    private float limit = 6;
    private float key;
    public float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("key set");
    }

    // Update is called once per frame
    void Update()
    {
        //Create variables for positions
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        
        //Checking if variable x is past wall limit
        if (x > limit)
        {
            //Repositioning paddle to limited x range
            transform.position = new Vector3(limit,y,z);
        }
        if (x < limit * -1)
        {
            transform.position = new Vector3(limit*-1,y,z);
        }
        
        //Setting key variable to axis to move paddles
        key = Input.GetAxis(input);
        
        //When key is being pressed
        if (key != 0)
        {
            //Moves paddle to the direction the key axis is pointing
            transform.Translate(Vector3.right * Time.deltaTime * key * speed);
        }
    }
}
