using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject cameraObject;
    public int killPoints = 10;
    public float shootingTimeSeconds = 0.3f;
    private float lastShootTimeSeconds = 0;
    private PlayerInputs playerInputs;
    private CountPoints countPoints;

    // Start is called before the first frame update
    void Start()
    {
        playerInputs = GetComponent<PlayerInputs>();
        countPoints = GetComponent<CountPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInputs.shooting && Time.time - lastShootTimeSeconds >= shootingTimeSeconds)
        {
            lastShootTimeSeconds = Time.time;
            RaycastHit hit;
            LayerMask layerMask = LayerMask.GetMask("Enemy");
            Debug.Log(layerMask.value.ToString());
            if (Physics.Raycast(transform.position, cameraObject.transform.forward, out hit, float.MaxValue, layerMask.value))
            {
                countPoints.AddPoints(killPoints);
                Destroy(hit.transform.gameObject);
            }
        }
    }
}
