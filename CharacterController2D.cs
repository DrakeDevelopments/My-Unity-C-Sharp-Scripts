using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[SerializeField] private float doubleJumpForce = 60000;                          // Amount of force added when the player double jumps.
	[SerializeField] private float m_PogoForce = 60000;                          // Amount of force added when the player pogos.

	[Range(0, .3f)] [SerializeField] public float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = true;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround = 0;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck= null;                           // A position marking where to check if the player is grounded.

	private bool canPogo = true;
	public float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded = false;            // Whether or not the player is grounded.
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.

	private Rigidbody2D m_Rigidbody2D;
	private AudioManager audioManager;
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public int doubleJumpCount = 0;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		audioManager = FindObjectOfType<AudioManager>();

		if (OnLandEvent == null)
        {
			OnLandEvent = new UnityEvent();
        }
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				doubleJumpCount = 0;
				if (!wasGrounded)
                {
					OnLandEvent.Invoke();
                }
			}
		}
	}

	public void DoubleJump()
    {
		if(PlayerStats.doubleJump == true & m_Grounded == false & doubleJumpCount == 0)
        {
			audioManager.Play("DoubleJump");
			m_Rigidbody2D.velocity = new Vector2(0, 0);
			m_Rigidbody2D.AddForce(new Vector2(0f, doubleJumpForce));
			doubleJumpCount++;
		}
    }

	public IEnumerator Pogo()
	{
		if (canPogo == true)
		{
			canPogo = false;
			m_Rigidbody2D.velocity = new Vector2(0, 0);
			m_Rigidbody2D.AddForce(new Vector2(0f, m_PogoForce));

			//waitforseconds doesn't work if using timescale 0 so you have to use realtime at the end.
			yield return new WaitForSecondsRealtime(0.1f); 
			canPogo = true;
		}

		else
		{
			yield return null;
		}
	}


    public void Move(float move, bool jump)
	{
		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}

			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}

		// If the player should jump...
		if (m_Grounded && jump && Time.timeScale > 0.3)
		{
			audioManager.Play("Jump");

			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		if (m_Grounded == true)
		{
			m_FacingRight = !m_FacingRight;
			transform.Rotate(0f, 180f, 0f);
		}
	}
}