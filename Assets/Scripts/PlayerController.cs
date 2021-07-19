using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool facingRight;
    //[SerializeField] private float moveSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        HandleMovement(horizontal);
        Flip(horizontal);
    }

    private void HandleMovement(float horizontal)
    {
        anim.SetFloat("speed", Mathf.Abs(horizontal));
        //rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 thescale = transform.localScale;
            thescale.x *= -1;
            transform.localScale = thescale;
        }
    }

}
