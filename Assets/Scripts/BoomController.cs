using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomController : MonoBehaviour
{
    public float radius = 5, power = 10, upForce = 1, speed = 5;
    Rigidbody rb;
    public bool explode = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        StartCoroutine(DelayedDestroy());
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(1);
        DestroyThis();
    }

    void DestroyThis()
    {
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        Destroy(gameObject);
        foreach (Collider hit in colliders)
        {
            Rigidbody rbh = hit.GetComponent<Rigidbody>();
            if (rbh != null)
            {
                if (explode)
                {
                    rbh.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
                }
                else
                {
                    rbh.AddExplosionForce(-power * 5, explosionPosition, radius, upForce, ForceMode.VelocityChange);
                }
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Finish")
        {
            DestroyThis();
        }
    }
}
