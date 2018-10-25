using EZObjectPools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 6;

    private void OnEnable()
    {
        StartCoroutine(TimeToLive());
        rb.useGravity = false;
    }

    private IEnumerator TimeToLive()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity * speed;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            rb.useGravity = true;
        }
    }
}
