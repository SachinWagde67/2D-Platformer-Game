using UnityEngine;

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
	[SerializeField] private GameManager gameManager;
	[SerializeField] public int health;
	[SerializeField] private GameObject dustCloud;

	const float GroundedRadius = .2f; 
	const float CeilingRadius = .2f; 
	private float horizontal;
	private bool Grounded;
	private bool holdingGun = false;
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
		if(Input.GetKeyDown(KeyCode.Mouse0))
        {
			holdingGun = true;
        }
		else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
			holdingGun = false;
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
		anim.SetBool("holdinggun", holdingGun);
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
				if(holdingGun)
				{
					anim.SetBool("holdinggun", true);
				}
				move *= CrouchSpeed;
			}
			else
			{
				if (wasCrouching)
				{
					wasCrouching = false;
					anim.SetBool("crouch", false);
				}
				if (!holdingGun)
				{
					anim.SetBool("holdinggun", false);
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
			Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
			rb.AddForce(new Vector2(0f, JumpForce * 100));
		}
	}

	private void Flip()
	{
		facingRight = !facingRight;
		transform.Rotate(0, 180, 0);
	}

    //public void KeyPickUp()
    //{
    //    scoreManager.IncrementScore(10);
    //}

    //public void WaterDropletPickUp()
    //{
    //    scoreManager.IncrementScore(10);
    //}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("enemy"))
        {
			health -= 1;
			gameManager.Heart(health);
			if (health > 0)
			{
				anim.SetTrigger("hurt");
				SoundManager.Instance.Play(Sounds.PlayerHurt);
			}
			CheckHealth();
		}
		if(other.gameObject.CompareTag("deadzone"))
        {
			health -= 10;
			gameManager.Heart(health);
			CheckHealth();
		}
		if(other.gameObject.CompareTag("key"))
        {
			scoreManager.IncrementScore(10);
			scoreManager.IncrementKey(1);
        }
		if(other.gameObject.CompareTag("waterdroplet"))
        {
			scoreManager.IncrementScore(10);
			scoreManager.IncrementWaterDroplet(1);
        }
		if(other.gameObject.CompareTag("food"))
        {
			scoreManager.IncrementScore(10);
			scoreManager.IncrementFood(1);
        }
    }

    private void CheckHealth()
    {
		if(health <= 0)
        {
			anim.SetTrigger("dead");
			SoundManager.Instance.Play(Sounds.PlayerDeath);
			speed = 0;
        }
    }
}