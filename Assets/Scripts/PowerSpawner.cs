using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerSpawner : MonoBehaviour
{
    public GameObject powerUp;
    public float spawnTime = 12f;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnPowerUp", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnPowerUp()
    {
        Instantiate(powerUp, new Vector3(Random.Range(-7f, 7f), 1, Random.Range(-9f, 9f)), Quaternion.identity);
    }
    
}
