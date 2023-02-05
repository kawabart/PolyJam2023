using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SpringJoint))]
public class RootShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraObject;
    public float distanceCompress = 1f;
    private SpringJoint joint;
    private PlayerInputs playerInputs;
    private LineRenderer lineRenderer = null;
    private bool isConnected = false;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<SpringJoint>();
        playerInputs = GetComponent<PlayerInputs>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInputs.rootUsed)
        {
            if (!isConnected)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, cameraObject.transform.forward, out hit))
                {
                    joint.connectedAnchor = hit.point;
                    joint.maxDistance = distanceCompress * hit.distance;
                    isConnected = true;
                    lineRenderer.enabled = true;
                }
            }
        }
        else
        {
            isConnected = false;
            joint.connectedAnchor = gameObject.transform.TransformPoint(joint.anchor);
            joint.maxDistance = float.MaxValue;
            lineRenderer.enabled = false;
        }
    }
}
