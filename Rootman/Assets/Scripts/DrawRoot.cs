using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRoot : MonoBehaviour
{
    private SpringJoint _springJoint;
    private LineRenderer _lineRenderer = null;
    private bool _isHookObjectNull;

    // Start is called before the first frame update
    void Start()
    {
        _springJoint = GetComponent<SpringJoint>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startColor = Color.yellow;
        _lineRenderer.endColor = Color.yellow;
        _lineRenderer.startWidth = 0.03f;   // or just set it in the Inspector
        _lineRenderer.endWidth = 0.03f;   // or just set it in the Inspector
    }

    // Update is called once per frame
    void Update()
    {
        var characterPosition = transform.position + _springJoint.anchor;
        var hookPosition = _springJoint.connectedAnchor;

        characterPosition.y -= 0.15f;
        hookPosition.y += 0.1f;
        
        _lineRenderer.SetPosition(0, characterPosition);
        _lineRenderer.SetPosition(1, hookPosition);
    }
}
