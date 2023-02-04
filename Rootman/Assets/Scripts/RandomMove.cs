using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    public float speed = 10;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        var x = Random.Range(-1f, 1f);
        var z = Random.Range(-1f, 1f);
        direction = new Vector3(x, 0f, z).normalized;
        transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
