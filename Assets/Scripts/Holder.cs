using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///     Any object that, upon facing a Rigidbody, can pick up said Rigidbody
/// </summary>
public class Holder : MonoBehaviour
{
    /// <summary>
    ///     Distance between position and holdable when held
    /// </summary>
    [SerializeField] private Vector3 holdOffset;

    /// <summary>
    ///     The rigidbody we are currently holding. Null if nothing is held.
    /// </summary>
    private Rigidbody held;

    /// <summary>
    ///     bruh
    /// </summary>
    private bool pickupPressed;

    void FixedUpdate()
    {
        if (pickupPressed)
        {
            if (held)
            {
                held.transform.parent = null;
                held.useGravity = true;
                held = null;
            }
            else
            {
                var holdableMask = 1 << LayerMask.NameToLayer("Holdable");
                if (Camera.main != null)
                {
                    var camTransform = Camera.main.transform;
                    var facingRay = new Ray(camTransform.position, camTransform.forward);
                    if (Physics.Raycast(facingRay, out var hitInfo, maxDistance: Mathf.Infinity, layerMask: holdableMask))
                    {
                        held = hitInfo.rigidbody;
                        held.transform.position = camTransform.position + camTransform.TransformDirection(holdOffset);
                        held.transform.parent = camTransform;
                        held.useGravity = false;
                    }
                }
            }

            pickupPressed = false;
        }
    }

    void OnPickUp(InputValue inputValue)
    {
        pickupPressed = inputValue.isPressed;
    }
}