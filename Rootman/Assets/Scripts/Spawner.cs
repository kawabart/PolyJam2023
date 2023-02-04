using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public int startDelaySeconds = 3;
    public int repeatDelaySeconds = 1;
    public int spawnCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", startDelaySeconds, repeatDelaySeconds);
    }

    void Spawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Object.Instantiate(spawnObject, transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
