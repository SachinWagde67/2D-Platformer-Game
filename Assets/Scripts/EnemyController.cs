using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private int health = 100;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject bloodSplash;

    private bool movingRight = true;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundinfo = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);

        if(!groundinfo.collider)
        {
            if(movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else if(!movingRight)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
            Destroy(gameObject, 0.2f);
        }
    }
    
    private void Die()
    {
        SoundManager.Instance.Play(Sounds.EnemyDeath);
        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        anim.SetTrigger("dead");
        speed = 0;
    }
}
