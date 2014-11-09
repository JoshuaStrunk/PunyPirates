using UnityEngine;
using XInputDotNetPure; // Required in C#

public class PlatformerCharacter2D : MonoBehaviour 
{


	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	bool facingRight = true;							// For determining which way the player is currently facing.

	[SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
	[SerializeField] float jumpForce = 425f;			// Amount of force added when the player jumps.	

	[SerializeField] bool airControl = false;			// Whether or not a player can steer while jumping;
	[SerializeField] LayerMask whatIsGround;			// A mask determining what is ground to the character

    private bool jump;
	public int jumpMax = 2;
	private int jumpCount;
	public float jumpWait = .25f;
	private float lastJump = 0f;
	public int playerID = 0;
	


    void Start()
	{
		playerIndex = (PlayerIndex) playerID;
		jumpCount = jumpMax;
	}

	//Non-fixed update cuz we never want to miss a jump
	void Update()
	{
		prevState = state;
		state = GamePad.GetState(playerIndex);



		//Uses input manager, space = jump
		if ((state.ThumbSticks.Left.Y > .1f) && ((Time.time - lastJump) > jumpWait) ){
			jump = true;
			lastJump = Time.time;
		}



		
	}
	
	void OnCollisionEnter2D(Collision2D coll) 
	{

		if (coll.contacts[0].point.y < rigidbody2D.position.y)
		{
			jumpCount = jumpMax; 
		}
	}
	
	//Do actual physics and shiz fixed so we don't get different results at different dt's
	void FixedUpdate()
	{
		//Fake added "gravity" to give it that sweet classic platformer feel
		rigidbody2D.AddForce(new Vector2(0f, -40));

		float h = state.ThumbSticks.Left.X;

		// Pass all parameters to the character control script.
		Move( h , jump );

		jump = false;
	}

	public void Move(float move, bool jump)
	{

		// Move the character
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
			
		// If the input is moving the player right and the player is facing left...
		if(move > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(move < 0 && facingRight)
			// ... flip the player.
			Flip();


        // If the player should jump...
        if (jump && jumpCount > 0) {

			jumpCount--;

            // Add a vertical force to the player.
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0); 
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
	}

	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
