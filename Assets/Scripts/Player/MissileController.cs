using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        if (rb != null)
        {
            rb.velocity = transform.up * moveSpeed;
        }
        Invoke("Disable", 2f);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * moveSpeed;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Disable();
        }

    }
}
