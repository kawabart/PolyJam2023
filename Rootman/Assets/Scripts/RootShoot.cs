using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SpringJoint))]
public class RootShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraObject;
    private SpringJoint joint;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<SpringJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, cameraObject.transform.forward, out hit))
            {
                joint.connectedAnchor = hit.point;
                joint.maxDistance = 0.5f * hit.distance;
            }
        }
    }
}
