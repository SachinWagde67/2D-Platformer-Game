using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float JumpForce = 400f;                        
	[SerializeField] private float CrouchSpeed = .36f;						 	  
	[SerializeField] private float speed;
	[SerializeField] private bool AirControl = false;                        
	[SerializeField] private LayerMask WhatIsGround;                          
	[SerializeField] private Animator anim;
	[SerializeField] private Transform GroundCheck;                          
	[SerializeField] private Transform CeilingCheck;                         
	[SerializeField] private ScoreManager scoreManager;
	[SerializeField] private Collider2D CrouchDisableCollider;

	const float GroundedRadius = .2f; 
	const float CeilingRadius = .2f; 
	private float horizontal;
	private bool Grounded;            
	private bool facingRight = true;  
	private bool wasCrouching = false;
	private bool crouch = false;
	private bool jump = false;
	private Rigidbody2D rb;
	private Vector3 velocity = Vector3.zero;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
		horizontal = Input.GetAxisRaw("Horizontal") * speed;
		anim.SetFloat("speed", Mathf.Abs(horizontal));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			anim.SetBool("jump", true);
		}
		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}
	}

    private void FixedUpdate()
	{
		bool wasGrounded = Grounded;
		Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				Grounded = true;
				if (!wasGrounded)
                {
					anim.SetBool("jump", false);
                }
			}
		}
		Move(horizontal * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}

	public void Move(float move, bool crouch, bool jump)
	{
		if (!crouch)
		{
			if (Physics2D.OverlapCircle(CeilingCheck.position, CeilingRadius, WhatIsGround))
			{
				crouch = true;
			}
		}
		if (Grounded || AirControl)
		{
			if (crouch)
			{
				if (!wasCrouching)
				{
					wasCrouching = true;
					anim.SetBool("crouch", true);
				}
				move *= CrouchSpeed;

				if (CrouchDisableCollider != null)
					CrouchDisableCollider.enabled = false;
			}
			else
			{
				if (CrouchDisableCollider != null)
					CrouchDisableCollider.enabled = true;

				if (wasCrouching)
				{
					wasCrouching = false;
					anim.SetBool("crouch", false);
				}
			}
			Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
			rb.velocity = targetVelocity;
			
			if (move > 0 && !facingRight)
			{
				
				Flip();
			}
			
			else if (move < 0 && facingRight)
			{
				Flip();
			}
		}
	
		if (Grounded && jump)
		{
			Grounded = false;
			rb.AddForce(new Vector2(0f, JumpForce * 100));
		}
	}

	private void Flip()
	{
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void KeyPickUp()
    {
		Debug.Log("Picked up a key");
		scoreManager.IncrementScore(10);
    }
}