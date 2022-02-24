using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject ImpactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
