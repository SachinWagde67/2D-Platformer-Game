using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool facingRight;
    [SerializeField] private float jumpForce;
    //[SerializeField] private float moveSpeed;
   


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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        HandleMovement(horizontal,vertical);
        Flip(horizontal);
       
    }

    private void HandleMovement(float horizontal,float vertical)
    {
        anim.SetFloat("speed", Mathf.Abs(horizontal));
        //rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("crouch", true);
            rb.velocity = Vector2.zero;
        }
        else
        {
            anim.SetBool("crouch", false);
        }
        if(vertical > 0)
        {
            anim.SetTrigger("jump");
            rb.AddForce(new Vector2(0, jumpForce));
        }
        else
        {
            anim.ResetTrigger("jump");
        }
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
