using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    // Note, no need to update the position every frame, just set the velocity

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.velocity = transform.up * moveSpeed;
        }
        Invoke("Disable", 2.0f);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * moveSpeed;
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
