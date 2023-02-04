using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

[RequireComponent(typeof(SpringJoint))]
public class RootShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraObject;
    [SerializeField]
    private GameObject origin;
    private CapsuleCollider groundDetectCollider;
    private CharacterController characterController;
    private StarterAssetsInputs _input;
    private SpringJoint joint;
    private Rigidbody rigidbody;
    private bool lastShoot;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        groundDetectCollider = GetComponent<CapsuleCollider>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.shoot && _input.shoot != lastShoot)
        {
            Debug.Log("Shoot");
            LayerMask mask = LayerMask.GetMask("Default");
            if (joint != null)
            {
                Destroy(rigidbody);
                rigidbody = null;
                Destroy(joint);
                joint = null;
                groundDetectCollider.enabled = false;
                characterController.enabled = true;
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(origin.transform.position, cameraObject.transform.forward, out hit, float.MaxValue, mask))
                {
                    rigidbody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
                    joint = gameObject.AddComponent(typeof(SpringJoint)) as SpringJoint;
                    joint.anchor = origin.transform.position;
                    joint.connectedAnchor = hit.point;
                    joint.maxDistance = 0.5f * hit.distance;
                    groundDetectCollider.enabled = true;
                    characterController.enabled = false;
                    Debug.Log($"position: {origin.transform.position}, hitPoint: {hit.point}");
                    Debug.DrawLine(origin.transform.position, hit.point, Color.red, 10);
                }
            }
        }

        lastShoot = _input.shoot;
    }

    void OnCollisionEnter()
    {
        Destroy(rigidbody);
        rigidbody = null;
        Destroy(joint);
        joint = null;
        groundDetectCollider.enabled = false;
        characterController.enabled = true;
    }
}
