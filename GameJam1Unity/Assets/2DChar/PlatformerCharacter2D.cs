using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour 
{
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

	public GameObject projectile;
	public float rateOfFire = .5f;
	private float lastFire = 0f;
	public int playerID = 0;


    void Start()
	{
		jumpCount = jumpMax;
	}

	//Non-fixed update cuz we never want to miss a jump
	void Update()
	{
		//Uses input manager, space = jump
		if ((Input.GetAxis("Vertical"+playerID) > .1f) && ((Time.time - lastJump) > jumpWait) ){
			jump = true;
			lastJump = Time.time;
		}

		//Left click to shoot
		if ( ((Mathf.Abs(Input.GetAxis("FireHorizontal"+playerID)) > .2f) || (Mathf.Abs(Input.GetAxis("FireVertical"+playerID)) > .2f)) && ((Time.time - lastFire) > rateOfFire) )
		{
			//Casts ray from click point
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			//At a distance of zero (since its 2d) where is point in space?
			Vector3 worldClickPos3D = ray.GetPoint(0);

			//Vector2 aimVec = new Vector2(rigidbody2D.position.x - worldClickPos3D.x, rigidbody2D.position.y - worldClickPos3D.y);
			Vector2 aimVec = new Vector2(Input.GetAxis("FireHorizontal"+playerID), Input.GetAxis("FireVertical"+playerID));
			aimVec.Normalize();



			GameObject newProjectile = Instantiate(projectile, rigidbody2D.position + aimVec*-2, Quaternion.identity) as GameObject;
			//newProjectile.transform.parent = transform;
			newProjectile.rigidbody2D.AddForce(aimVec*-2000);

			lastFire = Time.time;
		}
		
		if (Input.GetKeyDown(KeyCode.R)) 
		{
			rigidbody2D.position = new Vector3(7, 32, 0); 
			
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

		float h = Input.GetAxis("Horizontal"+playerID);

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
