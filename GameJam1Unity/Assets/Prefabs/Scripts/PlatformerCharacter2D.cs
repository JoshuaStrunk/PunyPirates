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


	public int jumpMax = 2;
	private int jumpCount;
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
		if ((state.ThumbSticks.Left.Y > .1f) && (prevState.ThumbSticks.Left.Y < .1f) ){
			jump();
		}



		
	}

	void OnTriggerEnter2D() {
		rigidbody2D.velocity *= .2f;
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{

		if (coll.contacts[0].point.y < rigidbody2D.position.y)// && coll.collider.tag != "CannonBall")
		{
			jumpCount = jumpMax; 
		}


	}
	
	//Do actual physics and shiz fixed so we don't get different results at different dt's
	void FixedUpdate()
	{

		float h = state.ThumbSticks.Left.X;
		// Pass all parameters to the character control script.
		if(Mathf.Abs(h) > 0f) 
		Move( h );

	}

	public void Move(float move)
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
       
	}

	public void jump() {
		// If the player should jump...
		if (jumpCount > 0) {
			
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
