using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private float horizontal;
    private float runSpeedModifier = 2f;
    private float crouchSpeedModifier = 0f;
    const float groundCheckRadius = 0.2f;
    const float overheadCheckRadius = 0.2f;

    private bool facingRight = true;
    private bool isRunning = false;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isCrouching = false;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Collider2D standingCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckCollider;
    [SerializeField] private Transform overheadCheckCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       
    }

    private void Update()
    {

        //Store the horizontal value
        horizontal = Input.GetAxisRaw("Horizontal");

        //If LeftShift is clicked -> enable isRunning
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        //If LeftShift is released -> disable isRunning
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        //If we press Jump button -> enable Jump
        if(Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            anim.SetBool("jump", true);            
        }
        //Else disable it
        else if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        //If we press Crouch button -> enable Crouch
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        }
        //Else disable it
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }

        //Set the yVelocity in the animator
        anim.SetFloat("yVelocity", rb.velocity.y);
        
    }

    private void FixedUpdate()
    {
        
        GroundCheck();
        Move(horizontal, isJumping,isCrouching);
    }

    private void GroundCheck()
    {
        isGrounded = false;

        //check if the groundcheckObject is colliding with other
        //2d colliders that are in the "Ground" Layer
        //if yes(isGrounded true) else (isGrounded false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if(colliders.Length > 0)
        {
            isGrounded = true;
        }

        //As long as we are grounded the "jump" bool
        //in the animator is disabled
        anim.SetBool("jump", !isGrounded);
    }

    private void Move(float horizontal, bool jumpflag, bool crouchFlag)
    {
        #region Jump && Crouch

        //If we are crouching and disabled crouching
        //check overhead for collision with ground items
        //if there are any, remain crouch otherwise un-crouch 
        if (!crouchFlag)
        {
            if (Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundLayer))
            {
                crouchFlag = true;
            }
        }

        if (isGrounded)
        {
            //Crouch
            //If the player isCrouching we enable crouch animation + disable standing collider and
            //reduce the player speed (its in move and run region)
            //If the player is not crouching we disable crouch animation + enable standing collider
            //return to original player speed
            standingCollider.enabled = !crouchFlag;

            //Jump
            //if the player isGrounded and pressed space Jump
            if (jumpflag)
            {
                jumpflag = false;
                //isGrounded = false;
                //Add jump force
                rb.AddForce(new Vector2(0f, jumpForce));
            }
        }

        anim.SetBool("crouch", crouchFlag);
        #endregion

        #region Move and Run
        //set value of x using dir and speed
        float xVal = horizontal * speed * 100 * Time.fixedDeltaTime; 

        //if we are running multiply with the runSpeedModifier
        if(isRunning)
        {
            xVal *= runSpeedModifier;
        }

        //if we are crouching reduce the player speed
        if (crouchFlag)
        {
            xVal *= crouchSpeedModifier;
        }

        //create vector2 for velocity
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        //set the player's velocity
        rb.velocity = targetVelocity;

        //Fliping the player
        //if looking right and clicked left -> flip to the left
        //if looking left and clicked right -> flip to the right
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y * 1, transform.localScale.z * 1);
        }

        //(0 Idle, 3 Walking, 6 Running)
        //Set the xVelocity according to the x value of tyhe rigidbody velocity
        anim.SetFloat("xVelocity",Mathf.Abs(rb.velocity.x));
        #endregion
    }
}
