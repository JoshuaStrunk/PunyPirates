using UnityEngine;
using System.Collections;
using XInputDotNetPure; // Required in C#

public class ShootingController : MonoBehaviour {

	PlayerIndex playerIndex;
	GamePadState state;
	GamePadState prevState;

	public GameObject cannon;
	public int playerID = 0;

	public bool keyboardControl;

	private GameObject thisCannon;
	// Use this for initialization
	void Start () {
		playerIndex = (PlayerIndex) playerID;
		thisCannon = (GameObject)Instantiate (cannon, transform.position, transform.rotation);
		thisCannon.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		state = GamePad.GetState (playerIndex);

		//Left click to shoot
		if (((Mathf.Abs (state.ThumbSticks.Right.X) > .2f) || (Mathf.Abs (state.ThumbSticks.Right.Y) > .2f))) {
			CannonController c = thisCannon.GetComponent<CannonController> ();
			c.fire (-state.ThumbSticks.Right.X, -state.ThumbSticks.Right.Y);
		} else if (keyboardControl) 
		{
			if (Input.GetMouseButtonDown(0))
			{

				//Casts ray from click point
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				//At a distance of zero (since its 2d) where is point in space?
				Vector3 worldClickPos3D = ray.GetPoint(0);
				Vector2 aimVec = new Vector2(rigidbody2D.position.x - worldClickPos3D.x, rigidbody2D.position.y - worldClickPos3D.y);
				aimVec.Normalize();
				CannonController c = thisCannon.GetComponent<CannonController> ();
				c.fire(aimVec.x, aimVec.y);

			}
		}
	}
}
