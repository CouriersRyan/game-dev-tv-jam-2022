using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
///     Any object that, upon facing a Holdable, can pick up said Holdable
/// </summary>
public class Holder : MonoBehaviour
{
    /// <summary>
    ///     Distance between position and holdable when held
    /// </summary>
    [SerializeField] private Vector3 holdOffset;
    
    void FixedUpdate()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo))
        {
            var holdable = hitInfo.collider.GetComponent<Holdable>();
            if (holdable) Hold(holdable);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="holdable"></param>
    private void Hold(Holdable holdable)
    {
        holdable.transform.position = transform.position + transform.TransformDirection(holdOffset);
        holdable.transform.parent = transform;
        var rigidbody = holdable.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
    }
}