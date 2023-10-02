using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jampad : MonoBehaviour
{
    public Vector2 _jumpForce;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var rb = other.attachedRigidbody;

        if (!rb)
        {
            return;
        }
        
        rb.AddForce(_jumpForce, ForceMode2D.Impulse);
    }
}
